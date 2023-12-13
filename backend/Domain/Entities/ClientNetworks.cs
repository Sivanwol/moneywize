using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Application.Queries;
using Domain.Common;
namespace Domain.Entities;

public class ClientNetworks : BaseEntity, ISoftDeletable {
    [ForeignKey("Client")]
    public Guid ClientId { get; set; }

    public Clients Client { get; set; }

    [ForeignKey("Asset")]
    public Guid AssetId { get; set; }

    public Assets Asset { get; set; }

    [StringLength(100)]
    public string Label { get; set; }

    [StringLength(20)]
    public string Color { get; set; } = "#000000";

    [Column(TypeName = "text")]
    public string Notes { get; set; }

    public DateTime? DisabledAt { get; set; }
    public ICollection<ClientNetworkTags> Tags { get; set; }
    public DateTime? DeletedAt { get; }
}