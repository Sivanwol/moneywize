using System.ComponentModel.DataAnnotations.Schema;
using Application.Queries;
using Domain.Common;
namespace Domain.Entities;

public class ClientServersHasAssets : BaseEntity, ISoftDeletable {
    [ForeignKey("ClientServer")]
    public Guid ClientServerId { get; set; }

    public ClientServers Server { get; set; }

    [ForeignKey("Asset")]
    public Guid AssetId { get; set; }

    public Assets Asset { get; set; }

    public DateTime? DisabledAt { get; set; }
    public DateTime? DeletedAt { get; }
}