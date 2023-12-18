using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Application.Queries;
using Domain.Common;
namespace Domain.Entities;

public class ClientNetworksHasAssets : BaseEntity, ISoftDeletable {
    [ForeignKey("Client")]
    public Guid ClientNetworkId { get; set; }

    public ClientNetworks Network { get; set; }

    [ForeignKey("Asset")]
    public Guid AssetId { get; set; }

    public Assets Asset { get; set; }


    [StringLength(100)]
    public string Label { get; set; }

    [Column(TypeName = "text")]
    public string Notes { get; set; }

    public DateTime? DisabledAt { get; set; }
    public DateTime? DeletedAt { get; }
}