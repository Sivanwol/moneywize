using Application.Utils;
using Domain.DTO.OTP;
using Domain.Entities;
using Domain.Persistence.Context;
using Domain.Persistence.Repositories.Common;
using Domain.Persistence.Repositories.Interfaces;
using Infrastructure.Enums;
using Infrastructure.Utils.SMS;
using Microsoft.Extensions.Logging;
namespace Domain.Persistence.Repositories;

public class OTPRepository : BaseRepository, IOTPRepository {
    private readonly ILogger _logger;
    public OTPRepository(
        IDomainContext context,
        ILoggerFactory logger
    ) : base(context) {
        _logger = logger.CreateLogger<OTPRepository>();
    }

    public async Task<RequestOTPResponseData> RequestOTP(Countries country, ApplicationUser user, ApplicationUserOtpCodes codeEntity, MFAProvider provider) {
        _logger.LogInformation($"Requesting OTP for user {user.Id}-{codeEntity.Id}");
        SMSSender.Instance.Init(country.Provider, country.CountryCode, user.PhoneNumber, $"Your OTP code is ##code##");
        SMSSender.Instance.SendSMS(codeEntity.Code);
        var res = new RequestOTPResponseData {
            UserToken = user.Token,
            OTPToken = codeEntity.Token,
            OTPExpired = codeEntity.ExpirationDate,
        };
        return res;
    }

    public async Task<VerifyOTPResponseData> VerifyOTP(ApplicationUser user, ApplicationUserOtpCodes codeEntity, string code) {
        _logger.LogInformation($"Verifying OTP for user {user.Id}-{codeEntity.Id}");
        if (codeEntity.Code != code || codeEntity.ComplateAt.HasValue) {
            _logger.LogInformation($"Invalid OTP for user {user.Id}-{codeEntity.Id}");
            return new VerifyOTPResponseData {
                IsValid = false,
                User = null
            };
        }
        if (codeEntity.ExpirationDate < SystemClock.Now().DateTime) {
            _logger.LogInformation($"Invalid OTP for user {user.Id}-{codeEntity.Id}");
            return new VerifyOTPResponseData {
                IsValid = false,
                User = null
            };
        }
        _logger.LogInformation($"Valid OTP for user {user.Id}-{codeEntity.Id}");
        codeEntity.ComplateAt = SystemClock.Now().DateTime;
        await Context.Instance.SaveChangesAsync();
        return new VerifyOTPResponseData {
            IsValid = true,
            User = user
        };
    }
}