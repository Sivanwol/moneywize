using Domain.Common;
namespace Domain.Entities;

public class ClientNetworkHasTags : BaseEntityWithoutId {
    public Guid NetworkId { get; set; }
    public ClientNetworks Network { get; set; }
    public Guid TagId { get; set; }
    public Tags Tag { get; set; }
}