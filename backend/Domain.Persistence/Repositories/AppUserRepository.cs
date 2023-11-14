using Application.Exceptions;
using Domain.Entities;
using Domain.Entities.User;
using Domain.Persistence.Context;
using Domain.Persistence.Interfaces.Mock;
using Domain.Persistence.Interfaces.Repositories;
using Domain.Persistence.Repositories.Common;
using Serilog;
namespace Domain.Persistence.Repositories;

public class AppUserRepository : BaseRepository, IAppUserRepository {
    private readonly ICountriesRepository _countriesRepository;
    private readonly ILanguagesRepository _languagesRepository;
    private readonly IAppUserMockService _mock;
    public AppUserRepository(
        IDomainContext context,
        ICountriesRepository countriesRepository,
        ILanguagesRepository languagesRepository,
        IAppUserMockService mock
    ) : base(context) {
        _mock = mock;
        _countriesRepository = countriesRepository;
        _languagesRepository = languagesRepository;
    }

    public async Task DeleteUser(int userId) {
        Log.Logger.Information($"Delete new user with id {userId}");
        var user = await GetById(userId);
        if (user == null) {
            throw new EntityNotFoundException("User", userId.ToString());
        }

    }
    public async Task ExportUser(int userId) {
        Log.Logger.Information($"Export new user with id {userId}");
        var user = await GetById(userId);
        if (user == null) {
            throw new EntityNotFoundException("User", userId.ToString());
        }
    }

    public Task<UserProfile> GetUserProfile(int id) {
        Log.Logger.Information($"Fetching user with id {id}");
        return Task.FromResult(_mock.GetUserProfile(id));
    }

    public Task<ApplicationUser> GetUserByToken(string userToken) {
        Log.Logger.Information($"Fetching user with token {userToken}");
        return Task.FromResult(_mock.GetOneByToken(userToken));
    }
    public Task<ApplicationUser> GetById(int id) {
        Log.Logger.Information($"Fetching user with id {id}");
        return Task.FromResult(_mock.GetOne(id));
    }
}