using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;
namespace Infrastructure.Requests.Controllers.Auth;

[SwaggerSchema(Required = new[] {
    "Forget User Password"
})]
[SwaggerTag("Auth Controllers")]
public class ForgetPasswordRequest {

    [SwaggerSchema("Email of the user")]
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [SwaggerSchema("Country Id of the user")]
    [Required]
    public Guid? CountyId { get; set; }

    [SwaggerSchema("Phone of the user")]
    [Required]
    [Phone]
    public string? Phone { get; set; }
}