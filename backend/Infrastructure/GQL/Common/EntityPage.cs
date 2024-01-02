namespace Infrastructure.GQL.Common;

public class EntityPage<T> where T : class {
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }
    public bool HasNextPage { get; set; }
    public bool HasPreviousPage { get; set; }
    public IEnumerable<T> Records { get; set; } = new List<T>();
}