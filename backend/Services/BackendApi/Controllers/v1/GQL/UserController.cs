using System.ComponentModel;
using Application.Configs;
using Application.Utils.Service;
using GraphQL.AspNet.Attributes;
using Infrastructure.GQL;
using Infrastructure.Requests.Processor.Services.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
namespace BackendApi.Controllers.v1.GQL;

[Authorize]
public class UserController : BaseGraphController {
    public UserController(
        IMediator mediator,
        BackendApplicationConfig configuration) : base(mediator, configuration) { }
    [QueryRoot("myProfile")]
    [Description("Will return the current user profile")]
    public async Task<User> GetMyProfile() {
        var userId = User.Claims.FirstOrDefault(x => x.Type == "id")?.Value;
        if (userId == null) {
            throw new Exception("User not found");
        }
        return await Mediator.Send(new GetUserProfileRequest {
            UserId = Guid.Parse(userId)
        });
    }

    [QueryRoot("userProfile")]
    [Description("Will return the user profile")]
    public async Task<User> GetUserProfile([Description("User Id")] Guid userId) {
        return await Mediator.Send(new GetUserProfileRequest {
            UserId = userId
        });
    }
}