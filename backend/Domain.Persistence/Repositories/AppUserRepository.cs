using Application.Exceptions;
using Application.Utils;
using Domain.Entities;
using Domain.Persistence.Context;
using Domain.Persistence.Mock.Services.Interfaces;
using Domain.Persistence.Repositories.Common;
using Domain.Persistence.Repositories.Interfaces;
using Infrastructure.Enums;
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


    public async Task SaveDisplayLanguage(Guid userId, int languageId) {
        Log.Logger.Information($"Saving display language for user {userId}-{languageId}");
        var languageData = await _languagesRepository.GetById(languageId);
        if (languageData == null) {
            throw new EntityNotFoundException("Languages", languageId.ToString());
        }
        var user = await GetById(userId);
        if (user == null) {
            throw new EntityNotFoundException("User", userId.ToString());
        }
    }

    public async Task DeleteUser(Guid userId) {
        Log.Logger.Information($"Delete new user with id {userId}");
        var user = await GetById(userId);
        if (user == null) {
            throw new EntityNotFoundException("User", userId.ToString());
        }

    }
    public async Task ExportUser(Guid userId) {
        Log.Logger.Information($"Export new user with id {userId}");
        var user = await GetById(userId);
        if (user == null) {
            throw new EntityNotFoundException("User", userId.ToString());
        }
    }


    public Task<ApplicationUser?> GetById(int id) {
        throw new NotImplementedException();
    }
    public Task<ApplicationUser> GetById(Guid id) {
        Log.Logger.Information($"Fetching user with id {id}");
        return Task.FromResult(_mock.GetOne(id));
    }

    public async Task<ApplicationUser?> GetUserByToken(string userToken) {
        Log.Logger.Information($"Fetching user with token {userToken}");
        return Context.Users.FirstOrDefault(w => w.Token == userToken);
    }

    public async Task<ApplicationUser?> LocateUserByPhone(int countryId, string phoneNumber) {
        Log.Logger.Information($"Locating user with phone number {countryId}-{phoneNumber}");
        var user = Context.Users.Where(w => w.CountryId == countryId && w.PhoneNumber == phoneNumber).First();
        if (user == null) {
            throw new EntityNotFoundException("User", $"{countryId}-{phoneNumber}");
        }
        return user;
    }

    public async Task<ApplicationUserOtpCodes?> GenerateOtpCode(ApplicationUser user, int expiredCodeInMinutes, bool forceSent, MFAProvider provider) {
        Log.Logger.Information($"Generate otp code for user {user.Id}-{provider}");
        if (provider == MFAProvider.Email) return null;
        var totalCodeSent = Context.UserOtpCodes.Count(w =>
            w.UserId.Equals(user.Id) && w.ExpirationDate <= SystemClock.Now().DateTime.AddMinutes(expiredCodeInMinutes) && w.ExpirationDate >= SystemClock.Now().DateTime &&
            w.ComplateAt == null);
        if (totalCodeSent != 0 && !forceSent)
            return null;
        var entity = new ApplicationUserOtpCodes {
            UserId = new Guid(user.Id),
            Token = StringExtensions.GenerateToken(),
            Code = ApplicationUserOtpCodes.GenerateCode(),
            ExpirationDate = SystemClock.Now().DateTime.AddMinutes(expiredCodeInMinutes),
            CreatedAt = SystemClock.Now().DateTime,
            UpdatedAt = SystemClock.Now().DateTime,
        };
        var expiredDate = SystemClock.Now().DateTime.AddMinutes(forceSent ? 0 : expiredCodeInMinutes);
        var entities = Context.UserOtpCodes.Where(w =>
            w.UserId.Equals(user.Id) && w.ExpirationDate <= expiredDate && w.ExpirationDate >= SystemClock.Now().DateTime && w.ComplateAt == null);
        if (entities.Any()) {
            Log.Logger.Information($"Clearing existing otp code for user {user.Id}-{provider}");
            foreach (var item in entities) {
                if (item == null)
                    continue;
                item.ComplateAt = SystemClock.Now().DateTime;
                item.UpdatedAt = SystemClock.Now().DateTime;
            }
        }
        Context.UserOtpCodes.Add(entity);
        await Context.Instance.SaveChangesAsync();
        return entity;
    }

    public async Task<ApplicationUserOtpCodes?> IsExistingOtpEntity(string userToken, string token, int expiredCodeInMinutes) {
        Log.Logger.Information($"Checking if otp code exist for user [{userToken} , {token}]");
        var user = await GetUserByToken(userToken);
        if (user == null) {
            return null;
        }
        var entity = Context.UserOtpCodes.FirstOrDefault(w =>
            w.UserId.Equals(user.Id) && w.Token == token && w.ExpirationDate >= SystemClock.Now().DateTime && w.ExpirationDate <= w.CreatedAt.AddMinutes(expiredCodeInMinutes) &&
            !w.ComplateAt.HasValue);
        return entity;
    }

    public async Task<int> GetTotalOtpCodesWithinExpirationDate(Guid userId, int expiredCodeInMinutes) {
        Log.Logger.Information($"Getting total otp code for user {userId}");
        var query = from w in Context.UserOtpCodes
            where w.UserId == userId &&
                  w.ExpirationDate >= SystemClock.Now().DateTime &&
                  !w.ComplateAt.HasValue
            select w;
        query.Where(w => w.ExpirationDate <= w.CreatedAt.AddMinutes(expiredCodeInMinutes));
        return query.Count();
    }

    public async Task<bool> IsAllowToResendOtpCode(ApplicationUser user, int expiredCodeInMinutes, int maxResendCount) {
        Log.Logger.Information($"Checking if user {user.Id} is allow to resend otp code");
        var totalCodesSent = await GetTotalOtpCodesWithinExpirationDate(new Guid(user.Id), expiredCodeInMinutes);
        return totalCodesSent <= maxResendCount;
    }

    public async Task ClearOtpCodes(Guid userId) {
        Log.Logger.Information($"Clearing otp code for user {userId}");
        var entities = Context.UserOtpCodes.Where(w => w.UserId.Equals(userId) && w.ExpirationDate >= SystemClock.Now().DateTime && !w.ComplateAt.HasValue);
        if (entities.Any()) {
            Log.Logger.Information($"Clearing existing otp code for user {userId} total of {entities.Count()} entities found");
            foreach (var item in entities) {
                if (item == null)
                    continue;
                item.ComplateAt = SystemClock.Now().DateTime;
                item.UpdatedAt = SystemClock.Now().DateTime;
            }
            await Context.Instance.SaveChangesAsync();
        }
    }

    public async Task<ApplicationUserOtpCodes?> FetchOTP(ApplicationUser user) {
        Log.Logger.Information($"Fetching otp code for user {user.Id}");
        return (from w in Context.UserOtpCodes
            where w.UserId.Equals(user.Id) && w.ExpirationDate >= SystemClock.Now().DateTime && !w.ComplateAt.HasValue
            orderby w.ExpirationDate descending
            select w).FirstOrDefault();
    }
}