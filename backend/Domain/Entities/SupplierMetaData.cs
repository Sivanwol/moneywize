using Domain.Common;
using Infrastructure.Enums;
namespace Domain.Entities;

public class SupplierMetaData : BaseEntity {
    public int SupplierId { get; set; }
    public string ModelInterface { get; set; }
    public SupplierScanningInterface ScanningInterface { get; set; }
    public string Url { get; set; }
    public string ApiToken { get; set; } = default!;
}