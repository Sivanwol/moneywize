using Domain.DTO.Filters;
using Domain.Entities;
using Domain.Persistence.Context;
using Domain.Persistence.Repositories.Common;
using Domain.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;
namespace Domain.Persistence.Repositories;

public class ActivityLogRepository : BaseRepository, IActivityLogRepository {

    public ActivityLogRepository(IDomainContext context) : base(context) { }

    public Task<ActivityLog?> GetById(Guid id) {
        Log.Logger.Information($"Fetching activity log with id {id}");
        return Context.ActivityLog.FirstOrDefaultAsync(w => w.Id == id);
    }

    public Task AddActivity(Guid? userId, Guid entityId, string entityType, string operationType, string activity, string details, string status) {
        Log.Logger.Information($"Adding activity log with id {userId}-{entityId}-{entityType}-{operationType}");
        var activityLog = new ActivityLog {
            UserId = userId,
            EntityId = entityId,
            EntityType = entityType,
            OperationType = operationType,
            Activity = activity,
            Details = details,
            Status = status
        };
        Context.ActivityLog.Add(activityLog);
        return Context.Instance.SaveChangesAsync();
    }

    public Task AddActivity(Guid? userId, Guid entityId, string entityType, string operationType, string activity, string details, string status, string? ipAddress,
        string? userAgent) {
        Log.Logger.Information($"Adding activity log with id {userId}-{entityId}-{entityType}-{operationType}");
        var activityLog = new ActivityLog {
            UserId = userId,
            EntityId = entityId,
            EntityType = entityType,
            OperationType = operationType,
            Activity = activity,
            Details = details,
            Status = status,
            IpAddress = ipAddress,
            UserAgent = userAgent
        };
        Context.ActivityLog.Add(activityLog);
        return Context.Instance.SaveChangesAsync();
    }

    public async Task<ICollection<ActivityLog>> GetActivities(RecordFilterPagination<ActivityLogFilters> filter) {
        Log.Logger.Information($"Fetching activity log with filter {filter}");
        var query = Context.ActivityLog.AsQueryable();
        return filter.ApplyQuery(query).ToList();
    }

    public async Task<int> GetActivitiesCount(RecordFilterPagination<ActivityLogFilters> filter) {
        Log.Logger.Information($"Fetching activity log count with filter {filter}");
        var query = Context.ActivityLog.AsQueryable();
        return filter.ApplyQuery(query).Count();
    }

    public Task DeleteOldActivities(PurgeActivityLogOlderThan priorToDelete) {
        Log.Logger.Information($"Deleting old activity log with filter {priorToDelete}");
        var query = Context.ActivityLog.AsQueryable();
        switch (priorToDelete) {
            case PurgeActivityLogOlderThan.TwoWeeks:
                query = query.Where(w => w.CreatedAt < DateTime.Now.AddDays(-14));
                break;
            case PurgeActivityLogOlderThan.OneMonth:
                query = query.Where(w => w.CreatedAt < DateTime.Now.AddMonths(-1));
                break;
            case PurgeActivityLogOlderThan.ThreeMonths:
                query = query.Where(w => w.CreatedAt < DateTime.Now.AddMonths(-3));
                break;
            case PurgeActivityLogOlderThan.SixMonths:
                query = query.Where(w => w.CreatedAt < DateTime.Now.AddMonths(-6));
                break;
        }
        return query.ForEachAsync(w => Context.ActivityLog.Remove(w));
    }
}