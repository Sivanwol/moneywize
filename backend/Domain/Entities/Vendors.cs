using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Application.Queries;
using Domain.Common;
namespace Domain.Entities;

public enum VendorSupportResponseType {
    API,
    SCRAPING,
}

[Table("Vendors")]
public class Vendors : BaseEntity, ISoftDeletable {
    [StringLength(100)]
    public string Name { get; set; }

    [Column(TypeName = "text")]
    public string Description { get; set; }

    public Media? Logo { get; set; }
    public string SiteUrl { get; set; }
    public string SuportUrl { get; set; }
    public string Address { get; set; }
    public string? SupportPhone { get; set; }
    public string? SupportEmail { get; set; }
    public VendorSupportResponseType SupportResponseType { get; set; }
    public ICollection<VendorContact> Contacts { get; set; }
    public DateTime? DisabledAt { get; set; }
    public DateTime? DeletedAt { get; }
}