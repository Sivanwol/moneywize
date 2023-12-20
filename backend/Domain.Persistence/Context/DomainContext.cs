using System.Linq.Expressions;
using Application.Queries;
using Application.Utils;
using Domain.Entities;
using Domain.Utils;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Serilog;
namespace Domain.Persistence.Context;

public class DomainContext : IdentityDbContext<ApplicationUser>, IDomainContext {
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


    public new void Dispose() {
        _logger.LogInformation("Disposing DomainContext instance.");
        base.Dispose();
    }

    private void Initialize() {
        Log.Information("Initializing DomainContext.");
    }

    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);
        _logger.LogInformation("Configuring DomainContext model.");
        builder.SetDefaultValuesTableName();
        builder.SetDefaultForIdsAndDates();
        builder.Entity<ActivityLog>();
        builder.Entity<ActivityLog>()
            .HasOne(x => x.User)
            .WithMany(x => x.ActivityLogs)
            .HasForeignKey(x => x.UserId);
        builder.Entity<Countries>();
        builder.Entity<Languages>();
        builder.Entity<Tags>();
        builder.Entity<ApplicationUserOtpCodes>()
            .HasIndex(x => x.Token).IsUnique();

        builder.Entity<ApplicationUserOtpCodes>().Property(x => x.ProviderType).HasConversion<byte>();

        builder.Entity<VendorHasContact>()
            .HasKey(vc => new {
                vc.VendorId,
                vc.ContactId
            });
        builder.Entity<Contacts>();
        builder.Entity<Contacts>().HasIndex(x => x.Email).IsUnique();
        builder.Entity<Contacts>().HasIndex(x => new {
            x.CountryId,
            x.FirstName,
            x.LastName
        }).IsUnique();
        builder.Entity<Vendors>();
        builder.Entity<Vendors>().HasIndex(x => x.Name).IsUnique();
        builder.Entity<VendorsMetaData>();
        builder.Entity<Categories>();
        builder.Entity<Providers>();
        builder.Entity<Providers>().HasIndex(x => x.Name).IsUnique();
        builder.Entity<Clients>();
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
        builder.Entity<ClientsHasClientContacts>().HasKey(vc => new {
            vc.ClientId,
            vc.ContactId
        });
        builder.Entity<Assets>();
        builder.Entity<ClientServers>();
        builder.Entity<ClientServersHasAssets>().HasKey(vc => new {
            vc.ClientServerId,
            vc.AssetId
        });

        builder.Entity<ClientServerHasTags>().HasKey(vc => new {
            vc.ServerId,
            vc.TagId
        });
        builder.Entity<ClientNetworks>();
        builder.Entity<ClientNetworkHasTags>().HasKey(vc => new {
            vc.NetworkId,
            vc.TagId
        });
        builder.Entity<ClientNetworksHasAssets>().HasKey(vc => new {
            vc.ClientNetworkId,
            vc.AssetId
        });
        builder.Entity<ClientEmployees>();
        builder.Entity<ClientEmployeeHasTags>().HasKey(vc => new {
            vc.EmployeeId,
            vc.TagId
        });
        builder.Entity<ClientEmployeesHasAssets>().HasKey(vc => new {
            vc.ClientEmployeeId,
            vc.AssetId
        });

        builder.Entity<ProviderHasCategories>().HasKey(vc => new {
            vc.ProviderId,
            vc.CategoryId
        });
        builder.Entity<ProviderHasContact>().HasKey(vc => new {
            vc.ProviderId,
            vc.ContactId
        });

        #region Foreign Keys

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
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default) {
        foreach (var entry in ChangeTracker.Entries<ISoftDeletable>()) {
            switch (entry.State) {
                case EntityState.Deleted:
                    // Override removal. Unchanged is better than Modified, because the latter flags ALL properties for update.
                    // With Unchanged, the change tracker will pick up on the freshly changed properties and save them.
                    entry.State = EntityState.Unchanged;
                    entry.Property(nameof(ISoftDeletable.DeletedAt)).CurrentValue = SystemClock.Now();
                    break;
            }
        }
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}