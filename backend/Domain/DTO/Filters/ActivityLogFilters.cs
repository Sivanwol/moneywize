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
    public ICollection<StringFilterField> UserAgentFilters { get; }
    public ICollection<GuidFilterField> IdFilters { get; }
    public ICollection<GuidFilterField> UserIdFilters { get; }
    public ICollection<GuidFilterField> EntityIdFilters { get; }
    public ICollection<StringFilterField> OperationTypeFilters { get; }
    public ICollection<StringFilterField> ActivityFilters { get; }
    public ICollection<StringFilterField> EntityTypeFilters { get; }
    public ICollection<StringFilterField> StatusFilters { get; }
    public ICollection<DateFilterField> CreatedAtFilters { get; }
    public override IQueryable<T> ApplyFilters<T>(IQueryable<T> query) {
        if (IpAddressFilters.Any()) {
            foreach (var filter in IpAddressFilters) {
                query = filter.ApplyFilter(query);
            }
        }
        if (UserAgentFilters.Any()) {
            foreach (var filter in UserAgentFilters) {
                query = filter.ApplyFilter(query);
            }
        }
        if (IdFilters.Any()) {
            foreach (var filter in IdFilters) {
                query = filter.ApplyFilter(query);
            }
        }
        if (UserIdFilters.Any()) {
            foreach (var filter in UserIdFilters) {
                query = filter.ApplyFilter(query);
            }
        }
        if (EntityIdFilters.Any()) {
            foreach (var filter in EntityIdFilters) {
                query = filter.ApplyFilter(query);
            }
        }
        if (OperationTypeFilters.Any()) {
            foreach (var filter in OperationTypeFilters) {
                query = filter.ApplyFilter(query);
            }
        }
        if (ActivityFilters.Any()) {
            foreach (var filter in ActivityFilters) {
                query = filter.ApplyFilter(query);
            }
        }
        if (EntityTypeFilters.Any()) {
            foreach (var filter in EntityTypeFilters) {
                query = filter.ApplyFilter(query);
            }
        }
        if (StatusFilters.Any()) {
            foreach (var filter in StatusFilters) {
                query = filter.ApplyFilter(query);
            }
        }
        if (CreatedAtFilters.Any()) {
            foreach (var filter in CreatedAtFilters) {
                query = filter.ApplyFilter(query);
            }
        }
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
    public override TFilterField CreateFilter<TFilterField>(string entityName) {
        return typeof(TFilterField) switch {
            Type t when t == typeof(StringFilterField) => new StringFilterField(entityName) as TFilterField,
            Type t when t == typeof(GuidFilterField) => new GuidFilterField(entityName) as TFilterField,
            Type t when t == typeof(DateFilterField) => new DateFilterField(entityName) as TFilterField,
            _ => throw new ArgumentException("Filter type is not supported.")
        };
    }
    public override TFilterField CreateFilter<TFilterField>(string entityName, bool isDisabled) {
        return typeof(TFilterField) switch {
            Type t when t == typeof(StringFilterField) => new StringFilterField(entityName, isDisabled) as TFilterField,
            Type t when t == typeof(GuidFilterField) => new GuidFilterField(entityName, isDisabled) as TFilterField,
            Type t when t == typeof(DateFilterField) => new DateFilterField(entityName, isDisabled) as TFilterField,
            _ => throw new ArgumentException("Filter type is not supported.")
        };
    }
}