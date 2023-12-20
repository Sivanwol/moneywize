namespace Domain.DTO.Filters.Common;

public interface IFilter {
    public abstract IQueryable<T> ApplyFilters<T>(IQueryable<T> query) where T : class;
    public abstract void ResetFilters();
    public abstract TFilterField CreateFilter<TFilterField>(string entityName) where TFilterField : class;
    public abstract TFilterField CreateFilter<TFilterField>(string entityName, bool isDisabled) where TFilterField : class;
}