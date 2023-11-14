using Domain.Entities;
using Domain.Entities.User;
using Domain.Persistence.Interfaces.Mock;
using Domain.Persistence.Mock.Configs;
namespace Domain.Persistence.Mock.Services;

public class MockAppUserServices : IAppUserMockService {
    public MockAppUserServices() {
        Faker = new AppUserMockConfig();
    }
    private AppUserMockConfig Faker { get; set; }

    public UserProfile GetUserProfile(int id) {
        var user = Faker.Generate(1).First();
        user.Id = id.ToString();
        return new UserProfile {
            User = user,
        };
    }
    public ApplicationUser GetOne(int id) {
        var user = Faker.Generate(1).First();
        user.Id = id.ToString();
        return user;
    }
    public ApplicationUser GetOneByToken(string userToken) {
        var user = Faker.Generate(1).First();
        user.Token = userToken;
        return user;
    }
}