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
    private readonly IValidator<ClientContactInput> _createClientContactValidator;
    private readonly IValidator<ClientInput> _createClientValidator;

    private readonly IValidator<EntityPage> _pageValidator;
    private readonly IValidator<ClientContactUpdateInput> _updateClientContactValidator;
    public ClientController(
        IMediator _mediator,
        BackendApplicationConfig _configuration,
        IValidator<EntityPage> pageValidator,
        IValidator<ClientContactInput> createClientContactValidator,
        IValidator<ClientContactUpdateInput> updateClientContactValidator,
        IValidator<ClientInput> createClientValidator
    ) : base(_mediator, _configuration) {
        _createClientContactValidator = createClientContactValidator;
        _createClientValidator = createClientValidator;
        _updateClientContactValidator = updateClientContactValidator;
        _pageValidator = pageValidator;
    }

    [Mutation("createClient", typeof(string))]
    [Description("Will create a new client")]
    public async Task<IGraphActionResult> CreateClient(
        [FromGraphQL("client")] [Description("Client object input")]
        ClientInput clientInput
    ) {
        if (IsMaintenanceMode()) {
            return BadRequest("Maintenance Mode");
        }
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try {
            var loggedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var validator = await _createClientValidator.ValidateAsync(clientInput);
            if (!validator.IsValid) {
                return BadRequest(string.Join(",", ResponseHelper.HandlerErrorResponse(validator)));
            }
            await Mediator.Send(clientInput.ToProcessEntity(Guid.Parse(loggedUserId!),
                HttpContext.Connection.RemoteIpAddress?.ToString() ?? "",
                HttpContext.Request.Headers["User-Agent"].ToString()));
            return Ok("ok");
        }
        catch (Exception e) {
            Log.Logger.Error(e, e.Message);
            return Error(e.Message);
        }
    }

    [Mutation("updateClient", typeof(string))]
    [Description("Will update a new client")]
    public async Task<IGraphActionResult> UpdateClient(
        [FromGraphQL("clientId")] [Description("Client Id")]
        int clientId,
        [FromGraphQL("client")] [Description("Client Object input")]
        ClientInput clientInput
    ) {
        if (IsMaintenanceMode()) {
            return BadRequest("Maintenance Mode");
        }
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try {
            var loggedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var validator = await _createClientValidator.ValidateAsync(clientInput);
            if (!validator.IsValid) {
                return BadRequest(string.Join(",", ResponseHelper.HandlerErrorResponse(validator)));
            }
            await Mediator.Send(clientInput.ToProcessEntity(Guid.Parse(loggedUserId!),
                clientId,
                HttpContext.Connection.RemoteIpAddress?.ToString() ?? "",
                HttpContext.Request.Headers["User-Agent"].ToString()));
            return Ok("ok");
        }
        catch (Exception e) {
            Log.Logger.Error(e, e.Message);
            return Error(e.Message);
        }
    }

    [Mutation("deleteClient", typeof(string))]
    [Description("Will delete a new client")]
    public async Task<IGraphActionResult> DeleteClient(
        [FromGraphQL("clientId")] [Description("Client Id")]
        int clientId
    ) {
        if (IsMaintenanceMode()) {
            return BadRequest("Maintenance Mode");
        }
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try {
            var loggedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await Mediator.Send(new DeleteClientRequest() {
                LoggedInUserId = Guid.Parse(loggedUserId!),
                ClientId = clientId,
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "",
                UserAgent = HttpContext.Request.Headers["User-Agent"].ToString()
            });
            return Ok("ok");
        }
        catch (Exception e) {
            Log.Logger.Error(e, e.Message);
            return Error(e.Message);
        }
    }


    [Query("getAll", typeof(EntityPage<Client>))]
    [Description("Will return the clients")]
    public async Task<IGraphActionResult> GetClients(
        [FromGraphQL("page")] [Description("Entity Page Data( paging , sorting , filtering )")]
        EntityPage page) {
        if (IsMaintenanceMode()) {
            return BadRequest("Maintenance Mode");
        }
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try {
            var loggedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var validator = await _pageValidator.ValidateAsync(page);
            if (!validator.IsValid) {
                return BadRequest(string.Join(",", ResponseHelper.HandlerErrorResponse(validator)));
            }
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
            return Error(e.Message);
        }
    }

    [Mutation("createClientContact", typeof(string))]
    [Description("Will create a new client contact")]
    public async Task<IGraphActionResult> CreateClientContact(
        [FromGraphQL("contact")] [Description("Client Contact Object input")]
        ClientContactInput clientContactInput
    ) {
        if (IsMaintenanceMode()) {
            return BadRequest("Maintenance Mode");
        }
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try {
            var loggedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var validator = await _createClientContactValidator.ValidateAsync(clientContactInput);
            if (!validator.IsValid) {
                return BadRequest(string.Join(",", ResponseHelper.HandlerErrorResponse(validator)));
            }
            await Mediator.Send(clientContactInput.ToProcessEntity(Guid.Parse(loggedUserId!),
                HttpContext.Connection.RemoteIpAddress?.ToString() ?? "",
                HttpContext.Request.Headers["User-Agent"].ToString()));
            return Ok("ok");
        }
        catch (Exception e) {
            Log.Logger.Error(e, e.Message);
            return Error(e.Message);
        }
    }

    [Mutation("updateClientContact", typeof(string))]
    [Description("Will update an existing client contact")]
    public async Task<IGraphActionResult> UpdateClientContact(
        [FromGraphQL("clientContactId")] [Description("Client Contact Id")]
        int clientContactId,
        [FromGraphQL("contact")] [Description("Client Contact Object input")]
        ClientContactUpdateInput clientContactInput
    ) {
        if (IsMaintenanceMode()) {
            return BadRequest("Maintenance Mode");
        }
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try {
            var loggedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var validator = await _updateClientContactValidator.ValidateAsync(clientContactInput);
            if (!validator.IsValid) {
                return BadRequest(string.Join(",", ResponseHelper.HandlerErrorResponse(validator)));
            }
            await Mediator.Send(clientContactInput.ToProcessEntity(Guid.Parse(loggedUserId!),
                clientContactId,
                HttpContext.Connection.RemoteIpAddress?.ToString() ?? "",
                HttpContext.Request.Headers["User-Agent"].ToString()));
            return Ok("ok");
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


    [Mutation("deleteClientContact", typeof(string))]
    [Description("Will delete a client contact")]
    public async Task<IGraphActionResult> DeleteClientContact(
        [FromGraphQL("clientId")] [Description("Client Id")]
        int clientId,
        [FromGraphQL("clientContactId")] [Description("Client Contact Id")]
        int clientContactId
    ) {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try {
            var loggedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await Mediator.Send(new DeleteClientContactRequest() {
                LoggedInUserId = Guid.Parse(loggedUserId!),
                ClientId = clientId,
                ClientContactId = clientContactId,
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "",
                UserAgent = HttpContext.Request.Headers["User-Agent"].ToString()
            });
            return Ok("ok");
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

    [Query("getAllContracts", typeof(EntityPage<ClientContact>))]
    [Description("Will return the clients")]
    public async Task<IGraphActionResult> GetClientContracts(
        [FromGraphQL("clientId")] [Description("Client Id")]
        int clientId,
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
            var activities = await Mediator.Send(new GetClientsContactsRequest {
                LoggedInUserId = Guid.Parse(loggedUserId!),
                ClientId = clientId,
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
}