using Application.Constraints;
using Application.Contract;
using Application.Utils;
using Domain.DTO.OTP;
using Domain.DTO.User;
using Domain.Entities;
using Domain.Persistence.Context;
using Domain.Persistence.Repositories.Common;
using Domain.Persistence.Repositories.Interfaces;
using Infrastructure.Enums;
using Infrastructure.Services.Email;
using Infrastructure.Utils.SMS;
using Microsoft.Extensions.Logging;
namespace Domain.Persistence.Repositories;

public class OTPRepository : BaseRepository, IOTPRepository {
    private readonly ILogger _logger;
    private readonly IMailService mailService;
    private readonly ITemplateService templateService;
    public OTPRepository(
        IDomainContext context,
        ILoggerFactory logger,
        ITemplateService templateService,
        IMailService mailService
    ) : base(context) {
        _logger = logger.CreateLogger<OTPRepository>();
        this.templateService = templateService;
        this.mailService = mailService;
    }
    public async Task<RequestOTPResponseData> RequestOTPForgetPassword(string homeUrl, string homeImageUrl, Countries country, ApplicationUser user,
        ApplicationUserOtpCodes codeEntity,
        MFAProvider provider, CancellationToken cancellationToken = default) {
        _logger.LogInformation($"Requesting OTP for user {user.Id}-{codeEntity.Id} that forget password");
        if (provider == MFAProvider.Email) {
            _logger.LogInformation("Sending OTP via email");
            var template = await templateService.GetTemplateHtmlAsStringAsync(EmailTemplates.UserForgotPassword, new SentOTPCodeMail {
                Code = codeEntity.Code,
                UserToken = user.Token,
                OTPToken = codeEntity.Token,
                Title = "OTP Code",
                HomePageUrl = homeUrl,
                HostImageUrl = homeImageUrl
            });
            var mail = new MailData(new List<string> {
                user.Email!
            }, "OTP Code", template);
            await mailService.SendAsync(mail, cancellationToken);
        } else {
            _logger.LogInformation("Sending OTP via SMS");
            var smsSender = SMSSender.Instance;
            var code = codeEntity.Code;
            smsSender.Init(SMSProviders.INFOU, country.CountryNumber, user.PhoneNumber!, code);
            smsSender.SendSMS(code);
        }
        var res = new RequestOTPResponseData {
            UserToken = user.Token,
            OTPToken = codeEntity.Token,
            OTPExpired = codeEntity.ExpirationDate,
        };
        return res;
    }
    public async Task<RequestOTPResponseData> RequestOTP(string homeUrl, string homeImageUrl, Countries country, ApplicationUser user, ApplicationUserOtpCodes codeEntity,
        MFAProvider provider, CancellationToken cancellationToken = default) {
        _logger.LogInformation($"Requesting OTP for user {user.Id}-{codeEntity.Id}");
        if (provider == MFAProvider.Email) {
            _logger.LogInformation("Sending OTP via email");
            var template = await templateService.GetTemplateHtmlAsStringAsync(EmailTemplates.UserSentOTPCode, new SentOTPCodeMail {
                Code = codeEntity.Code,
                UserToken = user.Token,
                OTPToken = codeEntity.Token,
                Title = "OTP Code",
                HomePageUrl = homeUrl,
                HostImageUrl = homeImageUrl
            });
            var mail = new MailData(new List<string> {
                user.Email!
            }, "OTP Code", template);
            await mailService.SendAsync(mail, cancellationToken);
        } else {
            _logger.LogInformation("Sending OTP via SMS");
            var smsSender = SMSSender.Instance;
            var code = codeEntity.Code;
            smsSender.Init(SMSProviders.INFOU, country.CountryNumber, user.PhoneNumber!, code);
            smsSender.SendSMS(code);
        }
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