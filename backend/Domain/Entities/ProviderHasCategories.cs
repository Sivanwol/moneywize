using Domain.Common;
namespace Domain.Entities;

public class ProviderHasCategories : BaseEntityWithoutId {
    public int ProviderId { get; set; }
    public Providers Provider { get; set; }

    public int CategoryId { get; set; }
    public Categories Category { get; set; }
}