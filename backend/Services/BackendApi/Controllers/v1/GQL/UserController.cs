using System.ComponentModel;
using System.Security.Claims;
using Application.Configs;
using Application.Exceptions;
using Application.Utils;
using Application.Utils.Service;
using FluentValidation;
using GraphQL.AspNet.Attributes;
using GraphQL.AspNet.Interfaces.Controllers;
using Infrastructure.GQL;
using Infrastructure.GQL.Common;
using Infrastructure.GQL.Inputs.Common;
using Infrastructure.GQL.Inputs.User;
using Infrastructure.Requests.Processor.Services.User;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Serilog;
namespace BackendApi.Controllers.v1.GQL;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[GraphRoute("user")]
public class UserController : BaseGraphController {
    private readonly IValidator<EntityPage> _pageValidator;
    private readonly IValidator<UpdateUserPreferenceRequest> _updateUserPreferenceValidator;
    public UserController(
        IMediator mediator,
        BackendApplicationConfig configuration,
        IValidator<UpdateUserPreferenceRequest> updateUserPreferenceValidator,
        IValidator<EntityPage> pageValidator) : base(mediator, configuration) {
        _pageValidator = pageValidator;
        _updateUserPreferenceValidator = updateUserPreferenceValidator;
    }
    [Query("getMyProfile", typeof(User))]
    [Description("Will return the current user profile")]
    public async Task<IGraphActionResult> GetMyProfile() {
        if (IsMaintenanceMode()) {
            return BadRequest("Maintenance Mode");
        }
        try {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) {
                Log.Logger.Error("GetMyProfile: User not found");
                return Error("User not found");
            }
            var user = await Mediator.Send(new GetUserProfileRequest {
                LoggedInUserId = Guid.Parse(userId),
                UserId = Guid.Parse(userId)
            });
            return Ok(user);
        }
        catch (Exception e) {
            Log.Logger.Error(e, e.Message);
            return Error(e.Message);
        }
    }

    [Query("getUserProfile", typeof(User))]
    [Description("Will return the user profile")]
    public async Task<IGraphActionResult> GetUserProfile(
        [FromGraphQL("userId")] [Description("User Id")]
        Guid userId) {
        if (IsMaintenanceMode()) {
            return BadRequest("Maintenance Mode");
        }
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try {
            var loggedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == Guid.Empty || Guid.Parse(loggedUserId!) == Guid.Empty) {
                Log.Logger.Error("GetMyProfile: User not found");
                return BadRequest("User not found");
            }
            var user = await Mediator.Send(new GetUserProfileRequest {
                LoggedInUserId = Guid.Parse(loggedUserId!),
                UserId = userId
            });
            return Ok(user);
        }
        catch (EntityNotFoundException e) {
            Log.Logger.Error(e, e.Message);
            return Error("Failed to find user");
        }
        catch (AuthorizationException e) {
            Log.Logger.Error(e, e.Message);
            return Error("User not authorized");
        }
        catch (Exception e) {
            Log.Logger.Error(e, e.Message);
            return Error(e.Message);
        }
    }

    [Query("getUsers", typeof(EntityPage<User>))]
    [Description("Will return the user activities")]
    public async Task<IGraphActionResult> GetUsers(
        [FromGraphQL("page")] [Description("Entity Page Data( paging , sorting , filtering )")]
        EntityPage page) {
        if (IsMaintenanceMode()) {
            return BadRequest("Maintenance Mode");
        }
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try {
            var loggedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (Guid.Parse(loggedUserId!) == Guid.Empty) {
                Log.Logger.Error("GetUsers: User not found");
                return BadRequest("User not found");
            }
            var validator = await _pageValidator.ValidateAsync(page);
            if (!validator.IsValid) {
                return BadRequest(string.Join(",", ResponseHelper.HandlerErrorResponse(validator)));
            }
            var activities = await Mediator.Send(new GetUsersRequest {
                LoggedInUserId = Guid.Parse(loggedUserId!),
                PageControl = page,
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "",
                UserAgent = HttpContext.Request.Headers["User-Agent"].ToString()
            });
            return Ok(activities);
        }
        catch (EntityNotFoundException e) {
            Log.Logger.Error(e, e.Message);
            return Error("Failed to find user");
        }
        catch (Exception e) {
            Log.Logger.Error(e, e.Message);
            return Error(e.Message);
        }
    }

    [Query("getUserActivities", typeof(EntityPage<Activities>))]
    [Description("Will return the user activities")]
    public async Task<IGraphActionResult> GetUserActivities(
        [FromGraphQL("userId")] [Description("User Id")]
        Guid userId,
        [FromGraphQL("page")] [Description("Entity Page Data( paging , sorting , filtering )")]
        EntityPage page) {
        if (IsMaintenanceMode()) {
            return BadRequest("Maintenance Mode");
        }
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try {
            var loggedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == Guid.Empty || Guid.Parse(loggedUserId!) == Guid.Empty) {
                Log.Logger.Error("GetMyProfile: User not found");
                return BadRequest("User not found");
            }
            var validator = await _pageValidator.ValidateAsync(page);
            if (!validator.IsValid) {
                return BadRequest(string.Join(",", ResponseHelper.HandlerErrorResponse(validator)));
            }
            var activities = await Mediator.Send(new UserActivityRequest {
                UserId = userId,
                LoggedInUserId = Guid.Parse(loggedUserId!),
                PageControl = page
            });
            return Ok(activities);
        }
        catch (EntityNotFoundException e) {
            Log.Logger.Error(e, e.Message);
            return Error("Failed to find user");
        }
        catch (Exception e) {
            Log.Logger.Error(e, e.Message);
            return Error(e.Message);
        }
    }

    [Mutation("updateUserPreference", typeof(bool))]
    [Description("Update user preference")]
    public async Task<IGraphActionResult> UpdateUserPreference(
        [FromGraphQL("preferences")] [Description("Preference Input")]
        List<UserPreferenceInput> preferences) {
        if (IsMaintenanceMode()) {
            return BadRequest("Maintenance Mode");
        }
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try {
            var loggedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (Guid.Parse(loggedUserId!) == Guid.Empty) {
                Log.Logger.Error("GetMyProfile: User not found");
                return BadRequest("User not found");
            }
            var preferenceObject = new UpdateUserPreferenceRequest {
                UserId = Guid.Parse(loggedUserId!),
                Preferences = preferences,
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "",
                UserAgent = HttpContext.Request.Headers["User-Agent"].ToString()
            };
            var validator = await _updateUserPreferenceValidator.ValidateAsync(preferenceObject);
            if (!validator.IsValid) {
                return BadRequest(string.Join(",", ResponseHelper.HandlerErrorResponse(validator)));
            }
            return Ok(await Mediator.Send(preferenceObject));
        }
        catch (EntityNotFoundException e) {
            Log.Logger.Error(e, e.Message);
            return Error("Failed to find user");
        }
        catch (Exception e) {
            Log.Logger.Error(e, e.Message);
            return Error(e.Message);
        }
    }

    [Mutation("resetUserPreference", typeof(bool))]
    [Description("Reset user preference")]
    public async Task<IGraphActionResult> ResetUserPreference() {
        if (IsMaintenanceMode()) {
            return BadRequest("Maintenance Mode");
        }
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try {
            var loggedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (Guid.Parse(loggedUserId!) == Guid.Empty) {
                Log.Logger.Error("GetMyProfile: User not found");
                return BadRequest("User not found");
            }
            await Mediator.Send(new ResetUserPreferenceRequest {
                UserId = Guid.Parse(loggedUserId!),
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "",
                UserAgent = HttpContext.Request.Headers["User-Agent"].ToString()
            });
            return Ok(true);
        }
        catch (EntityNotFoundException e) {
            Log.Logger.Error(e, e.Message);
            return Error("Failed to find user");
        }
        catch (Exception e) {
            Log.Logger.Error(e, e.Message);
            return Error(e.Message);
        }
    }
}