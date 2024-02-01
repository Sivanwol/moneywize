using System.Security.Claims;
using Application.Configs;
using Application.Configs.General;
using Application.Configs.Providers;
using Application.Constraints;
using Application.Events;
using Application.Events.Flow;
using Application.Extensions;
using BackendApi.Factories;
using Domain.Entities;
using Domain.Persistence;
using Domain.Persistence.Context;
using DotEnv.Core;
using GraphQL.AspNet.Configuration;
using GraphQL.AspNet.Security;
using HealthChecks.ApplicationStatus.DependencyInjection;
using HealthChecks.UI.Client;
using Infrastructure.Enums;
using Infrastructure.Services.Auth;
using Infrastructure.Services.Auth.Models;
using Infrastructure.Services.Cache;
using Infrastructure.Services.Email;
using Infrastructure.Validators;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.FeatureManagement;
using Microsoft.IdentityModel.Tokens;
using Processor;
using Serilog;
namespace BackendApi;

public class Bootstrap {

    private static TimeSpan KEEP_ALIVE_INTERVAL = TimeSpan.FromMilliseconds(10000);
    public Bootstrap(IConfiguration configuration, IWebHostEnvironment env) {
        Configuration = configuration;
        Environment = env;
        Log.Information("Start Bootstrap And Configuration Setup");
        ActiveAzureConfigConnectionString = Configuration["AzureConfigConnectionString"]!;
        IsLocalConfiguration = string.IsNullOrEmpty(ActiveAzureConfigConnectionString);
    }
    public static IWebHostEnvironment Environment { get; set; }
    public static bool IsLocalConfiguration { get; set; }

    public static IConfiguration Configuration { get; set; }
    public static string ActiveAzureConfigConnectionString { get; private set; }
    public static string ActiveConnectionString { get; set; }

