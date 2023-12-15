using Domain.Entities;
using Domain.Persistence.Repositories.Interfaces;
using Infrastructure.Requests.Processor.Services.User;
using Infrastructure.Services.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Serilog;
namespace Processor.Services.User;

public class LogoutUserHandler : IRequestHandler<LogoutUserRequest, bool> {
    private readonly IAppUserRepository _appUserRepository;
    private readonly IJwtAuthManager _jwtAuthManager;
    private readonly IMediator _mediator;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public LogoutUserHandler(IMediator mediator,
        IJwtAuthManager jwtAuthManager,
        IAppUserRepository appUserRepository,
        SignInManager<ApplicationUser> signInManager) {
        _jwtAuthManager = jwtAuthManager;
        _mediator = mediator;
        _appUserRepository = appUserRepository;
        _signInManager = signInManager;
    }

    public async Task<bool> Handle(LogoutUserRequest request, CancellationToken cancellationToken) {
        Log.Logger.Information($"LogoutUserHandler: [{request.UserToken}]");
        var result = await _appUserRepository.GetUserByToken(request.UserToken);
        if (result == null) {
            return false;
        }
        await _signInManager.SignOutAsync();
        _jwtAuthManager.RemoveRefreshTokenByUserToken(request.UserToken);
        return true;
    }
}