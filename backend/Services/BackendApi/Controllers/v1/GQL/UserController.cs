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
    public UserController(
        IMediator mediator,
        BackendApplicationConfig configuration,
        IValidator<EntityPage> pageValidator) : base(mediator, configuration) {
        _pageValidator = pageValidator;
    }
    [Query("getMyProfile", typeof(User))]
    [Description("Will return the current user profile")]
    public async Task<IGraphActionResult> GetMyProfile() {
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
            return Error("Something went wrong");
        }
    }

    [Query("getUserProfile", typeof(User))]
    [Description("Will return the user profile")]
    public async Task<IGraphActionResult> GetUserProfile(
        [FromGraphQL("userId")] [Description("User Id")]
        Guid userId) {
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
            return Error("Something went wrong");
        }
    }

    [Query("getUserActivities", typeof(EntityPage<Activity>))]
    [Description("Will return the user activities")]
    public async Task<IGraphActionResult> GetUserActivities(
        [FromGraphQL("userId")] [Description("User Id")]
        Guid userId,
        [FromGraphQL("page")] [Description("Entity Page Data( paging , sorting , filtering )")]
        EntityPage page) {
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
            try {
                var activities = await Mediator.Send(new UserActivityRequest {
                    UserId = userId,
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
                return Error("Something went wrong");
            }
        }
        catch (Exception e) {
            Log.Logger.Error(e, e.Message);
            return Error("Something went wrong");
        }
    }
}