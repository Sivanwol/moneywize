using System.ComponentModel;
using System.Security.Claims;
using Application.Configs;
using Application.Utils.Service;
using GraphQL.AspNet.Attributes;
using GraphQL.AspNet.Interfaces.Controllers;
using Infrastructure.GQL;
using Infrastructure.Requests.Processor.Services.Countries;
using Infrastructure.Requests.Processor.Services.Views;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Serilog;
namespace BackendApi.Controllers.v1.GQL;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[GraphRoute("general")]
public class GeneralController : BaseGraphController {
    public GeneralController(IMediator _mediator, BackendApplicationConfig _configuration) : base(_mediator, _configuration) { }
    [Query("maintenanceMode", typeof(bool))]
    [Description("Get maintenance mode status")]
    public IGraphActionResult MaintenanceMode() {
        return Ok(IsMaintenanceMode());
    }

    [Query("getCountries", typeof(IEnumerable<Country>))]
    [Description("Get all suported countries")]
    public async Task<IGraphActionResult> GetCountries() {
        if (IsMaintenanceMode()) {
            return BadRequest("Maintenance Mode");
        }
        try {
            return Ok(await Mediator.Send(new GetCountriesRequest()));
        }
        catch (Exception e) {
            Log.Logger.Error(e, e.Message);
            return Error(e.Message);
        }
    }

    [Query("getViews", typeof(IEnumerable<View>))]
    [Description("Get all views of the user and default views")]
    public async Task<IGraphActionResult> GetViews() {
        if (IsMaintenanceMode()) {
            return BadRequest("Maintenance Mode");
        }
        try {
            var loggedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var views = await Mediator.Send(new GetViewsRequest {
                UserId = loggedUserId!
            });
            return Ok(views);
        }
        catch (Exception e) {
            Log.Logger.Error(e, e.Message);
            return Error(e.Message);
        }
    }
}