using MediatR;
namespace Infrastructure.Requests.Processor.Services.User;

public class GetUserProfileRequest : IRequest<Infrastructure.GQL.User> {
    public Guid LoggedInUserId { get; set; }
    public Guid UserId { get; set; }
}