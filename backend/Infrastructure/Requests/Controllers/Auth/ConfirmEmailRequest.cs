using Elastic.Apm.Api.Constraints;
using Swashbuckle.AspNetCore.Annotations;
namespace Infrastructure.Requests.Controllers.Auth;

public class ConfirmEmailRequest {
    [SwaggerSchema("User Id of the user")]
    [Required]
    public Guid UserId { get; set; }

    [SwaggerSchema("User Token of the user")]
    [Required]
    public string UserToken { get; set; }

    [SwaggerSchema("Code of the from the email")]
    public string Code { get; set; }

    [SwaggerSchema("allow Redirect")]
    public bool AllowRedirect { get; set; } = false;


    public Infrastructure.Requests.Processor.Services.User.ConfirmEmailRequest ToProcessorEntity(Guid loggedUserId) {
        return new Infrastructure.Requests.Processor.Services.User.ConfirmEmailRequest {
            LoggedUserId = loggedUserId,
            UserId = UserId,
            UserToken = UserToken,
            Code = Code
        };
    }
}