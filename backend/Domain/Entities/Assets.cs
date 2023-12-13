using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Application.Queries;
using Domain.Common;
namespace Domain.Entities;

public enum AssetType {
    DESKTOP,
    LAPTOP,
    SERVER,
    SWITCH,
    ROUTER,
    PRINTER,
    MONITOR,
    PHONE,
    TABLET,
    OTHER
}

public enum AssetStatus {
    UNASSIGNED,
    ASSIGNED,
    MAINTENANCE,
    DAMAGED,
    LOST,
    RETIRED
}

public class Assets : BaseEntity, ISoftDeletable {
    [ForeignKey("Client")]
    public Guid ClientId { get; set; }

    public Clients Client { get; set; }

    [ForeignKey("Vendor")]
    public Guid VendorId { get; set; }

    public Vendors Vendor { get; set; }

    [ForeignKey("Provider")]
    public Guid ProviderId { get; set; }

    public Providers Provider { get; set; }

    [StringLength(100)]
    public string Label { get; set; }

    [Column(TypeName = "text")]
    public string Notes { get; set; }

    [StringLength(250)]
    public string Model { get; set; }

    [StringLength(100)]
    public string SubModel { get; set; }

    [StringLength(100)]
    public string ServiceTag { get; set; }

    [ForeignKey("ServiceTagMedia")]
    public Guid ServiceTagMediaId { get; set; }

    public Media ServiceTagMedia { get; set; }

    [ForeignKey("ExtraMedia")]
    public Guid ExtraMediaId { get; set; }

    public Media ExtraMedia { get; set; }

    [StringLength(20)]
    public string? Cpu { get; set; }

    [StringLength(20)]
    public string? Memory { get; set; }

    [StringLength(20)]
    public string? StorageHD1 { get; set; }

    [StringLength(20)]
    public string? StorageHD2 { get; set; }

    [StringLength(20)]
    public string? StorageHD3 { get; set; }

    [StringLength(20)]
    public string? StorageHD4 { get; set; }

    [StringLength(20)]
    public string? StorageSSD1 { get; set; }

    [StringLength(20)]
    public string? StorageSSD2 { get; set; }

    [StringLength(20)]
    public string? StorageSSD3 { get; set; }

    [StringLength(20)]
    public string? StorageSSD4 { get; set; }

    public AssetType Type { get; set; }

    [DefaultValue(AssetStatus.UNASSIGNED)]
    public AssetStatus Status { get; set; }

    public DateTime? DeletedAt { get; }
}