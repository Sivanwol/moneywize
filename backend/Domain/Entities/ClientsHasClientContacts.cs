using Domain.Common;
namespace Domain.Entities;

public class ClientsHasClientContacts : BaseEntityWithoutId {
    public int ClientId { get; set; }
    public Clients Client { get; set; }
    public int ContactId { get; set; }
    public ClientContacts Contact { get; set; }
}