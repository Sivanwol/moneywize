using Domain.Entities;
using Domain.Persistence.Context;
using Moq;
namespace Test.Shared.Common;

public static class MockTestHelper {
    private static Mock<IDomainContext> Context = null;
    public static List<Countries> Countries { get; set; } = new List<Countries>();

    public static Mock<IDomainContext> SetupContext() {
        var context = SetupEntities();
        Context.Setup(x => x.Instance.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
        return context;
    }

    private static Mock<IDomainContext> SetupEntities() {
        Context = new Mock<IDomainContext>();
        // Context.SetupProperty(c => c.Users, users.AsQueryable().BuildMockDbSet().Object);
        return Context;
    }
}