using System.Linq.Expressions;
using Domain.DTO.ConfigurableEntities;
using Domain.DTO.Filters.Common;
using Domain.DTO.Filters.Fields;
using Domain.Entities;
using Infrastructure.Enums;
using LinqKit;
namespace Domain.DTO.Filters;

public class ApplicationUserFilters : BaseFilters {
    public ApplicationUserFilters() {
        FirstNameFilters = new List<StringFilterField>();
        LastNameFilters = new List<StringFilterField>();
        IdFilters = new List<UserIdFilterField>();
        Addressilters = new List<StringFilterField>();
    }
    public ICollection<UserIdFilterField> IdFilters { get; }
    public ICollection<StringFilterField> FirstNameFilters { get; }
    public ICollection<StringFilterField> LastNameFilters { get; }
    public ICollection<StringFilterField> Addressilters { get; }

    private bool HasAnyFilter() {
        return FirstNameFilters.Any() ||
               LastNameFilters.Any() ||
               IdFilters.Any() ||
               Addressilters.Any();
    }
    public override IQueryable<T> ApplyFilters<T>(IQueryable<T> query, FilterCollectionOperation filterCollectionOperation,
        IEnumerable<ConfigurableUserViewColumn> SelectedColumns) {

        if (!HasAnyFilter()) {
            return query;
        }
        var predicate = PredicateBuilder.New<T>(false);
        Func<Expression<Func<T, bool>>, Expression<Func<T, bool>>, Expression<Func<T, bool>>> operatorFunc = filterCollectionOperation == FilterCollectionOperation.Or
            ? (Func<Expression<Func<T, bool>>, Expression<Func<T, bool>>, Expression<Func<T, bool>>>)((p1, p2) => p1.Or(p2))
            : (p1, p2) => {
                if (p1.ToString() == "f => False" && p2.ToString() != "f => False") {
                    return p2;
                }
                return p1.And(p2);
            };

        if (HasAllowedColumnFilter(SelectedColumns, nameof(ActivityLog.IpAddress))) {
            predicate = ApplyFiltersPredicate<T, string, StringFilterField>(FirstNameFilters, predicate, operatorFunc);
        }
        if (HasAllowedColumnFilter(SelectedColumns, nameof(ActivityLog.UserAgent))) {
            predicate = ApplyFiltersPredicate<T, string, StringFilterField>(LastNameFilters, predicate, operatorFunc);
        }
        if (HasAllowedColumnFilter(SelectedColumns, nameof(ActivityLog.Id))) {
            predicate = ApplyFiltersPredicate<T, string, UserIdFilterField>(IdFilters, predicate, operatorFunc);
        }
        if (HasAllowedColumnFilter(SelectedColumns, nameof(ActivityLog.UserId))) {
            predicate = ApplyFiltersPredicate<T, string, StringFilterField>(Addressilters, predicate, operatorFunc);
        }

        return query.Where(predicate);
    }
    public override void ResetFilters() {
        Addressilters.Clear();
        LastNameFilters.Clear();
        IdFilters.Clear();
        FirstNameFilters.Clear();
    }
}