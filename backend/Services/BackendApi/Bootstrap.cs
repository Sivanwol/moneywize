using System.Text;
using Application.Configs;
using Application.Configs.Providers;
using Application.Events;
using Application.Events.Flow;
using Application.Extensions;
using Domain.Entities;
using Domain.Persistence;
using Domain.Persistence.Context;
using FluentValidation;
using HealthChecks.ApplicationStatus.DependencyInjection;
using HealthChecks.UI.Client;
using Infrastructure.Services.Auth;
using Infrastructure.Services.Auth.Models;
using Infrastructure.Services.Email;
using Infrastructure.Validators.Backoffice.Account;
using MassTransit;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.FeatureManagement;
using Microsoft.IdentityModel.Tokens;
using Processor;
using Serilog;
namespace BackendApi;

public class Bootstrap {
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
            .AddFeatureManagement();
        if (!IsLocalConfiguration) {
            services.Configure<BackendApplicationConfig>(Configuration.GetSection("ApplicationConfig"));
        }
        var applicationConfig = Configuration.GetSection("ApplicationConfig").Get<BackendApplicationConfig>()!;
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
            Sender = applicationConfig.SMSProviderInfouSender,
            TotalRetriesWithinSession = applicationConfig.SMSOtpTotalRetriesWithinSession,
            ExpiredInMinutes = applicationConfig.SMSCodeExpiredInMinutes
        };
        if (string.IsNullOrEmpty(applicationConfig.ConnectionString)) {
            ActiveConnectionString = Configuration.GetConnectionString("DbConnection")!;
            Log.Information($"Domain Connection: {ActiveConnectionString}");
        }
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
        services.AddGenericServiceExtension(Configuration, () => {
            var serverVersion = new MySqlServerVersion(new Version(5, 7));
            services.AddDbContext<DomainContext>(options => {
                options.UseMySql(ActiveConnectionString, serverVersion,
                        mySqlOptions => {
                            mySqlOptions.EnableRetryOnFailure(
                                maxRetryCount: 3,
                                maxRetryDelay: TimeSpan.FromSeconds(30),
                                errorNumbersToAdd: null);
                        })
                    .LogTo(Console.WriteLine, LogLevel.Information);
                if (Environment.IsDevelopment()) {
                    options.EnableSensitiveDataLogging()
                        .EnableDetailedErrors();
                }
            });
            services.Configure<MailServerConfig>(Configuration.GetSection(nameof(MailServerConfig)));
            services.AddTransient<IDomainContext>(provider => provider.GetService<DomainContext>()!);
            services.AddTransient<ITemplateService, TemplateService>();
            services.AddTransient<IMailService, MailService>();
            services.AddSingleton<IJwtAuthManager, JwtAuthManager>();
            services.AddValidatorsFromAssemblyContaining<ForgetPasswordValidator>();
            services.AddValidatorsFromAssemblyContaining<RegisterUserValidator>();
            services.AddValidatorsFromAssemblyContaining<ResetPasswordValidator>();
            services.AddValidatorsFromAssemblyContaining<RefreshTokenValidator>();
            services.AddValidatorsFromAssemblyContaining<LoginUserValidator>();
            services.AddValidatorsFromAssemblyContaining<SendCodeToProviderValidator>();
            services.AddValidatorsFromAssemblyContaining<SendCodeFromProviderValidator>();

            services.AddHostedService<JwtRefreshTokenCache>();
        });
        if (Environment.IsDevelopment() || applicationConfig.EnableSwagger) {
            services.AddSwaggerExtension(applicationConfig, "Application Backend Api Docs");
        }

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
            ServiceProcessExtensions.AddJobsExtension(cfg, applicationConfig);
        }, bus => {
            bus.AddSagaStateMachine<GeneralEventStateMachineFlow, EventStage>(cfg => {
                cfg.ConcurrentMessageLimit = 8;
            });
        });

        #endregion

        services.AddApiVersionExtension(applicationConfig);
        // health checks registration 
        if (!applicationConfig.DisableHealthCheck) { // we check if we want the health checks to be disabled (mostly in dev)
            services.AddHealthChecks()
                .AddMySql(ActiveConnectionString, tags: new[] {
                    "db"
                })
                .AddApplicationStatus();
            services.AddHealthChecksUI().AddSqlServerStorage(ActiveConnectionString);
        }
        // auth registration

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<DomainContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();
        services.AddAuthentication(o => {
            o.DefaultScheme = IdentityConstants.ApplicationScheme;
            o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        });

        services.AddAuthentication("Bearer").AddJwtBearer(x => {
            x.RequireHttpsMetadata = true;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters {
                ValidateIssuer = true,
                ValidIssuer = jwtTokenConfig.Issuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.Secret)),
                ValidAudience = jwtTokenConfig.Audience,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(1)
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
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
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

        app.UseEndpoints(endpoints => {
            // endpoints.MapGroup("/auth").MapIdentityApi<ApplicationUser>();
            endpoints.MapControllers();
        });
        Log.Information("End Perp Setup Starting Server");
    }
}