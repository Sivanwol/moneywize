using Application.Queries;
using Domain.Common;
namespace Domain.Entities;

public class Clients : BaseEntity, ISoftDeletable {
    public ApplicationUser Owner { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Media Logo { get; set; }
    public string Website { get; set; }
    public DateTime? DisabledAt { get; set; }

    public ICollection<ClientTags> Tags { get; set; }
    public DateTime? DeletedAt { get; }
}