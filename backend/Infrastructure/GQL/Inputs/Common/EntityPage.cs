using System.ComponentModel;
using GraphQL.AspNet.Attributes;
using Infrastructure.Enums;
namespace Infrastructure.GQL.Inputs.Common;

[GraphType(InputName = "page")]
[Description("Entity page where u defined page, pageSize, sortByField, sortDirection and filters")]
public class EntityPage {
    [Description("Page number")]
    public int Page { get; set; } = 1;

    [Description("Page size")]
    public PageSize PageSize { get; set; } = PageSize.Twenty;

    [Description("Sort by field")]
    public string? SortByField { get; set; } = "CreatedAt";

    [Description("Sort direction")]
    public SortDirection SortDirection { get; set; } = Enums.SortDirection.Descending;

    [Description("Filter collection operation")]
    public FilterCollectionOperation FilterCollectionOperation { get; set; } = FilterCollectionOperation.Or;

    [Description("Filters")]
    public ICollection<EntityFilterItem> Filters { get; set; } = new List<EntityFilterItem>();
}