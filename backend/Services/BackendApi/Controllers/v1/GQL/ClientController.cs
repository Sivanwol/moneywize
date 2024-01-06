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
using Infrastructure.GQL.Inputs.Client;
using Infrastructure.GQL.Inputs.Common;
using Infrastructure.Requests.Processor.Services.Client;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Serilog;
namespace BackendApi.Controllers.v1.GQL;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[GraphRoute("clients")]
public class ClientController : BaseGraphController {
    private readonly IValidator<ClientInput> _createClientValidator;
    private readonly IValidator<EntityPage> _pageValidator;
    public ClientController(
        IMediator _mediator,
        BackendApplicationConfig _configuration,
        IValidator<EntityPage> pageValidator,
        IValidator<ClientInput> createClientValidator
    ) : base(_mediator, _configuration) {
        _createClientValidator = createClientValidator;
        _pageValidator = pageValidator;
    }

    [Mutation("createClient", typeof(ClientInput))]
    [Description("Will create a new client")]
    public async Task<IGraphActionResult> CreateClient(
        [FromGraphQL("client")] [Description("Client Create Object input")]
        ClientInput clientInput
    ) {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try {
            var loggedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var validator = await _createClientValidator.ValidateAsync(clientInput);
            if (!validator.IsValid) {
                return BadRequest(string.Join(",", ResponseHelper.HandlerErrorResponse(validator)));
            }
            try {
                await Mediator.Send(clientInput.ToProcessEntity(Guid.Parse(loggedUserId!),
                    HttpContext.Connection.RemoteIpAddress?.ToString() ?? "",
                    HttpContext.Request.Headers["User-Agent"].ToString()));
                return Ok();
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

    [Mutation("updateClient", typeof(ClientInput))]
    [Description("Will update a new client")]
    public async Task<IGraphActionResult> UpdateClient(
        [FromGraphQL("clientId")] [Description("Client Id")]
        int clientId,
        [FromGraphQL("client")] [Description("Client Create Object input")]
        ClientInput clientInput
    ) {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try {
            var loggedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var validator = await _createClientValidator.ValidateAsync(clientInput);
            if (!validator.IsValid) {
                return BadRequest(string.Join(",", ResponseHelper.HandlerErrorResponse(validator)));
            }
            try {
                await Mediator.Send(clientInput.ToProcessEntity(Guid.Parse(loggedUserId!),
                    HttpContext.Connection.RemoteIpAddress?.ToString() ?? "",
                    HttpContext.Request.Headers["User-Agent"].ToString()));
                return Ok();
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

    [Mutation("deleteClient")]
    [Description("Will delete a new client")]
    public async Task<IGraphActionResult> DeleteClient(
        [FromGraphQL("clientId")] [Description("Client Id")]
        int clientId
    ) {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try {
            var loggedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try {
                await Mediator.Send(new DeleteClientRequest() {
                    LoggedInUserId = Guid.Parse(loggedUserId!),
                    ClientId = clientId,
                    IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "",
                    UserAgent = HttpContext.Request.Headers["User-Agent"].ToString()
                });
                return Ok();
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


    [Query("getAll", typeof(EntityPage<Client>))]
    [Description("Will return the clients")]
    public async Task<IGraphActionResult> GetClients(
        [FromGraphQL("page")] [Description("Entity Page Data( paging , sorting , filtering )")]
        EntityPage page) {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try {
            var loggedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var validator = await _pageValidator.ValidateAsync(page);
            if (!validator.IsValid) {
                return BadRequest(string.Join(",", ResponseHelper.HandlerErrorResponse(validator)));
            }
            try {
                var activities = await Mediator.Send(new GetClientsRequest {
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
                return Error("Something went wrong");
            }
        }
        catch (Exception e) {
            Log.Logger.Error(e, e.Message);
            return Error("Something went wrong");
        }
    }
}