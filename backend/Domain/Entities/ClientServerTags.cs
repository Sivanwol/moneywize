using Domain.Common;
namespace Domain.Entities;

public class ClientServerTags : BaseEntityWithoutId {
    public Guid ServerId { get; set; }
    public ClientServers Server { get; set; }
    public Guid TagId { get; set; }
    public Tags Tag { get; set; }
}