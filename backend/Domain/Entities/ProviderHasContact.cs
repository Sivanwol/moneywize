using Domain.Common;
namespace Domain.Entities;

public class ProviderHasContact : BaseEntityWithoutId {
    public int ProviderId { get; set; }
    public Providers Provider { get; set; }
    public int ContactId { get; set; }
    public Contacts Contact { get; set; }
}