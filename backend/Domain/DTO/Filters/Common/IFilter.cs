namespace Domain.DTO.Filters.Common;

public interface IFilter {
    public IQueryable<T> ApplyFilters<T>(IQueryable<T> query, FilterCollectionOperation filterCollectionOperation) where T : class;
    public void ResetFilters();
}