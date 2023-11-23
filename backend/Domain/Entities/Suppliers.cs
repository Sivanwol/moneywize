using Domain.Common;
namespace Domain.Entities;

public class Suppliers : BaseEntity {
    public string Name { get; set; }
    public string Description { get; set; }
    public string SiteUtl { get; set; }
    public string Address { get; set; }
    public bool IsEnabled { get; set; }
}