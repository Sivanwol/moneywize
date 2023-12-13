using Application.Queries;
using Domain.Common;
namespace Domain.Entities;

public class Categories : BaseEntity, ISoftDeletable {

    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<ProviderCategories> Providers { get; set; }
    public DateTime? DeletedAt { get; }
}