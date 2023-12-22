using Application.Configs;
using Application.Utils.Service;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ILogger = Microsoft.Extensions.Logging.ILogger;
namespace BackendApi.Controllers.v1;

[ApiVersion("1.0")]
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
}