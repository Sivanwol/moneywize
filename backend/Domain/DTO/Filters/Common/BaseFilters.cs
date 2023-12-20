using Domain.DTO.Filters.Fields;
using LinqKit;
namespace Domain.DTO.Filters.Common;

public enum FilterCollectionOperation {
    Or,
    And
}

public abstract class BaseFilters : IFilter {
    public abstract IQueryable<T> ApplyFilters<T>(IQueryable<T> query) where T : class;
    public abstract void ResetFilters();
    protected IQueryable<T> ApplyFilterToQuery<T, TCast, TFilter>(IQueryable<T> query,
        ICollection<TFilter> filterList, FilterCollectionOperation operation)
        where T : class where TFilter : IFilterField<TCast> {
        if (!filterList.Any())
            return query;
        var predicate = PredicateBuilder.New<T>(false);
        foreach (var filter in filterList) {
            if (!filter.IsFilterDisabled) {
                if (operation == FilterCollectionOperation.Or) {
                    predicate = predicate.Or(filter.ApplyFilter<T>());
                    continue;
                }
                if (operation == FilterCollectionOperation.And) {
                    predicate = predicate.And(filter.ApplyFilter<T>());
                    continue;
                }

                throw new ArgumentException("Invalid filter operation");
            }
        }
        query = query.Where(predicate);

        return query;
    }
}