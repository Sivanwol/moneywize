using System.ComponentModel.DataAnnotations.Schema;
using Application.Queries;
using Domain.Common;
namespace Domain.Entities;

public class ClientEmployees : BaseEntity, ISoftDeletable {
    [ForeignKey("Client")]
    public Guid ClientId { get; set; }

    public Clients Client { get; set; }

    [ForeignKey("Network")]
    public Guid NetworkId { get; set; }

    public ClientNetworks Network { get; set; }

    [ForeignKey("Server")]
    public Guid ServerId { get; set; }

    public ClientServers Server { get; set; }

    [ForeignKey("Asset")]
    public Guid AssetId { get; set; }

    public Assets Asset { get; set; }

    [ForeignKey("ContactEmployee")]
    public Guid ContactEmployeeId { get; set; }

    public Contacts ContactEmployee { get; set; }

    public DateTime? DisabledAt { get; set; }
    public ICollection<ClientEmployeeHasTags> Tags { get; set; }
    public ICollection<ClientEmployeesHasAssets> Assets { get; set; }
    public DateTime? DeletedAt { get; }
}