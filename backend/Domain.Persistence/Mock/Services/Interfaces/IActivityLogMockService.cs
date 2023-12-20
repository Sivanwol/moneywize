using Domain.Entities;
namespace Domain.Persistence.Mock.Services.Interfaces;

public interface IActivityLogMockService {
    ActivityLog GetOne(Guid userId);
    ICollection<ActivityLog> GetMany(Guid userId);
}