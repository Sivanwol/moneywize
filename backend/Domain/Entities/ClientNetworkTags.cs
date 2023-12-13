using Domain.Common;
namespace Domain.Entities;

public class ClientNetworkTags : BaseEntityWithoutId {
    public Guid NetworkId { get; set; }
    public ClientNetworks Network { get; set; }
    public Guid TagId { get; set; }
    public Tags Tag { get; set; }
}