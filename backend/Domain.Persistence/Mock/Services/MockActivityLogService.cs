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
        log.UserId = userId;
        return log;
    }
    public ICollection<ActivityLog> GetMany(Guid userId) {
        var logs = Faker.Generate(10);
        foreach (var log in logs) {
            log.UserId = userId;
        }
        return logs;
    }
}