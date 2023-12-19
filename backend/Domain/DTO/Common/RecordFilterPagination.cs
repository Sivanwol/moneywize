namespace Domain.DTO.Common;

public enum SortDirection {
    Ascending,
    Descending
}

public class RecordFilterPagination<TFilter> where TFilter : class {
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public ICollection<TFilter?> Filter { get; set; }
    public SortDirection SortDirection { get; set; } = SortDirection.Descending;
    public string SortByField { get; set; } = "CreatedAt";
}