using Application.Responses.Base;
using MediatR;
namespace Infrastructure.Requests.Processor.Services.User;

public class ForgotPasswordRequest : IRequest<EmptyResponse> {
    public string HostUrl { get; set; }
    public string? Email { get; set; }
    public Guid? CountryId { get; set; }
    public string? Phone { get; set; }
}