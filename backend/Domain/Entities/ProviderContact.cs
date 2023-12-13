using Domain.Common;
namespace Domain.Entities;

public class ProviderContact : BaseEntityWithoutId {
    public Guid ProviderId { get; set; }
    public Providers Provider { get; set; }
    public Guid ContactId { get; set; }
    public Contacts Contact { get; set; }
}