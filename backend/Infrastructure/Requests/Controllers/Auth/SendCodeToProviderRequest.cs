using Infrastructure.Enums;
namespace Infrastructure.Requests.Controllers.Auth;

public class SendCodeToProviderRequest {
    public MFAProvider Provider { get; set; }
}