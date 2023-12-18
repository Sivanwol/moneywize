using Domain.Common;
namespace Domain.Entities;

public class ClientsHasClientContacts : BaseEntityWithoutId {
    public Guid ClientId { get; set; }
    public Clients Client { get; set; }
    public Guid ContactId { get; set; }
    public ClientContacts Contact { get; set; }
}