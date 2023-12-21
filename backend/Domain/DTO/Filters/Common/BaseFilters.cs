using System.Linq.Expressions;
using Domain.DTO.Filters.Fields;
using LinqKit;
namespace Domain.DTO.Filters.Common;

public abstract class BaseFilters : IFilter {
    public abstract IQueryable<T> ApplyFilters<T>(IQueryable<T> query, FilterCollectionOperation filterCollectionOperation) where T : class;
    public abstract void ResetFilters();
    protected Expression<Func<T, bool>> ApplyFilterToQuery<T, TCast, TFilter>(ICollection<TFilter> filterList )
        where T : class where TFilter : IFilterField<TCast> {
        if (!filterList.Any())
            return null;
        var predicate = PredicateBuilder.New<T>(false);
        return filterList.Where(filter => !filter.IsFilterDisabled).Aggregate(predicate, (current, filter) => current.Or(filter.ApplyFilter<T>()));
    }
}