using Application.Configs;
using Application.Configs.Providers;
using Application.Exceptions;
using Application.Utils;
using Domain.Entities;
using Domain.Persistence.Repositories.Interfaces;
using Infrastructure.Enums;
using Infrastructure.Requests.Processor.Services.User;
using Infrastructure.Responses.Controllers.Auth;
using Infrastructure.Utils.SMS;
using MediatR;
using Microsoft.Extensions.Logging;
namespace Processor.Services.User;

public class RequestOtpHandler : IRequestHandler<SendOTPRequest, RequestOTPResponse> {
    private readonly IActivityLogRepository _activityLogRepository;
    private readonly IApplicationUserRepository _applicationUserRepository;
    private readonly BackendApplicationConfig _config;
    private readonly ICountriesRepository _countriesRepository;
    private readonly InfoUSmsConfig _infoUSmsConfig;
    private readonly ILogger _logger;
    private readonly ILoggerFactory _loggerFactory;
    private readonly IMediator _mediator;
    private readonly IOTPRepository _otpRepository;

    public RequestOtpHandler(
        IMediator mediator,
        ILoggerFactory logger,
        IOTPRepository otpRepository,
        IApplicationUserRepository applicationUserRepository,
        IActivityLogRepository activityLogRepository,
        ICountriesRepository countriesRepository,
        BackendApplicationConfig config,
        InfoUSmsConfig infoUSmsConfig) {
        _otpRepository = otpRepository;
        _countriesRepository = countriesRepository;
        _applicationUserRepository = applicationUserRepository;
        _logger = logger.CreateLogger<RequestOtpHandler>();
        _mediator = mediator;
        _loggerFactory = logger;
        _activityLogRepository = activityLogRepository;
        _infoUSmsConfig = infoUSmsConfig;
        _config = config;
    }

    public async Task<RequestOTPResponse> Handle(SendOTPRequest request, CancellationToken cancellationToken) {
        _logger.LogInformation($"RequestOtpHandler: [{request.Provider},{request.CountryId}, {request.PhoneNumber}]");
        _logger.LogInformation("Start Prep for Sending SMS OTP");
        _logger.LogInformation($"Locating User By Phone: {request.CountryId}-{request.PhoneNumber}");
        if (IsIdentifyByPhoneNumber(request)) {
            var country = await _countriesRepository.GetById(request.CountryId!.Value);
            if (country == null)
                throw new EntityNotFoundException("Country", request.CountryId!.Value.ToString());
        }
        var user = await LocateUser(request);
        LoadProvider(user.Country.Provider);
        if (request.ReSend) {
            return await HandleResend(user, request);
        }
        return await SendOTP(user, request);
    }

    private bool IsIdentifyByEmailAddress(SendOTPRequest request) {
        return !string.IsNullOrEmpty(request.Email) && request.CountryId == null && string.IsNullOrEmpty(request.PhoneNumber);
    }

    private bool IsIdentifyByPhoneNumber(SendOTPRequest request) {
        return !string.IsNullOrEmpty(request.PhoneNumber) && request.CountryId != null && string.IsNullOrEmpty(request.Email);
    }

    private void LoadProvider(SMSProviders provider) {
        _logger.LogInformation($"Loading Sms Provider: {provider}");
        switch (provider) {
            case SMSProviders.INFOU:
                SMSSender.Instance.Load(_loggerFactory, _infoUSmsConfig);
                break;
            default:
                throw new MFAProviderImplementedException(Enum.GetName(typeof(SMSProviders), provider)!);
        }
    }

    private async Task<ApplicationUser> LocateUser(SendOTPRequest request) {
        ApplicationUser? user = null;
        if (IsIdentifyByEmailAddress(request)) {
            user = await _applicationUserRepository.LocateUserByEmail(request.Email!);
        } else if (IsIdentifyByPhoneNumber(request)) {
            user = await _applicationUserRepository.LocateUserByPhone(request.CountryId!.Value, request.PhoneNumber!);
        }
        if (user == null)
            throw new EntityNotFoundException("User", $"{request.CountryId}-{request.PhoneNumber}");

        return user;
    }


    private async Task<RequestOTPResponse> HandleResend(ApplicationUser user, SendOTPRequest request) {
        _logger.LogInformation($"Verify OTP Entity for user {user.Id} and check if user will able to resend");
        var totalCodesSent = await _applicationUserRepository.GetTotalOtpCodesWithinExpirationDate(new Guid(user.Id), _config.OTPCodeTotalRetriesWithinSession);

        if (totalCodesSent >= _config.OTPCodeTotalRetriesWithinSession) {
            _logger.LogInformation($"User {user.Id} has reached max resend limit");
            return await FetchOtpResponse(user);
        }

        return await SendOTP(user, request);
    }

    private async Task<RequestOTPResponse> SendOTP(ApplicationUser user, SendOTPRequest request) {
        var forceResent = request.ReSend &&
                          await _applicationUserRepository.IsAllowToResendOtpCode(user, _infoUSmsConfig.ExpiredInMinutes, _infoUSmsConfig.TotalRetriesWithinSession);
        var otpEntity = await _applicationUserRepository.GenerateOtpCode(user, _infoUSmsConfig.ExpiredInMinutes, forceResent, request.Provider);
        if (otpEntity == null) {
            _logger.LogInformation($"User {user.Id} has within the expiration date and won't able to sent or been reached max resend limit");
            await _activityLogRepository.AddActivity(Guid.Parse(user.Id), "", nameof(ApplicationUserOtpCodes), "Send OTP Code to User",
                $"Send OTP COde for the user from {request.IpAddress} under user agent {request.UserAgent}", "Sending code pass checks", "Failed");
            return await FetchOtpResponse(user);
        }
        _logger.LogInformation($"OTP Entity for user {user.Id} has been generated otp entity id {otpEntity.Id}");
        var result = await _otpRepository.RequestOTP(request.BaseUrl, request.BaseUrl + "/assets", user.Country, user, otpEntity, request.Provider);
        _logger.LogInformation($"Sendt OTP for user {user.Id}");
        await _activityLogRepository.AddActivity(Guid.Parse(user.Id), otpEntity.Id.ToString(), nameof(ApplicationUserOtpCodes), "Send OTP Code to User",
            $"Send OTP COde for the user from {request.IpAddress} under user agent {request.UserAgent}", "Sending code pass checks", "Success");
        return new RequestOTPResponse {
            UserToken = result.UserToken,
            OTPExpired = result.OTPExpired,
            OTPToken = result.OTPToken,
            HasSent = true
        };
    }

    private async Task<RequestOTPResponse> FetchOtpResponse(ApplicationUser user) {
        var otpResult = await _applicationUserRepository.FetchOTP(user);
        if (otpResult != null) {
            return new RequestOTPResponse {
                UserToken = user.Token,
                OTPExpired = otpResult.ExpirationDate,
                OTPToken = otpResult.Token,
                HasSent = false
            };
        }
        return new RequestOTPResponse {
            UserToken = "",
            OTPExpired = SystemClock.Now().DateTime,
            OTPToken = "",
            HasSent = false
        };
    }
}