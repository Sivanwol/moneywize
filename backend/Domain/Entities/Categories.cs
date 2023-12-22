using Application.Queries;
using Domain.Entities.Common;
namespace Domain.Entities;

public class Categories : BaseEntity, ISoftDeletable {

    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<ProviderHasCategories> Providers { get; set; }
    public DateTime? DeletedAt { get; }
}