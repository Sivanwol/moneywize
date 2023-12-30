using Application.Configs;
using Application.Utils;
using Application.Utils.Service;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ILogger = Microsoft.Extensions.Logging.ILogger;
namespace BackendApi.Controllers.v1;

[ApiVersion("1.0")]
[Produces("application/json")]
[Authorize]
// [Authorize]
public class GeneralController : BaseApiController {
    private readonly ILogger _logger;
    // private readonly IValidator<MobileLoginUserRequest> _loginValidator;

    public GeneralController(
        IMediator mediator,
        BackendApplicationConfig configuration,
        ILoggerFactory loggerFactory
        // IValidator<MobileLoginUserRequest> loginValidator
    ) : base(mediator, configuration) {
        _logger = loggerFactory.CreateLogger<GeneralController>();
        // _loginValidator = loginValidator;
    }
    [Route("/secure-route")]
    public async Task<IActionResult> SecureRoute() {
        return ResponseHelper.CreateEmptyResponse();
    }
    [Route("/unsecure-route")]
    [AllowAnonymous]
    public async Task<IActionResult> UnsecureRoute() {
        return ResponseHelper.CreateEmptyResponse();
    }
}