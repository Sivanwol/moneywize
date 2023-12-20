using Domain.DTO.Filters.Common;
using Domain.DTO.Filters.Fields;
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
    public FilterCollectionOperation IpAddressFilterOperation { get; set; } = FilterCollectionOperation.Or;
    public ICollection<StringFilterField> UserAgentFilters { get; }
    public FilterCollectionOperation UserAgentFilterOperation { get; set; } = FilterCollectionOperation.Or;
    public ICollection<GuidFilterField> IdFilters { get; }
    public FilterCollectionOperation IdFilterOperation { get; set; } = FilterCollectionOperation.Or;
    public ICollection<GuidFilterField> UserIdFilters { get; }
    public FilterCollectionOperation UserIdFilterOperation { get; set; } = FilterCollectionOperation.Or;
    public ICollection<GuidFilterField> EntityIdFilters { get; }
    public FilterCollectionOperation EntityIdFilterOperation { get; set; } = FilterCollectionOperation.Or;
    public ICollection<StringFilterField> OperationTypeFilters { get; }
    public FilterCollectionOperation OperationTypeFilterOperation { get; set; } = FilterCollectionOperation.Or;
    public ICollection<StringFilterField> ActivityFilters { get; }
    public FilterCollectionOperation ActivityFilterOperation { get; set; } = FilterCollectionOperation.Or;
    public ICollection<StringFilterField> EntityTypeFilters { get; }
    public FilterCollectionOperation EntityTypeFilterOperation { get; set; } = FilterCollectionOperation.Or;
    public ICollection<StringFilterField> StatusFilters { get; }
    public FilterCollectionOperation StatusFilterOperation { get; set; } = FilterCollectionOperation.Or;
    public ICollection<DateFilterField> CreatedAtFilters { get; }
    public FilterCollectionOperation CreatedAtFilterOperation { get; set; } = FilterCollectionOperation.Or;

    public override IQueryable<T> ApplyFilters<T>(IQueryable<T> query) {
        query = ApplyFilterToQuery<T, string, StringFilterField>(query, IpAddressFilters, IpAddressFilterOperation);
        query = ApplyFilterToQuery<T, string, StringFilterField>(query, UserAgentFilters, UserAgentFilterOperation);
        query = ApplyFilterToQuery<T, Guid, GuidFilterField>(query, IdFilters, IdFilterOperation);
        query = ApplyFilterToQuery<T, Guid, GuidFilterField>(query, UserIdFilters, UserIdFilterOperation);
        query = ApplyFilterToQuery<T, Guid, GuidFilterField>(query, EntityIdFilters, EntityIdFilterOperation);
        query = ApplyFilterToQuery<T, string, StringFilterField>(query, OperationTypeFilters, OperationTypeFilterOperation);
        query = ApplyFilterToQuery<T, string, StringFilterField>(query, ActivityFilters, ActivityFilterOperation);
        query = ApplyFilterToQuery<T, string, StringFilterField>(query, EntityTypeFilters, EntityTypeFilterOperation);
        query = ApplyFilterToQuery<T, string, StringFilterField>(query, StatusFilters, StatusFilterOperation);
        query = ApplyFilterToQuery<T, DateTime, DateFilterField>(query, CreatedAtFilters, CreatedAtFilterOperation);
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