using System.Linq.Expressions;
using Domain.DTO.Filters.Common;
using Domain.DTO.Filters.Fields;
using LinqKit;
namespace Domain.DTO.Filters;

public class ActivityLogFilters : BaseFilters {
    public ActivityLogFilters() {
        IpAddressFilters = new List<StringFilterField>();
        UserAgentFilters = new List<StringFilterField>();
        IdFilters = new List<GuidFilterField>();
        UserIdFilters = new List<GuidFilterField>();
        EntityIdFilters = new List<GuidFilterField>();
        OperationTypeFilters = new List<StringFilterField>();
        ActivityFilters = new List<StringFilterField>();
        EntityTypeFilters = new List<StringFilterField>();
        StatusFilters = new List<StringFilterField>();
        CreatedAtFilters = new List<DateFilterField>();
    }
    public ICollection<StringFilterField> IpAddressFilters { get; }
    public ICollection<StringFilterField> UserAgentFilters { get; }
    public ICollection<GuidFilterField> IdFilters { get; }
    public ICollection<GuidFilterField> UserIdFilters { get; }
    public ICollection<GuidFilterField> EntityIdFilters { get; }
    public ICollection<StringFilterField> OperationTypeFilters { get; }
    public ICollection<StringFilterField> ActivityFilters { get; }
    public ICollection<StringFilterField> EntityTypeFilters { get; }
    public ICollection<StringFilterField> StatusFilters { get; }
    public ICollection<DateFilterField> CreatedAtFilters { get; }

    private bool HasAnyFilter() {
        return IpAddressFilters.Any() ||
               UserAgentFilters.Any() ||
               IdFilters.Any() ||
               UserIdFilters.Any() ||
               EntityIdFilters.Any() ||
               OperationTypeFilters.Any() ||
               ActivityFilters.Any() ||
               EntityTypeFilters.Any() ||
               StatusFilters.Any() ||
               CreatedAtFilters.Any();
    }
    public override IQueryable<T> ApplyFilters<T>(IQueryable<T> query, FilterCollectionOperation filterCollectionOperation) {

        if (!HasAnyFilter()) {
            return query;
        }
        var predicate = PredicateBuilder.New<T>(false);
        Func<Expression<Func<T, bool>>, Expression<Func<T, bool>>, Expression<Func<T, bool>>> operatorFunc = filterCollectionOperation == FilterCollectionOperation.Or
            ? (Func<Expression<Func<T, bool>>, Expression<Func<T, bool>>, Expression<Func<T, bool>>>)((p1, p2) => p1.Or(p2))
            : (p1, p2) => p1.And(p2);

        predicate = ApplyFiltersPredicate<T, string, StringFilterField>(IpAddressFilters, predicate, operatorFunc);
        predicate = ApplyFiltersPredicate<T, string, StringFilterField>(UserAgentFilters, predicate, operatorFunc);
        predicate = ApplyFiltersPredicate<T, Guid, GuidFilterField>(IdFilters, predicate, operatorFunc);
        predicate = ApplyFiltersPredicate<T, Guid, GuidFilterField>(UserIdFilters, predicate, operatorFunc);
        predicate = ApplyFiltersPredicate<T, Guid, GuidFilterField>(EntityIdFilters, predicate, operatorFunc);
        predicate = ApplyFiltersPredicate<T, string, StringFilterField>(OperationTypeFilters, predicate, operatorFunc);
        predicate = ApplyFiltersPredicate<T, string, StringFilterField>(ActivityFilters, predicate, operatorFunc);
        predicate = ApplyFiltersPredicate<T, string, StringFilterField>(EntityTypeFilters, predicate, operatorFunc);
        predicate = ApplyFiltersPredicate<T, string, StringFilterField>(StatusFilters, predicate, operatorFunc);
        predicate = ApplyFiltersPredicate<T, DateTime, DateFilterField>(CreatedAtFilters, predicate, operatorFunc);

        return query.Where(predicate);
    }

    public override void ResetFilters() {
        IpAddressFilters.Clear();
        UserAgentFilters.Clear();
        IdFilters.Clear();
        UserIdFilters.Clear();
        EntityIdFilters.Clear();
        OperationTypeFilters.Clear();
        ActivityFilters.Clear();
        EntityTypeFilters.Clear();
        StatusFilters.Clear();
        CreatedAtFilters.Clear();
    }
}