using Domain.Common;
namespace Domain.Entities;

public class ClientServerHasTags : BaseEntityWithoutId {
    public int ServerId { get; set; }
    public ClientServers Server { get; set; }
    public int TagId { get; set; }
    public Tags Tag { get; set; }
}