    public void ConfigureServices(IServiceCollection services) {
        Log.Information("Start Configure Server");
        // Load configuration from Azure App Configuration
        services.AddAzureAppConfiguration()
            .AddFeatureManagement(Configuration.GetSection("FeatureManagement"));
        if (!IsLocalConfiguration) {
            services.Configure<BackendApplicationConfig>(Configuration.GetSection("ApplicationConfig"));
        }
        var applicationConfig = Configuration.GetSection("ApplicationConfig").Get<BackendApplicationConfig>()!;
        services.Configure<MailServerConfig>(Configuration.GetSection("MailConfig"));
        new EnvLoader().Load();
        var settings = new EnvBinder().Bind<BackendSecretApplicationConfig>();

        if (string.IsNullOrEmpty(applicationConfig.ConnectionString)) {
            ActiveConnectionString = Configuration.GetConnectionString("DbConnection")!;
            Log.Information("Change Domain Connection: {ActiveConnectionString}", ActiveConnectionString);
        }
        if (settings != null) {
            Log.Information("Override Configuration base environment variables");
            applicationConfig.JwtSecret = settings.JwtSecret;
            applicationConfig.SMSProviderInfouToken = settings.SMSProviderInfouToken;
            applicationConfig.EMailPassword = settings.EMailPassword;
            applicationConfig.EMailUser = settings.EMailUser;
            ActiveConnectionString = settings.DatabaseConnectionString;
            Log.Information("Change Domain Connection: {ActiveConnectionString}", ActiveConnectionString);
        }

        var jwtTokenConfig = new JwtTokenConfig {
            Secret = applicationConfig.JwtSecret,
            Issuer = applicationConfig.JwtIssuer,
            Audience = applicationConfig.JwtAudience,
            AccessTokenExpiration = applicationConfig.JwtAccessTokenExpired,
            RefreshTokenExpiration = applicationConfig.JwtRefreshTokenExpired
        };
        var infoUSmsProvider = new InfoUSmsConfig {
            UserName = applicationConfig.SMSProviderInfouUserName,
            Token = applicationConfig.SMSProviderInfouToken,
            Sender = applicationConfig.SMSProviderInfouSender
        };
        services.AddSingleton(jwtTokenConfig!);
        services.AddSingleton(applicationConfig);
        services.AddSingleton(infoUSmsProvider);
        // auth configuration
        services.Configure<IdentityOptions>(options => {
            options.User.RequireUniqueEmail = true;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings.
            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        });
        services.Configure<RazorViewEngineOptions>(options => {
            options.ViewLocationFormats.Add("{0}.cshtml");
        });

        // services.AddEntityFrameworkMySqlJsonNewtonsoft();
        services.AddGenericServiceExtension(Configuration, () => {
            var serverVersion = new MySqlServerVersion(new Version(5, 7));
            services.AddDbContext<DomainContext>(options => {
                options.UseMySql(ActiveConnectionString, serverVersion,
                        mySqlOptions => {
                            mySqlOptions.UseNewtonsoftJson();
                            mySqlOptions.EnableRetryOnFailure(
                                3,
                                TimeSpan.FromSeconds(30),
                                null);
                        })
                    .LogTo(Console.WriteLine, LogLevel.Information);
                if (Environment.IsDevelopment()) {
                    options.EnableSensitiveDataLogging()
                        .EnableDetailedErrors();
                }
            });
            services.Configure<FrontEndPaths>(Configuration.GetSection("FrontEndWebPaths"));

            services.AddTransient<IDomainContext>(provider => provider.GetService<DomainContext>()!);
            services.AddTransient<ITemplateService, TemplateService>();
            services.AddTransient<IMailService, MailService>();
            services.AddSingleton<IJwtAuthManager, JwtAuthManager>();
            services.AddTransient<ICacheService, CacheService>();
            services.AddValidatorsExtension();
            services.AddRazorPages();

            services.AddGraphQL(schemaOptions => {
                schemaOptions.QueryHandler.Route = "/api/gql";
                schemaOptions.AuthorizationOptions.Method = AuthorizationMethod.PerField;
                if (Environment.IsDevelopment()) {
                    schemaOptions.ResponseOptions.ExposeExceptions = true;
                    schemaOptions.ResponseOptions.ExposeMetrics = true;
                    schemaOptions.ExecutionOptions.DebugMode = true;
                }
                schemaOptions.ExecutionOptions.MaxQueryDepth = 15;
                schemaOptions.AddType<SortDirection>();
                schemaOptions.AddType<FilterOperations>();
                schemaOptions.AddType<PageSize>();
                schemaOptions.AddType<EntityColumnOperationType>();
                schemaOptions.AddType<EntityColumnDataType>();
                schemaOptions.AddType<EntityTypes>();

            });
            services.AddHostedService<JwtRefreshTokenCache>();
        });
        if (Environment.IsDevelopment() || applicationConfig.EnableSwagger) {
            services.AddSwaggerExtension(applicationConfig, "Application Backend Api Docs");
        }
        if (!Environment.IsDevelopment() || applicationConfig.EnableGraphQLCache) {
            services.AddGraphQLLocalQueryCache();
        }
        // services.AddLZ4Compressor("lz4");
        // More info: https://easycaching.readthedocs.io/en/latest/Redis/
        services.AddEasyCaching(option => {
            option.UseRedis(config => {

                    config.DBConfig.AllowAdmin = true;
                    config.DBConfig.SyncTimeout = 10000;
                    config.DBConfig.AsyncTimeout = 10000;
                    config.DBConfig.Endpoints.Add(new EasyCaching.Core.Configurations.ServerEndPoint(applicationConfig.RedisHost, applicationConfig.RedisPort));
                    if (applicationConfig.RedisAuth.Trim() != "") {
                        config.DBConfig.Password = applicationConfig.RedisAuth;
                    }
                    if (applicationConfig.RabbitMqSslProtocol) {
                        config.DBConfig.IsSsl = true;
                    }
                    config.DBConfig.Database = applicationConfig.RedisDb;
                    config.EnableLogging = true;
                    config.DBConfig.ConnectionTimeout = 10000;
                    config.SerializerName = Cache.SerializerName;
                }, Cache.ProviderName)
                .WithJson()
                .WithMessagePack(Cache.SerializerName);
            // .WithCompressor(Cache.SerializerName, "lz4");
        });

        // add repositories
        services.AddRepositoriesExtension();
        // add mocks services
        services.AddMocksExtension();

        #region AddMediatR

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies().Where(x => !x.FullName!.Contains("Sql")).ToArray()));
        // enabled elastic search 
        // services.AddElasticsearch(Configuration);
        services.AddMassTransitExtension(ActiveConnectionString, applicationConfig, bus => {
            ServiceProcessExtensions.AddConsumersExtension(applicationConfig, bus);
        }, cfg => {
            cfg.AddJobsExtension(applicationConfig);
        }, bus => {
            bus.AddSagaStateMachine<GeneralEventStateMachineFlow, EventStage>(cfg => {
                cfg.ConcurrentMessageLimit = 8;
            });
        });

        #endregion

        services.AddApiVersionExtension(applicationConfig);
        // health checks registration 
        if (!applicationConfig.DisableHealthCheck) { // we check if we want the health checks to be disabled (mostly in dev)
            var redisConnectionString = applicationConfig.RedisSsl
                ? $"rediss://{applicationConfig.RedisHost}:{applicationConfig.RedisPort},defaultDatabase={applicationConfig.RedisDb}"
                : $"redis://{applicationConfig.RedisHost}:{applicationConfig.RedisPort},defaultDatabase={applicationConfig.RedisDb}";
            if (applicationConfig.RedisAuth.Trim() == "") {
                redisConnectionString += $",password={applicationConfig.RedisAuth}";
            }
            var rabbitMqConnectionString = applicationConfig.RabbitMqSslProtocol
                ? $"amqps://{applicationConfig.RabbitMqUsername}:{applicationConfig.RabbitMqPassword}@{applicationConfig.RabbitMqHost}"
                : $"amqp://{applicationConfig.RabbitMqUsername}:{applicationConfig.RabbitMqPassword}@{applicationConfig.RabbitMqHost}";
            services.AddHealthChecks()
                .AddMySql(ActiveConnectionString, tags: new[] {
                    "db"
                })
                .AddRedis(redisConnectionString, tags: new[] {
                    "redis"
                })
                .AddRabbitMQ(rabbitConnectionString: rabbitMqConnectionString, tags: new[] {
                    "rabbitmq"
                })
                .AddApplicationStatus();
            services
                .AddHealthChecksUI()
                .AddMySqlStorage(ActiveConnectionString);
        }
        // auth registration

        services.AddIdentity<ApplicationUser, AspNetRoles>()
            .AddEntityFrameworkStores<DomainContext>()
            .AddSignInManager()
            .AddClaimsPrincipalFactory<AppClaimsFactory>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(o => {
            o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        });

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x => {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.IncludeErrorDetails = true;
            x.TokenValidationParameters = new TokenValidationParameters {
                ValidateIssuerSigningKey = true,
                // ValidAudience = jwtTokenConfig.Audience,
                // ValidIssuer = jwtTokenConfig.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(jwtTokenConfig.Secret)),
                ValidateAudience = false,
                ValidateIssuer = false,
                NameClaimType = ClaimTypes.NameIdentifier
            };
        });

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
        Log.Information("Perp Server");
        var applicationConfig = Configuration.GetSection("ApplicationConfig").Get<BackendApplicationConfig>()!;

        if (Environment.IsDevelopment() || applicationConfig.EnableSwagger) {
            app.UseSwaggerExtension(env);
        }
        if (!applicationConfig.DisableHealthCheck) {
            app.UseHealthChecks("/healthz", new HealthCheckOptions {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
                ResultStatusCodes = {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Degraded] = StatusCodes.Status500InternalServerError,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                }
            });
        }

        app.UseGenericServiceExtension(env, () => {
            if (!Environment.IsDevelopment()) {
                app.UseHsts();
            }
        });

        if (!IsLocalConfiguration) {
            app.UseAzureAppConfiguration();
        }
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseStaticFiles();
        app.UseEndpoints(endpoints => {
            // endpoints.MapGroup("/auth").MapIdentityApi<ApplicationUser>();
            endpoints.MapControllers().RequireCors("AllowAll");
        });
        app.UseWebSockets();
        app.UseGraphQL();
        Log.Information("End Perp Setup Starting Server");
    }
}