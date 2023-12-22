using System.ComponentModel.DataAnnotations.Schema;
using Application.Queries;
using Domain.Entities.Common;
namespace Domain.Entities;

public class ClientContacts : BaseEntity, ISoftDeletable {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone1 { get; set; }
    public string? Phone2 { get; set; }
    public string? Fax { get; set; }
    public string? whatsapp { get; set; }

    [ForeignKey("Countries")]
    public int CountryId { get; set; }

    public Countries Country { get; set; }
    public string? PostalCode { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string JobTitle { get; set; }
    public string Company { get; set; }

    [Column(TypeName = "text")]
    public string Notes { get; set; }

    public ICollection<VendorHasContact> Vendors { get; set; }
    public ICollection<ProviderHasContact> Providers { get; set; }
    public DateTime? DeletedAt { get; }
}