using Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Domain.Persistence.Context;

public interface IDomainContext : IDisposable {
    DbContext Instance { get; }
    DbSet<Countries> Countries { get; set; }
    DbSet<Languages> Languages { get; set; }
    DbSet<ApplicationUser> Users { get; set; }
    DbSet<ApplicationUserOtpCodes> UserOtpCodes { get; set; }
    DbSet<Vendors> Vendors { get; set; }
    DbSet<VendorsMetaData> VendorsMetaData { get; set; }
    DbSet<Contacts> Contacts { get; set; }
    DbSet<VendorHasContact> VendorContacts { get; set; }
    DbSet<ProviderHasContact> ProviderContacts { get; set; }
    DbSet<Providers> Providers { get; set; }
    DbSet<ProviderHasCategories> ProviderCategories { get; set; }
    DbSet<Categories> Categories { get; set; }
    DbSet<Tags> Tags { get; set; }
    DbSet<ClientServers> ClientServers { get; set; }
    DbSet<ClientNetworks> ClientNetworks { get; set; }
    DbSet<ClientServerHasTags> ClientServerTags { get; set; }
    DbSet<ClientServersHasAssets> ClientServersHasAssets { get; set; }
    DbSet<ClientNetworkHasTags> ClientNetworkTags { get; set; }
    DbSet<ClientNetworksHasAssets> ClientNetworksHasAssets { get; set; }
    DbSet<ActivityLog> ActivityLog { get; set; }
    DbSet<Clients> Clients { get; set; }
    DbSet<Media> Media { get; set; }
    DbSet<Clients> Client { get; set; }
    DbSet<ClientContacts> ClientContacts { get; set; }
    DbSet<ClientsHasClientContacts> ClientHasClientContacts { get; set; }
    DbSet<ClientEmployees> ClientEmployees { get; set; }
    DbSet<ClientEmployeesHasAssets> ClientEmployeesHasAssets { get; set; }
    DbSet<ClientEmployeeHasTags> ClientEmployeeTags { get; set; }
    DbSet<Assets> Assets { get; set; }
}