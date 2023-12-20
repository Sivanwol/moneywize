using Domain.DTO.Filters;
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
        await repository.AddActivity(Guid.Parse(activityLog.UserId), activityLog.EntityId, activityLog.EntityType, activityLog.OperationType, activityLog.Activity,
            activityLog.Details,
            activityLog.Status);
        var result = await repository.GetById(activityLog.Id);
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() => {
            Assert.That(result!.Id, Is.EqualTo(activityLog.Id));
            Assert.That(result!.UserId, Is.EqualTo(activityLog.UserId));
            Assert.That(result!.EntityId, Is.EqualTo(activityLog.EntityId));
            Assert.That(result!.EntityType, Is.EqualTo(activityLog.EntityType));
            Assert.That(result!.OperationType, Is.EqualTo(activityLog.OperationType));
            Assert.That(result!.Activity, Is.EqualTo(activityLog.Activity));
            Assert.That(result!.Details, Is.EqualTo(activityLog.Details));
            Assert.That(result!.Status, Is.EqualTo(activityLog.Status));
        });
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
        await repository.AddActivity(Guid.Parse(activityLog.UserId), activityLog.EntityId, activityLog.EntityType, activityLog.OperationType, activityLog.Activity,
            activityLog.Details,
            activityLog.Status, activityLog.IpAddress, activityLog.UserAgent);
        var result = await repository.GetById(activityLog.Id);
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() => {
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
        });
    }


    [Test, Description("Get Total amount of pages")]
    [TestCase(10, 1)]
    [TestCase(20, 2)]
    [TestCase(30, 3)]
    public async Task GetActivitiesTotalPages(int totalEntries, int totalPages) {
        var mock = new MockActivityLogService();
        var mockData = mock.GetMany(totalEntries);
        var repository = new ActivityLogRepository(Context.Object);
        var mockEnumerable = mockData.ToList();
        var mockObj = mockEnumerable.AsQueryable().BuildMockDbSet();
        Context.Setup(x => x.ActivityLogs).Returns(mockObj.Object);
        var result = await repository.GetActivitiesTotalPages(new RecordFilterPagination<ActivityLogFilters>());
        Assert.That(result, Is.EqualTo(totalPages));
    }

    [Test, Description("Get Total amount of entries")]
    [TestCase(10)]
    [TestCase(20)]
    [TestCase(30)]
    public async Task GetActivitiesTotalEntities(int totalEntries) {
        var mock = new MockActivityLogService();
        var mockData = mock.GetMany(totalEntries);
        var repository = new ActivityLogRepository(Context.Object);
        var mockEnumerable = mockData.ToList();
        var mockObj = mockEnumerable.AsQueryable().BuildMockDbSet();
        Context.Setup(x => x.ActivityLogs).Returns(mockObj.Object);
        var result = await repository.GetActivitiesCount(new RecordFilterPagination<ActivityLogFilters>());
        Assert.That(result, Is.EqualTo(totalEntries));
    }

    [Test, Description("Get Activities without filters and pagination")]
    public async Task GetActivitiesWithoutFilltersAndPagination() {
        var mock = new MockActivityLogService();
        var mockData = mock.GetMany(); // Use a fixed number of entries for testing
        var repository = new ActivityLogRepository(Context.Object);

        var mockEnumerable = mockData.ToList();
        var mockObj = mockEnumerable.AsQueryable().BuildMockDbSet();
        Context.Setup(x => x.ActivityLogs).Returns(mockObj.Object);

        var result = await repository.GetActivities(new RecordFilterPagination<ActivityLogFilters>());

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(mockData.Count)); // Should return all entries

        foreach (var item in result) {
            Assert.Multiple(() => {
                Assert.That(mockData.Any(x => x.Id == item.Id), Is.True);
                Assert.That(mockData.Any(x => x.UserId == item.UserId), Is.True);
                Assert.That(mockData.Any(x => x.EntityId == item.EntityId), Is.True);
                Assert.That(mockData.Any(x => x.EntityType == item.EntityType), Is.True);
                Assert.That(mockData.Any(x => x.OperationType == item.OperationType), Is.True);
                Assert.That(mockData.Any(x => x.Activity == item.Activity), Is.True);
                Assert.That(mockData.Any(x => x.Details == item.Details), Is.True);
                Assert.That(mockData.Any(x => x.Status == item.Status), Is.True);
                Assert.That(mockData.Any(x => x.IpAddress == item.IpAddress), Is.True);
                Assert.That(mockData.Any(x => x.UserAgent == item.UserAgent), Is.True);
            });
        }
    }

    [Test, Description("Get Activities without filters and with pagination")]
    [TestCase(10, 10)]
    [TestCase(20, 20)]
    [TestCase(30, 30)]
    [TestCase(30, 10)]
    public async Task GetActivitiesWithoutFilltersAndWithPagination(int totalEntries, int pageSize) {

        var mock = new MockActivityLogService();
        var mockData = mock.GetMany(totalEntries); // Use a fixed number of entries for testing
        var repository = new ActivityLogRepository(Context.Object);

        var mockEnumerable = mockData.ToList();
        var mockObj = mockEnumerable.AsQueryable().BuildMockDbSet();
        Context.Setup(x => x.ActivityLogs).Returns(mockObj.Object);

        var result = await repository.GetActivities(new RecordFilterPagination<ActivityLogFilters>() {
            PageSize = pageSize
        });

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(pageSize)); // Should return all entries

        foreach (var item in result) {
            Assert.Multiple(() => {
                Assert.That(mockData.Any(x => x.Id == item.Id), Is.True);
                Assert.That(mockData.Any(x => x.UserId == item.UserId), Is.True);
                Assert.That(mockData.Any(x => x.EntityId == item.EntityId), Is.True);
                Assert.That(mockData.Any(x => x.EntityType == item.EntityType), Is.True);
                Assert.That(mockData.Any(x => x.OperationType == item.OperationType), Is.True);
                Assert.That(mockData.Any(x => x.Activity == item.Activity), Is.True);
                Assert.That(mockData.Any(x => x.Details == item.Details), Is.True);
                Assert.That(mockData.Any(x => x.Status == item.Status), Is.True);
                Assert.That(mockData.Any(x => x.IpAddress == item.IpAddress), Is.True);
                Assert.That(mockData.Any(x => x.UserAgent == item.UserAgent), Is.True);
            });
        }
    }
    [Test, Description("Get Activities without filters and with pagination Navigation")]
    [TestCase(30, 1)]
    [TestCase(30, 2)]
    [TestCase(30, 3)]
    public async Task GetActivitiesWithoutFilltersAndWithPaginationNavigation(int totalEntities, int page) {

        var mock = new MockActivityLogService();
        var mockData = mock.GetMany(totalEntities); // Use a fixed number of entries for testing
        var repository = new ActivityLogRepository(Context.Object);

        var pageSize = 10;
        var mockEnumerable = mockData.ToList();
        var mockObj = mockEnumerable.AsQueryable().BuildMockDbSet();
        Context.Setup(x => x.ActivityLogs).Returns(mockObj.Object);

        var result = await repository.GetActivities(new RecordFilterPagination<ActivityLogFilters>() {
            Page = page,
            PageSize = pageSize
        });

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(pageSize)); // Should return all entries

        foreach (var item in result) {
            Assert.Multiple(() => {
                Assert.That(mockData.Any(x => x.Id == item.Id), Is.True);
                Assert.That(mockData.Any(x => x.UserId == item.UserId), Is.True);
                Assert.That(mockData.Any(x => x.EntityId == item.EntityId), Is.True);
                Assert.That(mockData.Any(x => x.EntityType == item.EntityType), Is.True);
                Assert.That(mockData.Any(x => x.OperationType == item.OperationType), Is.True);
                Assert.That(mockData.Any(x => x.Activity == item.Activity), Is.True);
                Assert.That(mockData.Any(x => x.Details == item.Details), Is.True);
                Assert.That(mockData.Any(x => x.Status == item.Status), Is.True);
                Assert.That(mockData.Any(x => x.IpAddress == item.IpAddress), Is.True);
                Assert.That(mockData.Any(x => x.UserAgent == item.UserAgent), Is.True);
            });
        }
    }
}