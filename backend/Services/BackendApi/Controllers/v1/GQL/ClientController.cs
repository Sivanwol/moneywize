using System.ComponentModel;
using System.Security.Claims;
using Application.Configs;
using Application.Utils;
using Application.Utils.Service;
using FluentValidation;
using GraphQL.AspNet.Attributes;
using GraphQL.AspNet.Interfaces.Controllers;
using Infrastructure.GQL.Inputs.Client;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Serilog;
namespace BackendApi.Controllers.v1.GQL;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[GraphRoute("clients")]
public class ClientController : BaseGraphController {

    private readonly IValidator<ClientInput> _createClientValidator;
    public ClientController(
        IMediator _mediator,
        BackendApplicationConfig _configuration,
        IValidator<ClientInput> createClientValidator
    ) : base(_mediator, _configuration) {
        _createClientValidator = createClientValidator;
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
}