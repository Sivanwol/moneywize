using System.Net;
using Application.Configs;
using Application.Exceptions;
using Application.Responses.Base;
using Application.Utils;
using Application.Utils.Service;
using Domain.Entities;
using Domain.Requests.Processor.Services.User;
using FluentValidation;
using FluentValidation.Results;
using IdentityModel.Client;
using Infrastructure.Enums;
using Infrastructure.Requests.Controllers.Auth;
using Infrastructure.Requests.Processor.Services.Countries;
using Infrastructure.Requests.Processor.Services.User;
using Infrastructure.Responses.Controllers.Auth;
using Infrastructure.Responses.Controllers.Countries;
using Infrastructure.Services.Auth;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
namespace BackendApi.Controllers.v1.Auth;

[ApiVersion("1.0")]
[Authorize]
public class AuthController : BaseApiController {
    private readonly IValidator<ForgetPasswordRequest> _forgetPasswordValidator;
    private readonly IJwtAuthManager _jwtAuthManager;
    private readonly ILogger _logger;
    private readonly IValidator<LoginRequest> _loginValidator;
    private readonly IValidator<RefreshTokenRequest> _refreshTokenValidator;
    private readonly IValidator<RegisterNewUserRequest> _registerNewUserValidator;
    private readonly IValidator<ResetPasswordRequest> _resetPasswordValidator;
    private readonly IValidator<SendCodeToProviderRequest> _sendCodeToProviderValidator;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IValidator<VerifyFromProviderRequest> _verifyFromProviderValidator;
    private readonly IValidator<VerifyOtpUserRequest> _verifyOtpUserValidator;

    public AuthController(
        IMediator mediator,
        BackendApplicationConfig configuration,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ILoggerFactory loggerFactory,
        IJwtAuthManager jwtAuthManager,
        IValidator<VerifyOtpUserRequest> verifyOtpUserValidator,
        IValidator<ResetPasswordRequest> resetPasswordValidator,
        IValidator<ForgetPasswordRequest> forgetPasswordValidator,
        IValidator<LoginRequest> loginValidator,
        IValidator<RefreshTokenRequest> refreshTokenValidator,
        IValidator<VerifyFromProviderRequest> verifyFromProviderValidator,
        IValidator<SendCodeToProviderRequest> sendCodeToProviderValidator,
        IValidator<RegisterNewUserRequest> registerNewUserValidator) : base(mediator, configuration) {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = loggerFactory.CreateLogger<AuthController>();
        _jwtAuthManager = jwtAuthManager;
        _registerNewUserValidator = registerNewUserValidator;
        _forgetPasswordValidator = forgetPasswordValidator;
        _verifyOtpUserValidator = verifyOtpUserValidator;
        _resetPasswordValidator = resetPasswordValidator;
        _loginValidator = loginValidator;
        _sendCodeToProviderValidator = sendCodeToProviderValidator;
        _refreshTokenValidator = refreshTokenValidator;
        _verifyFromProviderValidator = verifyFromProviderValidator;
    }

