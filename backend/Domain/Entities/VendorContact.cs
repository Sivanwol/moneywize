using Domain.Common;
namespace Domain.Entities;

public class VendorContact : BaseEntityWithoutId {
    public Guid VendorId { get; set; }
    public Vendors Vendor { get; set; }
    public Guid ContactId { get; set; }
    public Contacts Contact { get; set; }
}