using Domain.Entities;
using Domain.Entities.User;
namespace Domain.Persistence.Interfaces.Repositories;

public interface IAppUserRepository : IGenericRepository<ApplicationUser> {
    Task DeleteUser(int userId);
    Task ExportUser(int userId);
    Task<UserProfile> GetUserProfile(int id);
    Task<ApplicationUser> GetUserByToken(string userToken);
}