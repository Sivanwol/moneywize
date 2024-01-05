using System.Linq.Expressions;
using Application.Queries;
using Application.Utils;
using Domain.Entities;
using Domain.Entities.Common;
using Domain.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Serilog;
namespace Domain.Persistence.Context;

public class DomainContext : IdentityDbContext<ApplicationUser, AspNetRoles, string,
    IdentityUserClaim<string>, ApplicationUserRole,
    IdentityUserLogin<string>, IdentityRoleClaim<string>,
    IdentityUserToken<string>>, IDomainContext {
    private readonly ILogger<DomainContext> _logger;

    public DomainContext(DbContextOptions<DomainContext> options, ILogger<DomainContext> logger)
        : base(options) {
        _logger = logger;
        _logger.LogInformation("Pre Initializing DomainContext.");
        ChangeTracker.CascadeDeleteTiming = CascadeTiming.OnSaveChanges;
        Initialize();
    }
    public DbContext Instance => this;

    public virtual DbSet<Countries> Countries { get; set; }
    public virtual DbSet<Languages> Languages { get; set; }
    public virtual DbSet<ApplicationUser> Users { get; set; }
    public virtual DbSet<ApplicationUserOtpCodes> UserOtpCodes { get; set; }
    public virtual DbSet<Vendors> Vendors { get; set; }
    public virtual DbSet<VendorsMetaData> VendorsMetaData { get; set; }
    public virtual DbSet<Contacts> Contacts { get; set; }
    public virtual DbSet<VendorHasContact> VendorContacts { get; set; }
    public virtual DbSet<ProviderHasContact> ProviderContacts { get; set; }
    public virtual DbSet<Providers> Providers { get; set; }
    public virtual DbSet<ProviderHasCategories> ProviderCategories { get; set; }
    public virtual DbSet<Categories> Categories { get; set; }
    public virtual DbSet<Tags> Tags { get; set; }
    public virtual DbSet<ClientServers> ClientServers { get; set; }
    public virtual DbSet<ClientNetworks> ClientNetworks { get; set; }
    public virtual DbSet<ClientServerHasTags> ClientServerTags { get; set; }
    public virtual DbSet<ClientNetworkHasTags> ClientNetworkTags { get; set; }
    public virtual DbSet<ActivityLog> ActivityLogs { get; set; }
    public virtual DbSet<Clients> Clients { get; set; }
    public virtual DbSet<Media> Media { get; set; }
    public virtual DbSet<Clients> Client { get; set; }
    public virtual DbSet<ClientEmployees> ClientEmployees { get; set; }
    public virtual DbSet<ClientEmployeeHasTags> ClientEmployeeTags { get; set; }
    public virtual DbSet<Assets> Assets { get; set; }
    public virtual DbSet<ClientContacts> ClientContacts { get; set; }
    public virtual DbSet<ClientsHasClientContacts> ClientHasClientContacts { get; set; }
    public virtual DbSet<ClientServersHasAssets> ClientServersHasAssets { get; set; }
    public virtual DbSet<ClientNetworksHasAssets> ClientNetworksHasAssets { get; set; }
    public virtual DbSet<ClientEmployeesHasAssets> ClientEmployeesHasAssets { get; set; }
    public virtual DbSet<ConfigurableCategories> ConfigurableCategories { get; set; }
    public virtual DbSet<ConfigurableEntityColumnDefinition> ConfigurableEntityColumnDefinitions { get; set; }
    public virtual DbSet<ConfigurableUserView> ConfigurableUserViews { get; set; }
    public virtual DbSet<ConfigurableUserViewHasConfigurableEntityColumnDefinition> ConfigurableUserViewHasConfigurableEntityColumnDefinitions { get; set; }
    public virtual DbSet<ConfigurableUserViewTags> ConfigurableUserViewTags { get; set; }
    public virtual DbSet<ConfigurableUserViewHasConfigurableUserViewTags> ConfigurableUserViewHasConfigurableUserViewTags { get; set; }
    public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }

    public new void Dispose() {
        _logger?.LogInformation("Disposing DomainContext instance.");
        base.Dispose();
    }

    private void Initialize() {
        Log.Information("Initializing DomainContext.");
        UpdateTimestamps();
    }

    private void UpdateTimestamps() {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.State is EntityState.Added or EntityState.Modified);

        foreach (var entityEntry in entries) {
            var entity = entityEntry.Entity;

            // Handle CreatedAt
            var createdAtProperty = entity.GetType().GetProperty("CreatedAt");
            if (createdAtProperty != null && entityEntry.State == EntityState.Added) {
                createdAtProperty.SetValue(entity, DateTime.Now);
            }

            // Handle UpdatedAt
            var updatedAtProperty = entity.GetType().GetProperty("UpdatedAt");
            if (updatedAtProperty != null) {
                updatedAtProperty.SetValue(entity, DateTime.Now);
            }
        }
    }
    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);
        _logger?.LogInformation("Configuring DomainContext model.");
        builder.SetDefaultValuesTableName();
        builder.SetDefaultForIdsAndDates();
        builder.Entity<ActivityLog>()
            .HasOne(x => x.User)
            .WithMany(x => x.ActivityLogs)
            .HasForeignKey(x => x.UserId);

        builder.Entity<ConfigurableUserView>().Property(x => x.Settings).HasColumnType("json");
        builder.Entity<ConfigurableUserView>().HasIndex(x => new {
            x.UserId,
            x.Name
        }).IsUnique();

        builder.Entity<ConfigurableUserViewTags>().HasIndex(x => new {
            x.UserId,
            x.Name
        }).IsUnique();

        builder.Entity<ConfigurableEntityColumnDefinition>().Property(e => e.MetaData).HasColumnType("json");
        builder.Entity<ConfigurableEntityColumnDefinition>().Property(e => e.Permissions).HasColumnType("json");
        builder.Entity<ConfigurableEntityColumnDefinition>().HasIndex(x => new {
            x.EntityName,
            x.ColumnName,
            x.DisplayName
        }).IsUnique();
        builder.Entity<ApplicationUserOtpCodes>()
            .HasIndex(x => x.Token).IsUnique();

        builder.Entity<ApplicationUserOtpCodes>().Property(x => x.ProviderType).HasConversion<byte>();

        builder.Entity<ConfigurableUserViewHasConfigurableEntityColumnDefinition>().HasKey(vc => new {
            vc.ConfigurableUserViewId,
            vc.ConfigurableEntityColumnDefinitionId
        });

        builder.Entity<ConfigurableUserViewHasConfigurableUserViewTags>().HasKey(vc => new {
            vc.ConfigurableUserViewId,
            vc.ConfigurableUserViewTagsId
        });

        builder.Entity<VendorHasContact>()
            .HasKey(vc => new {
                vc.VendorId,
                vc.ContactId
            });
        builder.Entity<ProviderHasContact>()
            .HasKey(vc => new {
                vc.ProviderId,
                vc.ContactId
            });
        builder.Entity<ProviderHasCategories>()
            .HasKey(x => new {
                x.ProviderId,
                x.CategoryId
            });
        builder.Entity<ClientServerHasTags>()
            .HasKey(x => new {
                x.ServerId,
                x.TagId
            });
        builder.Entity<ClientNetworkHasTags>()
            .HasKey(x => new {
                x.NetworkId,
                x.TagId
            });
        builder.Entity<ClientEmployeeHasTags>()
            .HasKey(x => new {
                x.EmployeeId,
                x.TagId
            });
        builder.Entity<ClientServersHasAssets>()
            .HasKey(x => new {
                x.ClientServerId,
                x.AssetId
            });
        builder.Entity<ClientNetworksHasAssets>()
            .HasKey(x => new {
                x.ClientNetworkId,
                x.AssetId
            });
        builder.Entity<ClientEmployeesHasAssets>()
            .HasKey(x => new {
                x.ClientEmployeeId,
                x.AssetId
            });
        builder.Entity<ClientsHasClientContacts>()
            .HasKey(x => new {
                x.ClientId,
                x.ContactId
            });
        builder.Entity<VendorHasContact>()
            .HasKey(x => new {
                x.VendorId,
                x.ContactId
            });
        builder.Entity<Contacts>().HasIndex(x => x.Email).IsUnique();
        builder.Entity<Contacts>().HasIndex(x => new {
            x.CountryId,
            x.FirstName,
            x.LastName
        }).IsUnique();
        builder.Entity<Vendors>().HasIndex(x => x.Name).IsUnique();
        builder.Entity<Providers>().HasIndex(x => x.Name).IsUnique();
        builder.Entity<Clients>().HasIndex(x => new {
            x.OwnerUserId,
            x.ParentId,
            x.CountryId,
            x.Address,
            x.City,
            x.Name
        }).IsUnique();
        builder.Entity<Clients>().HasOne(x => x.OwnerUser)
            .WithMany(x => x.Clients)
            .HasForeignKey(x => x.OwnerUserId);
        builder.Entity<ClientContacts>();
        builder.Entity<ClientContacts>().HasIndex(x => x.Email).IsUnique();
        builder.Entity<ClientContacts>().HasIndex(x => new {
            x.CountryId,
            x.FirstName,
            x.LastName
        }).IsUnique();

        #region Foreign Keys

        builder.Entity<ConfigurableUserViewHasConfigurableEntityColumnDefinition>()
            .HasOne(vc => vc.ConfigurableUserView)
            .WithMany(v => v.Columns)
            .HasForeignKey(vc => vc.ConfigurableUserViewId);
        builder.Entity<ConfigurableUserViewHasConfigurableEntityColumnDefinition>()
            .HasOne(vc => vc.ConfigurableEntityColumnDefinition)
            .WithMany(c => c.Views)
            .HasForeignKey(vc => vc.ConfigurableEntityColumnDefinitionId);

        builder.Entity<ConfigurableUserViewHasConfigurableUserViewTags>()
            .HasOne(vc => vc.ConfigurableUserView)
            .WithMany(v => v.Tags)
            .HasForeignKey(vc => vc.ConfigurableUserViewId);
        builder.Entity<ConfigurableUserViewHasConfigurableUserViewTags>()
            .HasOne(vc => vc.ConfigurableUserViewTags)
            .WithMany(c => c.Views)
            .HasForeignKey(vc => vc.ConfigurableUserViewTagsId);

        builder.Entity<ClientNetworkHasTags>()
            .HasOne(vc => vc.Tag)
            .WithMany(v => v.ClientNetworks)
            .HasForeignKey(vc => vc.TagId);

        builder.Entity<ClientNetworkHasTags>()
            .HasOne(vc => vc.Network)
            .WithMany(c => c.Tags)
            .HasForeignKey(vc => vc.NetworkId);

        builder.Entity<ClientNetworksHasAssets>()
            .HasOne(vc => vc.Asset)
            .WithMany(v => v.Networks)
            .HasForeignKey(vc => vc.AssetId);

        builder.Entity<ClientNetworksHasAssets>()
            .HasOne(vc => vc.Network)
            .WithMany(c => c.Assets)
            .HasForeignKey(vc => vc.ClientNetworkId);

        builder.Entity<ClientServerHasTags>()
            .HasOne(vc => vc.Tag)
            .WithMany(v => v.ClientServers)
            .HasForeignKey(vc => vc.TagId);

        builder.Entity<ClientServerHasTags>()
            .HasOne(vc => vc.Server)
            .WithMany(c => c.Tags)
            .HasForeignKey(vc => vc.ServerId);

        builder.Entity<ClientServersHasAssets>()
            .HasOne(vc => vc.Asset)
            .WithMany(v => v.Servers)
            .HasForeignKey(vc => vc.AssetId);

        builder.Entity<ClientServersHasAssets>()
            .HasOne(vc => vc.Server)
            .WithMany(c => c.Assets)
            .HasForeignKey(vc => vc.ClientServerId);

        builder.Entity<ClientEmployeeHasTags>()
            .HasOne(vc => vc.Tag)
            .WithMany(v => v.ClientEmployees)
            .HasForeignKey(vc => vc.TagId);

        builder.Entity<ClientEmployeeHasTags>()
            .HasOne(vc => vc.Employee)
            .WithMany(c => c.Tags)
            .HasForeignKey(vc => vc.EmployeeId);

        builder.Entity<ClientEmployeesHasAssets>()
            .HasOne(vc => vc.Asset)
            .WithMany(v => v.Employees)
            .HasForeignKey(vc => vc.AssetId);

        builder.Entity<ClientEmployeesHasAssets>()
            .HasOne(vc => vc.Employee)
            .WithMany(c => c.Assets)
            .HasForeignKey(vc => vc.ClientEmployeeId);

        builder.Entity<ProviderHasCategories>()
            .HasOne(vc => vc.Provider)
            .WithMany(v => v.Categories)
            .HasForeignKey(vc => vc.ProviderId);

        builder.Entity<ProviderHasCategories>()
            .HasOne(vc => vc.Category)
            .WithMany(c => c.Providers)
            .HasForeignKey(vc => vc.CategoryId);

        builder.Entity<ProviderHasContact>()
            .HasOne(vc => vc.Provider)
            .WithMany(v => v.Contacts)
            .HasForeignKey(vc => vc.ProviderId);

        builder.Entity<ProviderHasContact>()
            .HasOne(vc => vc.Contact)
            .WithMany(c => c.Providers)
            .HasForeignKey(vc => vc.ContactId);

        builder.Entity<VendorHasContact>()
            .HasOne(vc => vc.Vendor)
            .WithMany(v => v.Contacts)
            .HasForeignKey(vc => vc.VendorId);

        builder.Entity<VendorHasContact>()
            .HasOne(vc => vc.Contact)
            .WithMany(c => c.Vendors)
            .HasForeignKey(vc => vc.ContactId);

        #endregion

    }

    private static void ConfigureSoftDeleteFilter(ModelBuilder builder) {
        foreach (var softDeletableTypeBuilder in builder.Model.GetEntityTypes()
                     .Where(x => typeof(ISoftDeletable).IsAssignableFrom(x.ClrType))) {
            var parameter = Expression.Parameter(softDeletableTypeBuilder.ClrType, "p");

            softDeletableTypeBuilder.SetQueryFilter(
                Expression.Lambda(
                    Expression.Equal(
                        Expression.Property(parameter, nameof(ISoftDeletable.DeletedAt)),
                        Expression.Constant(null)),
                    parameter)
            );
        }
    }
    public override int SaveChanges() {
        OnBeforeSaving();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken)) {
        OnBeforeSaving();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void OnBeforeSaving() {
        var entries = ChangeTracker.Entries();
        var utcNow = SystemClock.Now().DateTime;

        foreach (var entry in entries) {
            switch (entry.Entity) {
                case BaseEntity trackable:
                    UpdateTimestamps(entry, trackable, utcNow);
                    break;

                case BaseEntityWithoutId trackableWithoutId:
                    UpdateTimestamps(entry, trackableWithoutId, utcNow);
                    break;
            }
        }
    }

    private void UpdateTimestamps(EntityEntry entry, object trackable, DateTime utcNow) {
        switch (entry.State) {
            case EntityState.Modified:
                // Assuming BaseEntityWithoutId has an UpdatedAt property
                ((dynamic)trackable).UpdatedAt = utcNow;

                // Check if the property "CreatedAt" exists before trying to set its modification status
                if (entry.Properties.Any(p => p.Metadata.Name == "CreatedAt")) {
                    entry.Property("CreatedAt").IsModified = false;
                }
                break;

            case EntityState.Added:
                // Assuming BaseEntityWithoutId does not have a CreatedAt property
                if (entry.Properties.Any(p => p.Metadata.Name == "CreatedAt")) {
                    ((dynamic)trackable).CreatedAt = utcNow;
                }

                // Assuming BaseEntityWithoutId has an UpdatedAt property
                ((dynamic)trackable).UpdatedAt = utcNow;
                break;
        }
    }
}