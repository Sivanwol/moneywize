using Swashbuckle.AspNetCore.Annotations;
namespace Infrastructure.Responses.Controllers.Auth;

[SwaggerSchema(Required = new[] {
    "Login Response"
})]
[SwaggerTag("User")]
public class LoginResponse {
    [SwaggerSchema("user token this is a unique token for the user")]
    public string? UserToken { get; set; }
}