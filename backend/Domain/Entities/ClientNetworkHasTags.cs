using Domain.Common;
namespace Domain.Entities;

public class ClientNetworkHasTags : BaseEntityWithoutId {
    public int NetworkId { get; set; }
    public ClientNetworks Network { get; set; }
    public int TagId { get; set; }
    public Tags Tag { get; set; }
}