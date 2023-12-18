using System.ComponentModel.DataAnnotations;
using Infrastructure.Enums;
using Infrastructure.Requests.Processor.Services.User;
using Swashbuckle.AspNetCore.Annotations;
namespace Infrastructure.Requests.Controllers.Auth;

[SwaggerSchema(Required = new[] {
    "Request OTP for user"
})]
[SwaggerTag("Controllers")]
public class MobileRequestOTPUserRequest {

    [Required]
    [SwaggerSchema("Phone number")]
    public string? PhoneNumber { get; set; }

    [Required]
    [SwaggerSchema("Country Id")]
    public Guid? CountryId { get; set; }

    [Required]
    [SwaggerSchema("Provider of the MFA")]
    public string Provider { get; set; }

    [Required]
    [SwaggerSchema("Resent OTP")]
    public bool Resent { get; set; }

    public SendOTPRequest ToProcessorEntity() {
        return new SendOTPRequest {
            CountryId = CountryId,
            PhoneNumber = PhoneNumber,
            Provider = (MFAProvider)Enum.Parse(typeof(MFAProvider), Provider),
            ReSend = Resent
        };
    }
}