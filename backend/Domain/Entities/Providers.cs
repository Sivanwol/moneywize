using Application.Queries;
using Domain.Common;
namespace Domain.Entities;

public enum ProviderType {
    RANTER,
    SELLER
}

public class Providers : BaseEntity, ISoftDeletable {

    public string Name { get; set; }
    public string Description { get; set; }
    public Media? Logo { get; set; }
    public string SiteUtl { get; set; }
    public string SuportUtl { get; set; }
    public ProviderType ProviderType { get; set; }
    public ICollection<ProviderContact> Contacts { get; set; }
    public ICollection<ProviderCategories> Categories { get; set; }
    public DateTime? DisabledAt { get; set; }
    public DateTime? DeletedAt { get; }
}