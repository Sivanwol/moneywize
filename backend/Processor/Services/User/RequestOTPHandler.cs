using Application.Configs.Providers;
using Application.Exceptions;
using Application.Utils;
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

        var countryData = await _countriesRepository.GetById(request.CountryId);
        if (countryData == null) {
            throw new EntityNotFoundException("Countries", request.CountryId.ToString());
        }
        if (request.Provider != MFAProvider.SMS)
            throw new MFAProviderImplementedException(Enum.GetName(typeof(MFAProvider), request.Provider)!);
        _logger.LogInformation($"Start Prep for Sending SMS OTP");
        _logger.LogInformation($"Locating User By Phone: {request.CountryId}-{request.PhoneNumber}");
        var user = await _appUserRepository.LocateUserByPhone(request.CountryId, request.PhoneNumber);
        if (user == null) {
            throw new EntityNotFoundException("User", $"{request.CountryId}-{request.PhoneNumber}");
        }

        var country = await _countriesRepository.GetById(user.CountryId);
        if (country == null) {
            throw new EntityNotFoundException("Countries", user.CountryId.ToString());
        }
        _logger.LogInformation($"Loading Sms Provider: {country.Provider}");
        switch (country.Provider) {
            case SMSProviders.INFOU:
                SMSSender.Instance.Load(_loggerFactory, _infoUSmsConfig);
                break;
            default:
                throw new MFAProviderImplementedException(Enum.GetName(typeof(SMSProviders), country.Provider)!);
        }
        _logger.LogInformation($"Generating OTP Entity for user {user.Id}");
        if (request.ReSend) {
            _logger.LogInformation($"Verify OTP Entity for user {user.Id} and check if user will able to resend");
            var totalCodesSent = await _appUserRepository.GetTotalOtpCodesWithinExpirationDate(new Guid(user.Id), _infoUSmsConfig.ExpiredInMinutes);
            if (totalCodesSent >= _infoUSmsConfig.TotalRetriesWithinSession) {
                _logger.LogInformation($"User {user.Id} has reached max resend limit");
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
        var forceResent = false;

        if (request.ReSend) {
            var allowResend = await _appUserRepository.IsAllowToResendOtpCode(user, _infoUSmsConfig.ExpiredInMinutes, _infoUSmsConfig.TotalRetriesWithinSession);
            if (allowResend) {
                _logger.LogInformation($"User {user.Id} is force to resend");
                forceResent = true;
            }
        }
        var otpEntity = await _appUserRepository.GenerateOtpCode(user, _infoUSmsConfig.ExpiredInMinutes, forceResent, request.Provider);
        if (otpEntity == null) {
            _logger.LogInformation($"User {user.Id} has within the expiration date and won't able to sent or been reached max resend limit");
            var otpResult = await _appUserRepository.FetchOTP(user);
            if (otpResult == null) {
                throw new OTPNotFoundException(user.Token);
            }
            return new RequestOTPResponse {
                UserToken = user.Token,
                OTPExpired = otpResult.ExpirationDate,
                OTPToken = otpResult.Token,
                HasSent = false
            };

        }
        _logger.LogInformation($"OTP Entity for user {user.Id} has been generated otp entity id {otpEntity.Id}");
        var result = await _otpRepository.RequestOTP(country, user, otpEntity, request.Provider);
        _logger.LogInformation($"Sendt OTP for user {user.Id}");
        return new RequestOTPResponse {
            UserToken = result.UserToken,
            OTPExpired = result.OTPExpired,
            OTPToken = result.OTPToken,
            HasSent = true
        };
    }
}