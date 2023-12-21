using Infrastructure.Responses.Controllers.User;
using MediatR;
namespace Infrastructure.Requests.Processor.Services.User;

public class GetUserProfileRequest : IRequest<ProfileResponse> {
    public Guid UserId { get; set; }
}