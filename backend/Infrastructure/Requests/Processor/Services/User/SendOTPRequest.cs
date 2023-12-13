using Infrastructure.Enums;
using Infrastructure.Responses.Controllers.Auth;
using MediatR;
namespace Infrastructure.Requests.Processor.Services.User;

public class SendOTPRequest : IRequest<RequestOTPResponse> {
    public string PhoneNumber { get; set; }
    public Guid CountryId { get; set; }
    public MFAProvider Provider { get; set; }
    public bool ReSend { get; set; }
}