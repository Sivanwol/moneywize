using System.ComponentModel.DataAnnotations.Schema;
using Application.Queries;
using Domain.Common;
namespace Domain.Entities;

public class ClientEmployeesHasAssets : BaseEntity, ISoftDeletable {
    [ForeignKey("Client")]
    public Guid ClientEmployeeId { get; set; }

    public ClientEmployees Employee { get; set; }

    [ForeignKey("Asset")]
    public Guid AssetId { get; set; }

    public Assets Asset { get; set; }

    [ForeignKey("ContactEmployee")]
    public Guid ContactEmployeeId { get; set; }

    public ClientContacts ContactEmployee { get; set; }

    public DateTime? DisabledAt { get; set; }
    public DateTime? DeletedAt { get; }
}