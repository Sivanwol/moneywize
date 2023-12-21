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

    public override IQueryable<T> ApplyFilters<T>(IQueryable<T> query, FilterCollectionOperation filterCollectionOperation) {
        var predicate = PredicateBuilder.New<T>(false);
        if (filterCollectionOperation == FilterCollectionOperation.Or) {

            query = query.Where(predicate.Or(ApplyFilterToQuery<T, string, StringFilterField>(IpAddressFilters)));
            query = query.Where(predicate.Or(ApplyFilterToQuery<T, string, StringFilterField>(UserAgentFilters)));
            query = query.Where(predicate.Or(ApplyFilterToQuery<T, Guid, GuidFilterField>(IdFilters)));
            query = query.Where(predicate.Or(ApplyFilterToQuery<T, Guid, GuidFilterField>(UserIdFilters)));
            query = query.Where(predicate.Or(ApplyFilterToQuery<T, Guid, GuidFilterField>(EntityIdFilters)));
            query = query.Where(predicate.Or(ApplyFilterToQuery<T, string, StringFilterField>(OperationTypeFilters)));
            query = query.Where(predicate.Or(ApplyFilterToQuery<T, string, StringFilterField>(ActivityFilters)));
            query = query.Where(predicate.Or(ApplyFilterToQuery<T, string, StringFilterField>(EntityTypeFilters)));
            query = query.Where(predicate.Or(ApplyFilterToQuery<T, string, StringFilterField>(StatusFilters)));
            query = query.Where(predicate.Or(ApplyFilterToQuery<T, DateTime, DateFilterField>(CreatedAtFilters)));
            return query;

        }
        query = query.Where(predicate.And(ApplyFilterToQuery<T, string, StringFilterField>(IpAddressFilters)));
        query = query.Where(predicate.And(ApplyFilterToQuery<T, string, StringFilterField>(UserAgentFilters)));
        query = query.Where(predicate.And(ApplyFilterToQuery<T, Guid, GuidFilterField>(IdFilters)));
        query = query.Where(predicate.And(ApplyFilterToQuery<T, Guid, GuidFilterField>(UserIdFilters)));
        query = query.Where(predicate.And(ApplyFilterToQuery<T, Guid, GuidFilterField>(EntityIdFilters)));
        query = query.Where(predicate.And(ApplyFilterToQuery<T, string, StringFilterField>(OperationTypeFilters)));
        query = query.Where(predicate.And(ApplyFilterToQuery<T, string, StringFilterField>(ActivityFilters)));
        query = query.Where(predicate.And(ApplyFilterToQuery<T, string, StringFilterField>(EntityTypeFilters)));
        query = query.Where(predicate.And(ApplyFilterToQuery<T, string, StringFilterField>(StatusFilters)));
        query = query.Where(predicate.And(ApplyFilterToQuery<T, DateTime, DateFilterField>(CreatedAtFilters)));

        return query;
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