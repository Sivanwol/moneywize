using System.Security.Claims;
using Application.Configs;
using Application.Configs.Providers;
using Application.Exceptions;
using Application.Utils;
using Domain.Entities;
using Domain.Persistence.Repositories.Interfaces;
using Domain.Requests.Processor.Services.User;
using Infrastructure.Responses.Controllers.Auth;
using Infrastructure.Services.Auth;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog;
namespace Processor.Services.User;

public class VerifyOtpHandler : IRequestHandler<VerifyOtpUserRequest, VerifyOTPResponse> {
    private readonly IAppUserRepository _appUserRepository;
    private readonly BackendApplicationConfig _config;
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
        BackendApplicationConfig config,
        InfoUSmsConfig infoUSmsConfig) {
        _otpRepository = otpRepository;
        _appUserRepository = appUserRepository;
        _countriesRepository = countriesRepository;
        _infoUSmsConfig = infoUSmsConfig;
        _jwtAuthManager = jwtAuthManager;
        _loggerFactory = logger;
        _mediator = mediator;
        _config = config;
    }

    public async Task<VerifyOTPResponse> Handle(VerifyOtpUserRequest request, CancellationToken cancellationToken) {
        ApplicationUser user = await GetValidUserByToken(request.UserToken);
        Countries country = await GetValidCountryByUser(user);
        int expiredCodeInMinutes = _config.OTPCodeExpiredInMinutes;
        var otpEntity = await GetOtpEntity(request, expiredCodeInMinutes, user);
        var result = await _otpRepository.VerifyOTP(user, otpEntity, request.Code);

        Guid? userId = result.IsValid ? new Guid(user.Id) : null;
        if (result.IsValid) {
            Log.Logger.Information($"Clearing OTP Codes for User: [{userId}]");
            await _appUserRepository.ClearOtpCodes(userId.Value);
        }

        return new VerifyOTPResponse {
            UserId = userId,
            Tokens = result.IsValid
                ? _jwtAuthManager.GenerateTokens(request.UserToken, new List<Claim>(), SystemClock.Now().DateTime)
                : null
        };
    }

    private async Task<ApplicationUser> GetValidUserByToken(string userToken) {
        Log.Logger.Information($"VerifyOtpHandler: [{userToken}]");
        var user = await _appUserRepository.GetUserByToken(userToken);
        if (user == null) {
            throw new EntityNotFoundException("User", userToken);
        }
        return user;
    }

    private async Task<Countries> GetValidCountryByUser(ApplicationUser user) {
        Log.Logger.Information($"Locating User By Phone: {user.Country.Id}-{user.PhoneNumber}");
        var country = await _countriesRepository.GetById(user.Country.Id);
        if (country == null) {
            throw new EntityNotFoundException("Countries", user.Country.Id.ToString());
        }
        return country;
    }

    private async Task<ApplicationUserOtpCodes> GetOtpEntity(VerifyOtpUserRequest request, int expiredCodeInMinutes, ApplicationUser user) {
        Log.Logger.Information($"Checking if OTP Entity Exist for User: [{user.Id}, {request.OTPToken}]");
        var otpEntity = await _appUserRepository.IsExistingOtpEntity(request.UserToken, request.OTPToken, expiredCodeInMinutes);
        if (otpEntity == null) {
            throw new EntityNotFoundException("UserOtpCodes", request.UserToken);
        }
        return otpEntity;
    }
}