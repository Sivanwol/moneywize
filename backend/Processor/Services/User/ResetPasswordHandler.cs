using Application.Constraints;
using Application.Exceptions;
using Application.Responses.Base;
using Domain.Entities;
using Domain.Persistence.Repositories.Interfaces;
using Infrastructure.Requests.Processor.Services.User;
using Infrastructure.Services.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Serilog;
namespace Processor.Services.User;

public class ResetPasswordHandler : IRequestHandler<ResetPasswordRequest, EmptyResponse> {
    private readonly IApplicationUserRepository _applicationUserRepository;
    private readonly IJwtAuthManager _jwtAuthManager;
    private readonly IMediator _mediator;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public ResetPasswordHandler(IMediator mediator,
        IJwtAuthManager jwtAuthManager,
        IApplicationUserRepository applicationUserRepository,
        RoleManager<IdentityRole> roleManager,
        UserManager<ApplicationUser> userManager) {
        _jwtAuthManager = jwtAuthManager;
        _mediator = mediator;
        _applicationUserRepository = applicationUserRepository;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<EmptyResponse> Handle(ResetPasswordRequest request, CancellationToken cancellationToken) {
        Log.Logger.Information($"ResetPasswordHandler: [{request.UserId}]");
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null) {
            throw new EntityNotFoundException("User", request.UserId.ToString());
        }
        if (request.UserId != request.LoggedUserId) {
            var hasMatchingRoles = await HasMathchingRoles(user);
            if (!hasMatchingRoles) {
                throw new AuthenticationException("Invalid user not matching permissions");
            }
        }
        var result = await _userManager.CheckPasswordAsync(user, request.OldPassword);
        if (!result) {
            throw new AuthenticationException("Invalid password");
        }
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var resetResult = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);
        if (!resetResult.Succeeded) {
            resetResult.Errors.ToList().ForEach(e => Log.Logger.Error("unable change password - error: {0}", e.Description ?? ""));
            throw new AuthenticationException("unable change password");
        }
        return new EmptyResponse();
    }

    public async Task<bool> HasMathchingRoles(ApplicationUser user) {
        var userMgn = await _userManager.FindByIdAsync(user.Id);
        if (userMgn == null) {
            throw new EntityNotFoundException("User", user.Id);
        }
        var userRoles = await _userManager.GetRolesAsync(user);
        return userRoles.Any(r => r == Roles.Admin);
    }
}