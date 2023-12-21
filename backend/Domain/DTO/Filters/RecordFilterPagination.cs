using System.Linq.Expressions;
using Domain.DTO.Filters.Common;
using Microsoft.EntityFrameworkCore;
using Serilog;
namespace Domain.DTO.Filters;

public enum SortDirection {
    Ascending,
    Descending
}

public enum FilterCollectionOperation {
    Or,
    And
}

public class RecordFilterPagination<TFilter> where TFilter : IFilter {

    public RecordFilterPagination() {
        Filter = (TFilter)Activator.CreateInstance(typeof(TFilter));
    }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public TFilter Filter { get; set; }
    public FilterCollectionOperation FilterCollectionOperation { get; set; } = FilterCollectionOperation.Or;
    public SortDirection SortDirection { get; set; } = SortDirection.Descending;
    public string SortByField { get; set; } = "CreatedAt";

    public IQueryable<T> ApplyQueryWithoutPagination<T>(IQueryable<T> query) where T : class {
        query = Filter.ApplyFilters(query, FilterCollectionOperation);
        var type = typeof(T);
        var property = type.GetProperty(SortByField);
        var parameter = Expression.Parameter(type, "p");
        var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        var orderByExp = Expression.Lambda(propertyAccess, parameter);
        var typeArguments = new[] {
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
        Log.Information("Query without pagination");
        Log.Information("Filter: {@Filter}", Filter);
        Log.Verbose(query.ToQueryString());
        return query;
    }

    public IQueryable<T> ApplyQuery<T>(IQueryable<T> query) where T : class {
        query = Filter.ApplyFilters(query, FilterCollectionOperation);
        var type = typeof(T);
        var property = type.GetProperty(SortByField);
        var parameter = Expression.Parameter(type, "p");
        var propertyAccess = Expression.MakeMemberAccess(parameter, property!);
        var orderByExp = Expression.Lambda(propertyAccess, parameter);
        var typeArguments = new[] {
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

        query = query.Skip((Page - 1) * PageSize).Take(PageSize);
        Log.Information("Query with pagination");
        Log.Information("Page: {Page}, PageSize: {PageSize}", Page, PageSize);
        Log.Information("SortDirection: {SortDirection}, SortByField: {SortByField}", SortDirection, SortByField);
        Log.Information("Filter: {@Filter}", Filter);
        return query;
    }
}