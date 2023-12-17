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
    public virtual DbSet<VendorContact> VendorContacts { get; set; }
    public virtual DbSet<ProviderContact> ProviderContacts { get; set; }
    public virtual DbSet<Providers> Providers { get; set; }
    public virtual DbSet<ProviderCategories> ProviderCategories { get; set; }
    public virtual DbSet<Categories> Categories { get; set; }
    public virtual DbSet<Tags> Tags { get; set; }
    public virtual DbSet<ClientServers> ClientServers { get; set; }
    public virtual DbSet<ClientNetworks> ClientNetworks { get; set; }
    public virtual DbSet<ClientServerTags> ClientServerTags { get; set; }
    public virtual DbSet<ClientNetworkTags> ClientNetworkTags { get; set; }
    public virtual DbSet<ActivityLog> ActivityLog { get; set; }
    public virtual DbSet<Clients> Clients { get; set; }
    public virtual DbSet<Media> Media { get; set; }
    public virtual DbSet<Clients> Client { get; set; }
    public virtual DbSet<ClientTags> ClientTags { get; set; }
    public virtual DbSet<ClientEmployees> ClientEmployees { get; set; }
    public virtual DbSet<ClientEmployeeTags> ClientEmployeeTags { get; set; }
    public virtual DbSet<Assets> Assets { get; set; }

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
        builder.Entity<Countries>();
        builder.Entity<Languages>();
        builder.Entity<Tags>();
        builder.Entity<ApplicationUserOtpCodes>()
            .HasIndex(x => x.Token).IsUnique();

        builder.Entity<ApplicationUserOtpCodes>().Property(x => x.ProviderType).HasConversion<byte>();

        builder.Entity<VendorContact>()
            .HasKey(vc => new {
                vc.VendorId,
                vc.ContactId
            });
        builder.Entity<Contacts>();
        builder.Entity<Vendors>();
        builder.Entity<VendorsMetaData>();
        builder.Entity<Categories>();
        builder.Entity<Providers>();
        builder.Entity<Clients>();
        builder.Entity<ClientTags>()
            .HasKey(vc => new {
                vc.ClientId,
                vc.TagId
            });
        builder.Entity<Assets>();
        builder.Entity<ClientServers>();
        builder.Entity<ClientServerTags>().HasKey(vc => new {
            vc.ServerId,
            vc.TagId
        });
        builder.Entity<ClientNetworks>();
        builder.Entity<ClientNetworkTags>().HasKey(vc => new {
            vc.NetworkId,
            vc.TagId
        });
        builder.Entity<ClientEmployees>();
        builder.Entity<ClientEmployeeTags>().HasKey(vc => new {
            vc.EmployeeId,
            vc.TagId
        });

        builder.Entity<ProviderCategories>().HasKey(vc => new {
            vc.ProviderId,
            vc.CategoryId
        });
        builder.Entity<ProviderContact>().HasKey(vc => new {
            vc.ProviderId,
            vc.ContactId
        });
        builder.Entity<ClientTags>().HasKey(vc => new {
            vc.ClientId,
            vc.TagId
        });

        #region Foreign Keys

        builder.Entity<ClientNetworkTags>()
            .HasOne(vc => vc.Tag)
            .WithMany(v => v.ClientNetworks)
            .HasForeignKey(vc => vc.TagId);

        builder.Entity<ClientNetworkTags>()
            .HasOne(vc => vc.Network)
            .WithMany(c => c.Tags)
            .HasForeignKey(vc => vc.NetworkId);

        builder.Entity<ClientServerTags>()
            .HasOne(vc => vc.Tag)
            .WithMany(v => v.ClientServers)
            .HasForeignKey(vc => vc.TagId);

        builder.Entity<ClientServerTags>()
            .HasOne(vc => vc.Server)
            .WithMany(c => c.Tags)
            .HasForeignKey(vc => vc.ServerId);

        builder.Entity<ClientEmployeeTags>()
            .HasOne(vc => vc.Tag)
            .WithMany(v => v.ClientEmployees)
            .HasForeignKey(vc => vc.TagId);

        builder.Entity<ClientEmployeeTags>()
            .HasOne(vc => vc.Employee)
            .WithMany(c => c.Tags)
            .HasForeignKey(vc => vc.EmployeeId);

        builder.Entity<ClientTags>()
            .HasOne(vc => vc.Tag)
            .WithMany(v => v.Clients)
            .HasForeignKey(vc => vc.TagId);

        builder.Entity<ClientTags>()
            .HasOne(vc => vc.Client)
            .WithMany(c => c.Tags)
            .HasForeignKey(vc => vc.ClientId);

        builder.Entity<ProviderCategories>()
            .HasOne(vc => vc.Provider)
            .WithMany(v => v.Categories)
            .HasForeignKey(vc => vc.ProviderId);

        builder.Entity<ProviderCategories>()
            .HasOne(vc => vc.Category)
            .WithMany(c => c.Providers)
            .HasForeignKey(vc => vc.CategoryId);

        builder.Entity<ProviderContact>()
            .HasOne(vc => vc.Provider)
            .WithMany(v => v.Contacts)
            .HasForeignKey(vc => vc.ProviderId);

        builder.Entity<ProviderContact>()
            .HasOne(vc => vc.Contact)
            .WithMany(c => c.Providers)
            .HasForeignKey(vc => vc.ContactId);

        builder.Entity<VendorContact>()
            .HasOne(vc => vc.Vendor)
            .WithMany(v => v.Contacts)
            .HasForeignKey(vc => vc.VendorId);

        builder.Entity<VendorContact>()
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