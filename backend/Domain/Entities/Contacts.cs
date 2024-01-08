using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Application.Queries;
using Domain.Entities.Common;
namespace Domain.Entities;

public class Contacts : BaseEntity, ISoftDeletable {
    [Column(TypeName = "varchar(75)")]
    public string FirstName { get; set; }

    [Column(TypeName = "varchar(75)")]
    public string LastName { get; set; }

    [MaxLength(500)]
    public string Email { get; set; }

    [Column(TypeName = "varchar(20)")]
    public string? Phone1 { get; set; }

    [Column(TypeName = "varchar(20)")]
    public string? Phone2 { get; set; }

    [Column(TypeName = "varchar(20)")]
    public string? Fax { get; set; }

    [Column(TypeName = "varchar(20)")]
    public string? Whatsapp { get; set; }

    [ForeignKey("Countries")]
    public int CountryId { get; set; }

    public Countries? Country { get; set; }

    [Column(TypeName = "varchar(10)")]
    public string? PostalCode { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string? Address { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string? City { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string? State { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string JobTitle { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string? Department { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string Company { get; set; }

    [Column(TypeName = "text")]
    public string Notes { get; set; }

    public ICollection<VendorHasContact> Vendors { get; set; }
    public ICollection<ProviderHasContact> Providers { get; set; }
    public DateTime? DeletedAt { get; set; }
}