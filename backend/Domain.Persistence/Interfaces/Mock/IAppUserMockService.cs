using Domain.Entities;
using Domain.Entities.User;
namespace Domain.Persistence.Interfaces.Mock;

public interface IAppUserMockService {
    ApplicationUser GetOne(int id);
    UserProfile GetUserProfile(int id);
    ApplicationUser GetOneByToken(string userToken);
}