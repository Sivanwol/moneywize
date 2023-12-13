using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
namespace Domain.Entities;

public class VendorsMetaData : BaseEntity {
    [ForeignKey("Vendor")]
    public Guid VendorId { get; set; }

    public Vendors Vendor { get; set; }
    public string Key { get; set; }
    public string Description { get; set; }
    public string Value { get; set; }
}