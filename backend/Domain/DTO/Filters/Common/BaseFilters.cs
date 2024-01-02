using System.Linq.Expressions;
using Domain.DTO.Filters.Fields;
using Infrastructure.Enums;
using LinqKit;
namespace Domain.DTO.Filters.Common;

public abstract class BaseFilters : IFilter {
    public abstract IQueryable<T> ApplyFilters<T>(IQueryable<T> query, FilterCollectionOperation filterCollectionOperation) where T : class;
    public abstract void ResetFilters();
    protected Expression<Func<T, bool>> ApplyFilterToQuery<T, TCast, TFilter>(ICollection<TFilter> filterList)
        where T : class where TFilter : IFilterField<TCast> {
        var predicate = PredicateBuilder.New<T>(false);
        predicate = filterList.Where(filter => !filter.IsFilterDisabled).Aggregate(predicate, (current, filter) => current.Or(filter.ApplyFilter<T>()));
        return predicate;
    }

    protected Expression<Func<T, bool>> ApplyFiltersPredicate<T, TCast, TFilter>(ICollection<TFilter> filters, Expression<Func<T, bool>> predicate,
        Func<Expression<Func<T, bool>>, Expression<Func<T, bool>>, Expression<Func<T, bool>>> operatorFunc)
        where T : class where TFilter : IFilterField<TCast> {
        if (filters.Any(filter => !filter.IsFilterDisabled)) {
            predicate = operatorFunc(predicate, ApplyFilterToQuery<T, TCast, TFilter>(filters));
        }
        return predicate;
    }
}