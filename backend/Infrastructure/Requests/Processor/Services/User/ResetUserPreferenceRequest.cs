using MediatR;
namespace Infrastructure.Requests.Processor.Services.User;

public class ResetUserPreferenceRequest : IRequest {
    public Guid UserId { get; set; }
    public string IpAddress { get; set; }
    public string UserAgent { get; set; }
}