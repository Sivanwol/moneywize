using System.Linq.Expressions;
using Domain.DTO.Filters.Common;
namespace Domain.DTO.Filters;

public enum SortDirection {
    Ascending,
    Descending
}

public class RecordFilterPagination<TFilter> where TFilter : IFilter {
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public TFilter? Filter { get; set; }
    public SortDirection SortDirection { get; set; } = SortDirection.Descending;
    public string SortByField { get; set; } = "CreatedAt";

    public IQueryable<T> ApplyQueryWithoutPagination<T>(IQueryable<T> query) where T : class {
        if (Filter != null) {
            query = Filter!.ApplyFilters(query);
        }
        var type = typeof(T);
        var property = type.GetProperty(SortByField);
        var parameter = Expression.Parameter(type, "p");
        var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        var orderByExp = Expression.Lambda(propertyAccess, parameter);
        var typeArguments = new Type[] {
            type, property.PropertyType
        };

        if (SortDirection == SortDirection.Ascending) {
            var orderByCall = Expression.Call(
                typeof(Queryable),
                "OrderBy",
                typeArguments,
                query.Expression,
                Expression.Quote(orderByExp));

            query = query.Provider.CreateQuery<T>(orderByCall);
        } else {
            var orderByDescendingCall = Expression.Call(
                typeof(Queryable),
                "OrderByDescending",
                typeArguments,
                query.Expression,
                Expression.Quote(orderByExp));

            query = query.Provider.CreateQuery<T>(orderByDescendingCall);
        }

        return query;
    }

    public IQueryable<T> ApplyQuery<T>(IQueryable<T> query) where T : class {
        if (Filter != null) {
            query = Filter!.ApplyFilters(query);
        }
        var type = typeof(T);
        var property = type.GetProperty(SortByField);
        var parameter = Expression.Parameter(type, "p");
        var propertyAccess = Expression.MakeMemberAccess(parameter, property!);
        var orderByExp = Expression.Lambda(propertyAccess, parameter);
        var typeArguments = new Type[] {
            type, property.PropertyType
        };

        if (SortDirection == SortDirection.Ascending) {
            var orderByCall = Expression.Call(
                typeof(Queryable),
                "OrderBy",
                typeArguments,
                query.Expression,
                Expression.Quote(orderByExp));

            query = query.Provider.CreateQuery<T>(orderByCall);
        } else {
            var orderByDescendingCall = Expression.Call(
                typeof(Queryable),
                "OrderByDescending",
                typeArguments,
                query.Expression,
                Expression.Quote(orderByExp));

            query = query.Provider.CreateQuery<T>(orderByDescendingCall);
        }

        return query.Skip((Page - 1) * PageSize).Take(PageSize);
    }
}