using Domain.Common;
namespace Domain.Entities;

public class ClientTags : BaseEntityWithoutId {
    public Guid ClientId { get; set; }
    public Clients Client { get; set; }
    public Guid TagId { get; set; }
    public Tags Tag { get; set; }
}