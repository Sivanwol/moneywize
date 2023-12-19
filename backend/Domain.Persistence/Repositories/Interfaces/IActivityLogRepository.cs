using Domain.Entities;
using Domain.Persistence.Repositories.Common.Interfaces;
namespace Domain.Persistence.Repositories.Interfaces;

public interface IActivityLogRepository : IGenericRepository<ActivityLog> {
    Task AddActivity(Guid? userId, Guid entityId, string entityType, string operationType, string activity, string details, string status);
    Task AddActivity(Guid? userId, Guid entityId, string entityType, string operationType, string activity, string details, string status, string? ipAddress, string? userAgent);
    Task<ICollection<ActivityLog>> GetActivitiesByUserId(Guid userId);
}