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
    private readonly IAppUserRepository _appUserRepository;
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
        IAppUserRepository appUserRepository,
        ICountriesRepository countriesRepository,
        InfoUSmsConfig infoUSmsConfig) {
        _otpRepository = otpRepository;
        _countriesRepository = countriesRepository;
        _appUserRepository = appUserRepository;
        _logger = logger.CreateLogger<RequestOtpHandler>();
        _mediator = mediator;
        _loggerFactory = logger;
        _infoUSmsConfig = infoUSmsConfig;
    }

    public async Task<RequestOTPResponse> Handle(SendOTPRequest request, CancellationToken cancellationToken) {
        _logger.LogInformation($"RequestOtpHandler: [{request.Provider},{request.CountryId}, {request.PhoneNumber}]");
        _logger.LogInformation($"Start Prep for Sending SMS OTP");
        _logger.LogInformation($"Locating User By Phone: {request.CountryId}-{request.PhoneNumber}");
        ValidateProvider(request.Provider);
        var user = await LocateUser(request);
        LoadProvider(user.Country.Provider);
        if (request.ReSend) {
            return await HandleResend(user, request);
        } else {
            return await SendOTP(user, request);
        }
    }


    private void ValidateProvider(MFAProvider provider) {
        if (provider != MFAProvider.SMS)
            throw new MFAProviderImplementedException(Enum.GetName(typeof(MFAProvider), provider)!);
    }

    private async Task<ApplicationUser> LocateUser(SendOTPRequest request) {
        var user = await _appUserRepository.LocateUserByPhone(request.CountryId, request.PhoneNumber);
        if (user == null)
            throw new EntityNotFoundException("User", $"{request.CountryId}-{request.PhoneNumber}");

        return user;
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

    private async Task<RequestOTPResponse> HandleResend(ApplicationUser user, SendOTPRequest request) {
        _logger.LogInformation($"Verify OTP Entity for user {user.Id} and check if user will able to resend");
        var totalCodesSent = await _appUserRepository.GetTotalOtpCodesWithinExpirationDate(new Guid(user.Id), _infoUSmsConfig.ExpiredInMinutes);

        if (totalCodesSent >= _infoUSmsConfig.TotalRetriesWithinSession) {
            _logger.LogInformation($"User {user.Id} has reached max resend limit");
            return await FetchOtpResponse(user);
        }

        return await SendOTP(user, request);
    }

    private async Task<RequestOTPResponse> SendOTP(ApplicationUser user, SendOTPRequest request) {
        var forceResent = request.ReSend && await _appUserRepository.IsAllowToResendOtpCode(user, _infoUSmsConfig.ExpiredInMinutes, _infoUSmsConfig.TotalRetriesWithinSession);
        var otpEntity = await _appUserRepository.GenerateOtpCode(user, _infoUSmsConfig.ExpiredInMinutes, forceResent, request.Provider);
        if (otpEntity == null) {
            _logger.LogInformation($"User {user.Id} has within the expiration date and won't able to sent or been reached max resend limit");
            return await FetchOtpResponse(user);
        }
        _logger.LogInformation($"OTP Entity for user {user.Id} has been generated otp entity id {otpEntity.Id}");
        var result = await _otpRepository.RequestOTP(user.Country, user, otpEntity, request.Provider);
        _logger.LogInformation($"Sendt OTP for user {user.Id}");
        return new RequestOTPResponse {
            UserToken = result.UserToken,
            OTPExpired = result.OTPExpired,
            OTPToken = result.OTPToken,
            HasSent = true
        };
    }

    private async Task<RequestOTPResponse> FetchOtpResponse(ApplicationUser user) {
        var otpResult = await _appUserRepository.FetchOTP(user);
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