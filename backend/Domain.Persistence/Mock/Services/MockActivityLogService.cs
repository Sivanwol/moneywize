using Domain.Entities;
using Domain.Persistence.Mock.Configs;
using Domain.Persistence.Mock.Services.Interfaces;
namespace Domain.Persistence.Mock.Services;

public class MockActivityLogService : IActivityLogMockService {

    public MockActivityLogService() {
        Faker = new ActivityLogMockConfig();
    }
    private ActivityLogMockConfig Faker { get; set; }
    public ActivityLog GetOne(Guid userId) {
        var log = Faker.Generate(1).First();
        if (log.OperationType != nameof(ActivityOperationType.SYSTEM)) {
            log.UserId = userId.ToString();
        }
        return log;
    }
    public ICollection<ActivityLog> GetMany(int totalEntries = 10) {
        return Faker.Generate(totalEntries);
    }
    public ICollection<ActivityLog> GetMany(Guid userId, int totalEntries = 10) {
        var logs = Faker.Generate(totalEntries);
        foreach (var log in logs) {
            if (log.OperationType != nameof(ActivityOperationType.SYSTEM)) {
                log.UserId = userId.ToString();
            }
        }
        return logs;
    }
}