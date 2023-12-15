using Application.Exceptions;
using Domain.Entities;
using Infrastructure.Requests.Processor.Services.User;
using Infrastructure.Responses.Controllers.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Serilog;
namespace Processor.Services.User;

public class LoginWebHandler : IRequestHandler<LoginWebUserRequest, LoginResponse> {
    private readonly IMediator _mediator;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public LoginWebHandler(IMediator mediator,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager) {
        _mediator = mediator;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<LoginResponse> Handle(LoginWebUserRequest request, CancellationToken cancellationToken) {
        Log.Logger.Information($"LogoutUserHandler: [{request.Email}]");
        var result =
            await _signInManager.PasswordSignInAsync(request.Email, request.Password, request.RememberMe, lockoutOnFailure: true);
        var user = await _userManager.FindByEmailAsync(request.Email);
        // user not found
        if (user == null) {
            Log.Logger.Warning("User account not found");
            throw new UserNotFoundException(request.Email);
        }

        // user account got disabled
        if (result.IsNotAllowed) {
            Log.Logger.Warning("User account is disabled - {UserId}", user.Id);
            throw new UserBlockException();
        }

        // user locked out
        if (result.IsLockedOut) {
            Log.Logger.Warning("User account locked out - {UserId}", user.Id);
            throw new UserLockedoutException();
        }

        // bad password
        if (!result.Succeeded) {
            Log.Logger.Warning("Invalid login attempt - {UserId}", user.Id);
            throw new InvalidLoginException();
        }
        Log.Logger.Information("User logged in -  {UserId}", user.Id);
        return new LoginResponse {
            UserToken = user.Token
        };
    }
}