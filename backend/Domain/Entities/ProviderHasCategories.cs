using Domain.Common;
namespace Domain.Entities;

public class ProviderHasCategories : BaseEntityWithoutId {
    public Guid ProviderId { get; set; }
    public Providers Provider { get; set; }

    public Guid CategoryId { get; set; }
    public Categories Category { get; set; }
}