using Domain.Entities;
using Domain.Persistence.Mock.Services;
using Domain.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MockQueryable.Moq;
using Moq;
using Test.Shared.Common;
namespace Domain.Persistence.Test.Repositories;

[TestFixture, Description("Test the CountriesRepository")]
[Category("Users")]
public class ApplicationUserRepositoryTest : BaseTest {
    [SetUp]
    public void Setup() {
        SetupTest("CountriesRepositoryTest");
        var serviceProvider = new ServiceCollection()
            .AddLogging()
            .BuildServiceProvider();

        factory = serviceProvider.GetService<ILoggerFactory>()!;
        logger = factory.CreateLogger<CountriesRepository>();
    }
    protected ILoggerFactory factory;
    protected ILogger logger;

    [Test, Description("Test the SaveDisplayLanguage method")]
    public async Task SaveDisplayLanguage() {
        var newLanguage = new MockLanguagesServices().GetOne();
        var mockUser = new MockAppUserServices();
        var user = mockUser.GetOne(Guid.NewGuid());
        var mockEnumerableUser = new List<ApplicationUser> {
            user
        };
        var mockObjUser = mockEnumerableUser.AsQueryable().BuildMockDbSet();
        Context.Setup(x => x.Users).Returns(mockObjUser.Object);
        Context.Setup(x => x.Languages).Returns(new List<Languages> {
            newLanguage
        }.AsQueryable().BuildMockDbSet().Object);
        Context.Setup(c => c.Instance.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
        var languagesRepository = new LanguagesRepository(Context.Object, factory);
        var countriesRepository = new CountriesRepository(Context.Object, factory);
        var repository = new AppUserRepository(Context.Object, countriesRepository, languagesRepository);
        await repository.SaveDisplayLanguage(Guid.Parse(user.Id), newLanguage.Id);
        Context.Verify(x => x.Instance.SaveChangesAsync(It.IsAny<CancellationToken>()));
    }

    [Test, Description("Test the GetById method")]
    public async Task GetById() {
        var mockUser = new MockAppUserServices();
        var user = mockUser.GetOne(Guid.NewGuid());
        var mockEnumerableUser = new List<ApplicationUser> {
            user
        };
        var mockObjUser = mockEnumerableUser.AsQueryable().BuildMockDbSet();
        Context.Setup(x => x.Users).Returns(mockObjUser.Object);
        var languagesRepository = new LanguagesRepository(Context.Object, factory);
        var countriesRepository = new CountriesRepository(Context.Object, factory);
        var repository = new AppUserRepository(Context.Object, countriesRepository, languagesRepository);
        var result = await repository.GetById(Guid.Parse(user.Id));
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() => {
            Assert.That(result!.Id, Is.EqualTo(user.Id));
            Assert.That(result.Token, Is.EqualTo(user.Token));
            Assert.That(result.Email, Is.EqualTo(user.Email));
            Assert.That(result.PhoneNumber, Is.EqualTo(user.PhoneNumber));
            Assert.That(result.Country.Id, Is.EqualTo(user.Country.Id));
        });
    }

    [Test, Description("Test the GetUserByToken method")]
    public async Task GetUserByToken() {
        var mockUser = new MockAppUserServices();
        var user = mockUser.GetOne(Guid.NewGuid());
        var mockEnumerableUser = new List<ApplicationUser> {
            user
        };
        var mockObjUser = mockEnumerableUser.AsQueryable().BuildMockDbSet();
        Context.Setup(x => x.Users).Returns(mockObjUser.Object);
        var languagesRepository = new LanguagesRepository(Context.Object, factory);
        var countriesRepository = new CountriesRepository(Context.Object, factory);
        var repository = new AppUserRepository(Context.Object, countriesRepository, languagesRepository);
        var result = await repository.GetUserByToken(user.Token);
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() => {
            Assert.That(result!.Id, Is.EqualTo(user.Id));
            Assert.That(result.Token, Is.EqualTo(user.Token));
            Assert.That(result.Email, Is.EqualTo(user.Email));
            Assert.That(result.PhoneNumber, Is.EqualTo(user.PhoneNumber));
            Assert.That(result.Country.Id, Is.EqualTo(user.Country.Id));
        });
    }
    [Test, Description("Test the LocateUserByPhone method")]
    public async Task LocateUserByPhone() {
        var mockUser = new MockAppUserServices();
        var user = mockUser.GetOne(Guid.NewGuid());
        var mockEnumerableUser = new List<ApplicationUser> {
            user
        };
        var mockObjUser = mockEnumerableUser.AsQueryable().BuildMockDbSet();
        Context.Setup(x => x.Users).Returns(mockObjUser.Object);
        var languagesRepository = new LanguagesRepository(Context.Object, factory);
        var countriesRepository = new CountriesRepository(Context.Object, factory);
        var repository = new AppUserRepository(Context.Object, countriesRepository, languagesRepository);
        var result = await repository.LocateUserByPhone(user.Country.Id, user.PhoneNumber!);
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() => {
            Assert.That(result!.Id, Is.EqualTo(user.Id));
            Assert.That(result.Token, Is.EqualTo(user.Token));
            Assert.That(result.Email, Is.EqualTo(user.Email));
            Assert.That(result.PhoneNumber, Is.EqualTo(user.PhoneNumber));
            Assert.That(result.Country.Id, Is.EqualTo(user.Country.Id));
        });
    }
}