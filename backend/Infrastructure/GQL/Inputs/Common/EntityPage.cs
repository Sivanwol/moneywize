using Infrastructure.Enums;
namespace Infrastructure.GQL.Inputs.Common;

public class EntityPage {
    public int Page { get; set; } = 1;
    public PageSize PageSize { get; set; }
    public string? SortByField { get; set; } = "CreatedAt";
    public SortDirection? SortDirection { get; set; } = Enums.SortDirection.Descending;
    public ICollection<EntityFilterItem> Filters { get; set; } = new List<EntityFilterItem>();
}