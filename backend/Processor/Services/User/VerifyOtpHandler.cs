using System.Security.Claims;
using Application.Configs.Providers;
using Application.Exceptions;
using Application.Utils;
using Domain.Persistence.Repositories.Interfaces;
using Domain.Requests.Processor.Services.User;
using Infrastructure.Enums;
using Infrastructure.Responses.Controllers.Auth;
using Infrastructure.Services.Auth;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog;
namespace Processor.Services.User;

public class VerifyOtpHandler : IRequestHandler<VerifyOtpUserRequest, VerifyOTPResponse> {
    private readonly IAppUserRepository _appUserRepository;
    private readonly ICountriesRepository _countriesRepository;
    private readonly InfoUSmsConfig _infoUSmsConfig;
    private readonly IJwtAuthManager _jwtAuthManager;
    private readonly ILoggerFactory _loggerFactory;
    private readonly IMediator _mediator;
    private readonly IOTPRepository _otpRepository;

    public VerifyOtpHandler(IMediator mediator,
        ILoggerFactory logger,
        IJwtAuthManager jwtAuthManager,
        IAppUserRepository appUserRepository,
        ICountriesRepository countriesRepository,
        IOTPRepository otpRepository,
        InfoUSmsConfig infoUSmsConfig) {
        _otpRepository = otpRepository;
        _appUserRepository = appUserRepository;
        _countriesRepository = countriesRepository;
        _infoUSmsConfig = infoUSmsConfig;
        _jwtAuthManager = jwtAuthManager;
        _loggerFactory = logger;
        _mediator = mediator;
    }

    public async Task<VerifyOTPResponse> Handle(VerifyOtpUserRequest request, CancellationToken cancellationToken) {
        Log.Logger.Information($"VerifyOtpHandler: [{request.OTPToken}, {request.UserToken}]");
        var user = await _appUserRepository.GetUserByToken(request.UserToken);
        if (user == null) {
            throw new EntityNotFoundException("User", request.UserToken);
        }

        Log.Logger.Information($"Locating User By Phone: {user.CountryId}-{user.PhoneNumber}");
        var country = await _countriesRepository.GetById(user.CountryId);
        if (country == null) {
            throw new EntityNotFoundException("Countries", user.CountryId.ToString());
        }
        Log.Logger.Information($"Loading Sms Provider: {country.Provider}");
        var expiredCodeInMinutes = 0;
        switch (country.Provider) {
            case SMSProviders.INFOU:
                expiredCodeInMinutes = _infoUSmsConfig.ExpiredInMinutes;
                break;
            default:
                throw new MFAProviderImplementedException(Enum.GetName(typeof(SMSProviders), country.Provider)!);
        }
        Log.Logger.Information($"Checkinf if OTP Entity Exist for User: [{user.Id}, {request.OTPToken}]");
        var otpEntity = await _appUserRepository.IsExistingOtpEntity(request.UserToken, request.OTPToken, expiredCodeInMinutes);
        if (otpEntity == null) {
            throw new EntityNotFoundException("UserOtpCodes", request.UserToken);
        }
        Log.Logger.Information($"Verifying OTP Code for User: [{user.Id}, {request.OTPToken}]");
        var result = await _otpRepository.VerifyOTP(user, otpEntity, request.Code);
        var userId = new Guid(user.Id);
        if (result.IsValid) {
            Log.Logger.Information($"Clearing OTP Codes for User: [{userId}]");
            await _appUserRepository.ClearOtpCodes(userId);
        }
        return new VerifyOTPResponse {
            UserId = result.IsValid ? userId : null,
            Tokens = result.IsValid ? _jwtAuthManager.GenerateTokens(request.UserToken, new List<Claim>(), SystemClock.Now().DateTime) : null
        };
    }
}