using System.ComponentModel;
using System.Security.Claims;
using Application.Configs;
using Application.Utils.Service;
using GraphQL.AspNet.Attributes;
using GraphQL.AspNet.Interfaces.Controllers;
using Infrastructure.GQL;
using Infrastructure.GQL.Inputs.View;
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

    [Query("getView", typeof(View))]
    [Description("Get all views of the user and default views")]
    public async Task<IGraphActionResult> GetView(
        [FromGraphQL("viewClientKey")] [Description("view client key")]
        Guid viewClientKey) {
        return null;
    }

    [Mutation("addView", typeof(View))]
    [Description("Add a new view")]
    public async Task<IGraphActionResult> AddView(
        [FromGraphQL("view")] [Description("view")]
        ViewCreateInput view) {
        return null;
    }

    [Mutation("duplicateView", typeof(View))]
    [Description("Duplicate a view")]
    public async Task<IGraphActionResult> DuplicateView(
        [FromGraphQL("viewClientKey")] [Description("view client key to duplicate")]
        Guid viewClientKey) {
        return null;
    }

    [Mutation("updateView", typeof(View))]
    [Description("Update a view (will not update columns)")]
    public async Task<IGraphActionResult> UpdateView(
        [FromGraphQL("view")] [Description("view")]
        ViewUpdateInput view) {
        return null;
    }

    [Mutation("deleteView", typeof(bool))]
    [Description("Delete a view")]
    public async Task<IGraphActionResult> DeleteView(
        [FromGraphQL("viewClientKey")] [Description("view client key to delete")]
        Guid viewClientKey) {
        return null;
    }

    [Mutation("updateViewColumns", typeof(View))]
    [Description("Update a view columns")]
    public async Task<IGraphActionResult> UpdateViewColumns(
        [FromGraphQL("view")] [Description("view")]
        ViewUpdateColumnsInput view) {
        return null;
    }

    // [Mutation("setDefaultView", typeof(bool))]
    // [Description("Set default view")]
    // public async Task<IGraphActionResult> SetDefaultView(
    //     [FromGraphQL("viewClientKey")] [Description("view client key to set as default")]
    //     Guid viewClientKey) {
    //     return null;
    // }
}