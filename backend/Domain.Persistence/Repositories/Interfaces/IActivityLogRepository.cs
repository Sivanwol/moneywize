using Domain.DTO.Filters;
using Domain.Entities;
using Domain.Persistence.Repositories.Common.Interfaces;
namespace Domain.Persistence.Repositories.Interfaces;

public enum PurgeActivityLogOlderThan {
    TwoWeeks,
    OneMonth,
    ThreeMonths,
    SixMonths
}

public interface IActivityLogRepository : IGenericRepository<ActivityLog> {
    Task AddActivity(Guid? userId, Guid entityId, string entityType, string operationType, string activity, string details, string status);
    Task AddActivity(Guid? userId, Guid entityId, string entityType, string operationType, string activity, string details, string status, string? ipAddress, string? userAgent);
    Task<ICollection<ActivityLog>> GetActivities(RecordFilterPagination<ActivityLogFilters> filter);
    Task<int> GetActivitiesCount(RecordFilterPagination<ActivityLogFilters> filter);
    Task DeleteOldActivities(PurgeActivityLogOlderThan priorToDelete);
}