    /// <summary>
    /// will be called when user request to login
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("login/web")]
    [SwaggerOperation(Tags = new[] {
        "Auth"
    })]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginRequest request) {
        var validator = await _loginValidator.ValidateAsync(request);
        if (!validator.IsValid) {
            return ResponseHelper.CreateEmptyResponse(HandlerErrorResponse(validator), HttpStatusCode.BadRequest);
        }

        try {
            var response = await Mediator.Send(new LoginWebUserRequest {
                Email = request.Email,
                Password = request.Password,
                RememberMe = request.RememberMe
            });
            return ResponseHelper.CreateResponse(response);
        }
        catch (UserNotFoundException e) {
            return ResponseHelper.CreateEmptyResponse(["User not found or password is not correct"], HttpStatusCode.Unauthorized);
        }
        catch (UserBlockException e) {
            return ResponseHelper.CreateEmptyResponse(["User account disabled"], HttpStatusCode.Unauthorized);
        }
        catch (UserLockedoutException e) {
            return ResponseHelper.CreateEmptyResponse(["User account locked out"], HttpStatusCode.Unauthorized);
        }
        catch (InvalidLoginException e) {
            return ResponseHelper.CreateEmptyResponse(["User not found or password is not correct"], HttpStatusCode.Unauthorized);
        }
    }

    /// <summary>
    /// will be called when user request to logout
    /// </summary>
    /// <returns></returns>
    [HttpPost("logout")]
    [SwaggerOperation(Tags = new[] {
        "Auth"
    })]
    [Authorize]
    public async Task<IActionResult> Logout() {
        var userToken = User.Identity?.Name!;
        await Mediator.Send(new LogoutUserRequest {
            UserToken = userToken
        });
        return ResponseHelper.CreateEmptyResponse();
    }

    /// <summary>
    /// will be called when user forgot password and request to reset password link
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("forgot-password/request")]
    [SwaggerOperation(Tags = new[] {
        "Auth"
    })]
    [SwaggerResponse(StatusCodes.Status200OK, "Response of request OTP", typeof(DataResponse<RequestOTPResponse>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request", typeof(DataResponse<EmptyResponse>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error", typeof(DataResponse<EmptyResponse>))]
    [AllowAnonymous]
    public async Task<IActionResult> ForgotPassword(ForgetPasswordRequest request) {
        var validator = await _forgetPasswordValidator.ValidateAsync(request);
        if (!validator.IsValid) {
            return ResponseHelper.CreateEmptyResponse(HandlerErrorResponse(validator), HttpStatusCode.BadRequest);
        }
        try {
            var result = await Mediator.Send(request.ToProcessorEntity());
            return ResponseHelper.CreateResponse(result);
        }
        catch (EntityNotFoundException e) {
            _logger.LogError(e, "One of the entities was not found");
            return ResponseHelper.CreateEmptyResponse(new List<string> {
                "One of the params was not found"
            }, HttpStatusCode.BadRequest);
        }
        catch (MFAProviderImplementedException e) {
            _logger.LogError(e, "MFA Provider is not implemented");
            return ResponseHelper.CreateEmptyResponse(new List<string> {
                "MFA Provider is not implemented"
            }, HttpStatusCode.BadRequest);
        }
        catch (OTPNotFoundException e) {
            _logger.LogError(e, "OTP not found");
            return ResponseHelper.CreateEmptyResponse(new List<string> {
                "OTP not found"
            }, HttpStatusCode.Forbidden);
        }
        catch (Exception e) {
            _logger.LogError(e, "Error in RequestOTPCode");
            return ResponseHelper.CreateEmptyResponse(new List<string> {
                e.Message
            }, HttpStatusCode.InternalServerError);
        }
    }
    /// <summary>
    /// will be called when user confirm email and request to reset password
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("forgot-password/confirm")]
    [SwaggerOperation(Tags = new[] {
        "Auth"
    })]
    [AllowAnonymous]
    public async Task<IActionResult> ForgotPasswordConfirm(VerifyOtpUserRequest request) {
        var validator = await _verifyOtpUserValidator.ValidateAsync(request);
        if (!validator.IsValid) {
            return ResponseHelper.CreateEmptyResponse(HandlerErrorResponse(validator), HttpStatusCode.BadRequest);
        }
        try {
            return ResponseHelper.CreateResponse(await Mediator.Send(new Domain.Requests.Processor.Services.User.VerifyOtpUserRequest {
                Code = request.Code,
                OTPToken = request.OTPToken,
                UserToken = request.UserToken
            }));
        }
        catch (EntityNotFoundException e) {
            _logger.LogError(e, "One of the entities was not found");
            return ResponseHelper.CreateEmptyResponse(new List<string> {
                "One of the params was not found"
            }, HttpStatusCode.BadRequest);
        }
        catch (Exception e) {
            _logger.LogError(e, "Error in RequestOTPCode");
            return ResponseHelper.CreateEmptyResponse(new List<string> {
                e.Message
            }, HttpStatusCode.InternalServerError);
        }
    }

    /// <summary>
    /// will be called when user request to reset password
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("reset-password")]
    [SwaggerOperation(Tags = new[] {
        "Auth"
    })]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequest request) {
        var validator = await _resetPasswordValidator.ValidateAsync(request);
        if (!validator.IsValid) {
            return ResponseHelper.CreateEmptyResponse(HandlerErrorResponse(validator), HttpStatusCode.BadRequest);
        }

        ;
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null) {
            return ResponseHelper.CreateEmptyResponse(new List<string> {
                "User not found."
            }, HttpStatusCode.BadRequest);
        }

        if (request.Password == request.NewPassword) {
            return ResponseHelper.CreateEmptyResponse(new List<string> {
                "New password must be different from old password."
            }, HttpStatusCode.BadRequest);
        }

        var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
        if (result.Succeeded) {
            return ResponseHelper.CreateEmptyResponse();
        }

        _logger.LogError("User [{UserId}] failed to reset password. Errors: {ResultErrors}", user.Id,
            result.Errors.Select(s => s.Description).ToList());
        return ResponseHelper.CreateEmptyResponse(new List<string> {
            "Error while resetting password"
        }, HttpStatusCode.BadRequest);
    }

    /// <summary>
    /// will be called when active access token is expired and need to be refreshed
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("refresh-token")]
    [SwaggerOperation(Tags = new[] {
        "Auth"
    })]
    [Authorize]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request) {
        var validator = await _refreshTokenValidator.ValidateAsync(request);
        if (!validator.IsValid) {
            return ResponseHelper.CreateEmptyResponse(HandlerErrorResponse(validator), HttpStatusCode.BadRequest);
        }

        try {
            var userName = User.Identity?.Name!;
            _logger.LogInformation($"User [{userName}] is trying to refresh JWT token.");
            if (string.IsNullOrWhiteSpace(request.RefreshToken)) {
                return ResponseHelper.CreateEmptyResponse(new List<string> {
                    "Invalid client request"
                }, HttpStatusCode.BadRequest);
            }

            var accessToken = (await HttpContext.GetTokenAsync("Bearer", "access_token"))!;
            var jwtResult = _jwtAuthManager.Refresh(request.RefreshToken, accessToken, DateTime.Now);
            _logger.LogInformation($"User [{userName}] has refreshed JWT token.");

            var user = await _userManager.FindByEmailAsync(userName);
            return ResponseHelper.CreateResponse(new AuthResponse {
                Tokens = jwtResult,
                UserId = new Guid(user.Id)
            });
        }
        catch (SecurityTokenException e) {
            _logger.LogError("Invalid JWT token: {EMessage}", e.Message);
            return ResponseHelper.CreateEmptyResponse(new List<string> {
                "Invalid JWT token"
            }, HttpStatusCode.BadRequest); // return 401 so that the client side can redirect the user to login page
        }
    }

    /// <summary>
    /// will be called when user login request mfa access this will trigger the mfa code to be sent to user
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("request-code")]
    [SwaggerOperation(Tags = new[] {
        "Auth"
    })]
    [AllowAnonymous]
    public async Task<IActionResult> RequestCode(SendCodeToProviderRequest request) {
        var validator = await _sendCodeToProviderValidator.ValidateAsync(request);
        if (!validator.IsValid) {
            return ResponseHelper.CreateEmptyResponse(HandlerErrorResponse(validator), HttpStatusCode.BadRequest);
        }

        var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
        if (user == null) {
            return ResponseHelper.CreateEmptyResponse(new List<string> {
                "Unable to load two-factor authentication user."
            }, HttpStatusCode.BadRequest);
        }

        var providers = await _userManager.GetValidTwoFactorProvidersAsync(user);
        var requestProvider = request.Provider == MFAProvider.SMS ? "Phone" : "Email";
        if (!providers.Contains(requestProvider)) {
            return ResponseHelper.CreateEmptyResponse(new List<string> {
                "unknown provider."
            }, HttpStatusCode.BadRequest);
        }

        var code = await _userManager.GenerateTwoFactorTokenAsync(user, requestProvider);
        if (string.IsNullOrWhiteSpace(code)) {
            return ResponseHelper.CreateEmptyResponse(new List<string> {
                "Invalid code"
            }, HttpStatusCode.BadRequest);
        }

        var message = "Your security code is: " + code;
        switch (requestProvider) {
            case "Email":
                // await _emailSender.SendEmailAsync(await _userManager.GetEmailAsync(user), "Security Code", message);
                break;
            case "Phone":
                // await _smsSender.SendSmsAsync(await _userManager.GetPhoneNumberAsync(user), message);
                break;
        }
        return ResponseHelper.CreateEmptyResponse();
    }

    /// <summary>
    /// this will be called when user request to verify mfa code
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet("verify-code")]
    [SwaggerOperation(Tags = new[] {
        "Auth"
    })]
    [AllowAnonymous]
    public async Task<IActionResult> VerifyCode(VerifyFromProviderRequest request) {
        var validator = await _verifyFromProviderValidator.ValidateAsync(request);
        if (!validator.IsValid) {
            return ResponseHelper.CreateEmptyResponse(HandlerErrorResponse(validator), HttpStatusCode.BadRequest);
        }

        var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(request.Code, true, request.RememberMe);
        if (result.Succeeded) {
            return ResponseHelper.CreateEmptyResponse();
        }

        if (result.IsLockedOut) {
            _logger.LogWarning(2, "User account locked out.");
            return ResponseHelper.CreateEmptyResponse(new List<string> {
                "User account locked out"
            }, HttpStatusCode.BadRequest);
        }
        return ResponseHelper.CreateEmptyResponse(new List<string> {
            "Invalid code"
        }, HttpStatusCode.BadRequest);
    }

    /// <summary>
    /// this will be called when user request to confirm email
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    [HttpGet("confirm-email")]
    [SwaggerOperation(Tags = new[] {
        "Auth"
    })]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmail(string userId, string code) {
        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(code)) {
            return ResponseHelper.CreateEmptyResponse(new List<string> {
                "user or code not found"
            }, HttpStatusCode.BadRequest);
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) {
            return ResponseHelper.CreateEmptyResponse(new List<string> {
                "user or code not found"
            }, HttpStatusCode.BadRequest);
        }

        var result = await _userManager.ConfirmEmailAsync(user, code);
        if (result.Succeeded) {
            return ResponseHelper.CreateEmptyResponse();
        }

        return ResponseHelper.CreateEmptyResponse(new List<string> {
            "Invalid code"
        }, HttpStatusCode.BadRequest);
    }

    /// <summary>
    /// this will be called when user request to register new account
    /// </summary>
    /// <param name="request"></param>
    /// <param name="automaticLogin">only for admin uses</param>
    /// <returns></returns>
    [HttpPost("register")]
    [SwaggerOperation(Tags = new[] {
        "Auth"
    })]
    [SwaggerResponse(StatusCodes.Status200OK, "Register new User", typeof(DataResponse<AuthResponse>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "bad register data", typeof(DataResponse<EmptyResponse>))]
    [AllowAnonymous]
    public async Task<IActionResult> Register([SwaggerRequestBody("register user payload", Required = true)] RegisterNewUserRequest request, bool automaticLogin = false) {
        var response = new AuthResponse();
        var validator = await _registerNewUserValidator.ValidateAsync(request);
        if (!validator.IsValid) {
            return ResponseHelper.CreateEmptyResponse(HandlerErrorResponse(validator), HttpStatusCode.BadRequest);
        }

        LocateCountryResponse resultCountryResponse;

        try {
            resultCountryResponse = await Mediator.Send(new LocateCountryRequest {
                CountryId = int.Parse(request.CountryId)
            });
            if (!resultCountryResponse.IsFound) {
                return ResponseHelper.CreateEmptyResponse(new List<string> {
                    "country not found"
                }, HttpStatusCode.BadRequest);
            }
        }
        catch (Exception e) {
            _logger.LogError(e, "Error while locating country");
            return ResponseHelper.CreateEmptyResponse(new List<string> {
                "country not found"
            }, HttpStatusCode.BadRequest);
        }

        if (resultCountryResponse is { IsFound: true }) {
            var user = new ApplicationUser {
                UserName = request.Email,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                TwoFactorEnabled = true
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded) {
                _logger.LogInformation(3, "User created a new account with password.");
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                // Todo : handle new user emaqil confirmation / phone number confirmation
                // var callbackUrl = Url.Action("ConfirmEmail", "Account", new {userId = user.Id, code},
                //     protocol: HttpContext.Request.Scheme);
                // await _emailSender.SendEmailAsync(request.Email, "Confirm your account",
                //     $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");
                if (automaticLogin) {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                }

                _logger.LogInformation(4, "User created a new account with password.");
                return ResponseHelper.CreateResponse(new AuthResponse {
                    UserId = new Guid(user.Id)
                });
            }
        }

        return ResponseHelper.CreateEmptyResponse(new List<string> {
            "error while creating user account"
        }, HttpStatusCode.InternalServerError);
    }


    // TODO : implement this  the main idea took from https://github.com/dotnet-labs/JwtAuthDemo/blob/master/webapi/JwtAuthDemo/Controllers/AccountController.cs please rebuild it 
    // [HttpPost("impersonation")]
    // [Authorize(Roles = UserRoles.Admin)]
    // public ActionResult Impersonate([FromBody] ImpersonationRequest request)
    // {
    //     var userName = User.Identity?.Name;
    //     _logger.LogInformation($"User [{userName}] is trying to impersonate [{request.UserName}].");
    //
    //     var impersonatedRole = _userService.GetUserRole(request.UserName);
    //     if (string.IsNullOrWhiteSpace(impersonatedRole))
    //     {
    //         _logger.LogInformation($"User [{userName}] failed to impersonate [{request.UserName}] due to the target user not found.");
    //         return BadRequest($"The target user [{request.UserName}] is not found.");
    //     }
    //     if (impersonatedRole == UserRoles.Admin)
    //     {
    //         _logger.LogInformation($"User [{userName}] is not allowed to impersonate another Admin.");
    //         return BadRequest("This action is not supported.");
    //     }
    //
    //     var claims = new[]
    //     {
    //         new Claim(ClaimTypes.Name,request.UserName),
    //         new Claim(ClaimTypes.Role, impersonatedRole),
    //         new Claim("OriginalUserName", userName ?? string.Empty)
    //     };
    //
    //     var jwtResult = _jwtAuthManager.GenerateTokens(request.UserName, claims, DateTime.Now);
    //     _logger.LogInformation($"User [{request.UserName}] is impersonating [{request.UserName}] in the system.");
    //     return Ok(new LoginResult
    //     {
    //         UserName = request.UserName,
    //         Role = impersonatedRole,
    //         OriginalUserName = userName,
    //         AccessToken = jwtResult.AccessToken,
    //         RefreshToken = jwtResult.RefreshToken.TokenString
    //     });
    // }

    // TODO : implement this  the main idea took from https://github.com/dotnet-labs/JwtAuthDemo/blob/master/webapi/JwtAuthDemo/Controllers/AccountController.cs please rebuild it 
    // [HttpPost("stop-impersonation")]
    // public ActionResult StopImpersonation()
    // {
    //     var userName = User.Identity?.Name;
    //     var originalUserName = User.FindFirst("OriginalUserName")?.Value;
    //     if (string.IsNullOrWhiteSpace(originalUserName))
    //     {
    //         return BadRequest("You are not impersonating anyone.");
    //     }
    //     _logger.LogInformation($"User [{originalUserName}] is trying to stop impersonate [{userName}].");
    //
    //     var role = _userService.GetUserRole(originalUserName);
    //     var claims = new[]
    //     {
    //         new Claim(ClaimTypes.Name,originalUserName),
    //         new Claim(ClaimTypes.Role, role)
    //     };
    //
    //     var jwtResult = _jwtAuthManager.GenerateTokens(originalUserName, claims, DateTime.Now);
    //     _logger.LogInformation($"User [{originalUserName}] has stopped impersonation.");
    //     return Ok(new LoginResult
    //     {
    //         UserName = originalUserName,
    //         Role = role,
    //         OriginalUserName = null,
    //         AccessToken = jwtResult.AccessToken,
    //         RefreshToken = jwtResult.RefreshToken.TokenString
    //     });
    // }
    private List<string> HandlerErrorResponse(ValidationResult result) {
        return result.Errors.Select(error => error.ErrorMessage).ToList();
    }

    private Task<ApplicationUser> GetCurrentUserAsync() {
        return _userManager.GetUserAsync(HttpContext.User);
    }
}