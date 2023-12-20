namespace Domain.DTO.Filters.Common;

public interface IFilter {
    public IQueryable<T> ApplyFilters<T>(IQueryable<T> query) where T : class;
    public void ResetFilters();
}