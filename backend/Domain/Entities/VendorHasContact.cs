using Domain.Common;
namespace Domain.Entities;

public class VendorHasContact : BaseEntityWithoutId {
    public int VendorId { get; set; }
    public Vendors Vendor { get; set; }
    public int ContactId { get; set; }
    public Contacts Contact { get; set; }
}