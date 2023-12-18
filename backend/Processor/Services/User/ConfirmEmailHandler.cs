using Application.Constraints;
using Application.Exceptions;
using Application.Responses.Base;
using Domain.Entities;
using Domain.Persistence.Repositories.Interfaces;
using Infrastructure.Requests.Processor.Services.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Serilog;
namespace Processor.Services.User;

public class ConfirmEmailHandler : IRequestHandler<ConfirmEmailRequest, EmptyResponse> {
    private readonly IApplicationUserRepository _applicationUserRepository;
    private readonly IMediator _mediator;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public ConfirmEmailHandler(IMediator mediator,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IApplicationUserRepository applicationUserRepository) {
        _mediator = mediator;
        _applicationUserRepository = applicationUserRepository;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<EmptyResponse> Handle(ConfirmEmailRequest request, CancellationToken cancellationToken) {
        Log.Logger.Information($"LogoutUserHandler: [{request.UserToken}]");
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null) {
            throw new EntityNotFoundException("User", request.UserId.ToString());
        }
        if (user.Token != request.UserToken) {
            throw new AuthenticationException("Invalid user not matching the security token");
        }
        if (request.LoggedUserId == Guid.Empty || request.UserId == Guid.Empty) {
            throw new InvalidRequestException("Invalid user id and/or logged user id provided");
        }
        if (request.LoggedUserId == request.UserId && string.IsNullOrEmpty(request.Code)) {
            throw new InvalidRequestException("Code is required");
        }
        var code = request.Code;
        if (request.UserId != request.LoggedUserId) {
            var loggedUser = await _userManager.FindByIdAsync(request.LoggedUserId.ToString());
            if (loggedUser == null) {
                throw new EntityNotFoundException("User", request.LoggedUserId.ToString());
            }
            var hasMatchingRoles = await HasMathchingRoles(loggedUser);
            if (!hasMatchingRoles) {
                throw new AuthenticationException("Invalid user not matching permissions");
            }
            code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }
        var result = await _userManager.ConfirmEmailAsync(user, code);
        if (!result.Succeeded) {
            result.Errors.ToList().ForEach(e => Log.Logger.Error("unable confirm email - error: {0}", e.Description ?? ""));
            throw new AuthenticationException("unable confirm email");
        }
        return new EmptyResponse();
    }

    public async Task<bool> HasMathchingRoles(ApplicationUser user) {
        var userRoles = await _userManager.GetRolesAsync(user);
        return userRoles.Any(r => r == Roles.Admin);
    }
}