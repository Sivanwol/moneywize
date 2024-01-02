using System.ComponentModel;
using System.Security.Claims;
using Application.Configs;
using Application.Utils.Service;
using GraphQL.AspNet.Attributes;
using Infrastructure.GQL;
using Infrastructure.Requests.Processor.Services.User;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
namespace BackendApi.Controllers.v1.GQL;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[GraphRoute("user")]
public class UserController : BaseGraphController {
    public UserController(
        IMediator mediator,
        BackendApplicationConfig configuration) : base(mediator, configuration) { }
    [Query]
    [Description("Will return the current user profile")]
    public async Task<User> GetMyProfile() {
        var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) {
            throw new Exception("User not found");
        }
        return await Mediator.Send(new GetUserProfileRequest {
            UserId = Guid.Parse(userId)
        });
    }

    [Query]
    [Description("Will return the user profile")]
    public async Task<User> GetUserProfile(
        [FromGraphQL("userId")] [Description("User Id")]
        Guid userId) {
        return await Mediator.Send(new GetUserProfileRequest {
            UserId = userId
        });
    }
}