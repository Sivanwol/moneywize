using Domain.Persistence.Mock.Services;
using Domain.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MockQueryable.Moq;
using Test.Shared.Common;
namespace Domain.Persistence.Test.Repositories;

[TestFixture, Description("Test the ApplicationUserRepository")]
[Category("Activitys")]
public class ActivityLogRepositoryTest : BaseTest {

    [SetUp]
    public void Setup() {
        SetupTest("CountriesRepositoryTest");
        var serviceProvider = new ServiceCollection()
            .AddLogging()
            .BuildServiceProvider();

    }

    [Test, Description("Add a new activity log")]
    public async Task AddActivity() {
        var mock = new MockActivityLogService();
        var mockData = mock.GetMany(Guid.NewGuid());
        var repository = new ActivityLogRepository(Context.Object);
        var activityLog = mockData.First();

        var mockEnumerable = mockData.ToList();
        var mockObj = mockEnumerable.AsQueryable().BuildMockDbSet();
        Context.Setup(x => x.ActivityLogs).Returns(mockObj.Object);
        Context.Setup(x => x.Instance.SaveChanges()).Returns(1);
        await repository.AddActivity(activityLog.UserId, activityLog.EntityId, activityLog.EntityType, activityLog.OperationType, activityLog.Activity, activityLog.Details,
            activityLog.Status);
        var result = await repository.GetById(activityLog.Id);
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Id, Is.EqualTo(activityLog.Id));
        Assert.That(result!.UserId, Is.EqualTo(activityLog.UserId));
        Assert.That(result!.EntityId, Is.EqualTo(activityLog.EntityId));
        Assert.That(result!.EntityType, Is.EqualTo(activityLog.EntityType));
        Assert.That(result!.OperationType, Is.EqualTo(activityLog.OperationType));
        Assert.That(result!.Activity, Is.EqualTo(activityLog.Activity));
        Assert.That(result!.Details, Is.EqualTo(activityLog.Details));
        Assert.That(result!.Status, Is.EqualTo(activityLog.Status));
    }

    [Test, Description("Add a new activity log With IP and UserAgent")]
    public async Task AddActivityWithIpAndUserAgent() {
        var mock = new MockActivityLogService();
        var mockData = mock.GetMany(Guid.NewGuid());
        var repository = new ActivityLogRepository(Context.Object);
        var activityLog = mockData.First();

        var mockEnumerable = mockData.ToList();
        var mockObj = mockEnumerable.AsQueryable().BuildMockDbSet();
        Context.Setup(x => x.ActivityLogs).Returns(mockObj.Object);
        Context.Setup(x => x.Instance.SaveChanges()).Returns(1);
        await repository.AddActivity(activityLog.UserId, activityLog.EntityId, activityLog.EntityType, activityLog.OperationType, activityLog.Activity, activityLog.Details,
            activityLog.Status, activityLog.IpAddress, activityLog.UserAgent);
        var result = await repository.GetById(activityLog.Id);
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Id, Is.EqualTo(activityLog.Id));
        Assert.That(result!.UserId, Is.EqualTo(activityLog.UserId));
        Assert.That(result!.EntityId, Is.EqualTo(activityLog.EntityId));
        Assert.That(result!.EntityType, Is.EqualTo(activityLog.EntityType));
        Assert.That(result!.OperationType, Is.EqualTo(activityLog.OperationType));
        Assert.That(result!.Activity, Is.EqualTo(activityLog.Activity));
        Assert.That(result!.Details, Is.EqualTo(activityLog.Details));
        Assert.That(result!.Status, Is.EqualTo(activityLog.Status));
        Assert.That(result!.IpAddress, Is.EqualTo(activityLog.IpAddress));
        Assert.That(result!.UserAgent, Is.EqualTo(activityLog.UserAgent));
    }
}