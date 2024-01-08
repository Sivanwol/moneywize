using System.ComponentModel;
using Application.Configs;
using Application.Utils.Service;
using GraphQL.AspNet.Attributes;
using Infrastructure.GQL;
using Infrastructure.Requests.Processor.Services.Countries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
namespace BackendApi.Controllers.v1.GQL;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[GraphRoute("general")]
public class GeneralController : BaseGraphController {
    public GeneralController(IMediator _mediator, BackendApplicationConfig _configuration) : base(_mediator, _configuration) { }

    [Query("getCountries", typeof(IEnumerable<Country>))]
    [Description("Get all suported countries")]
    [AllowAnonymous]
    public async Task<IEnumerable<Country>> GetCountries() {
        return await Mediator.Send(new GetCountriesRequest());
    }
}