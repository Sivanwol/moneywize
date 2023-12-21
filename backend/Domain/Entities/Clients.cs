using System.ComponentModel.DataAnnotations.Schema;
using Application.Queries;
using Domain.Common;
using Elastic.Apm.Api.Constraints;
namespace Domain.Entities;

public class Clients : BaseEntity, ISoftDeletable {
    [ForeignKey("OwnerUser")]
    [MaxLength(36)]
    public string OwnerUserId { get; set; }

    public ApplicationUser OwnerUser { get; set; }

    [ForeignKey("Parent")]
    public int? ParentId { get; set; }

    public Clients? Parent { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Website { get; set; }
    public DateTime? DisabledAt { get; set; }

    [ForeignKey("Countries")]
    public int CountryId { get; set; }

    public Countries Country { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public DateTime? DeletedAt { get; }
}