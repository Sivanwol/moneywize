using Application.Responses.Base;
using MediatR;
namespace Infrastructure.Requests.Processor.Services.User;

public class DeleteUserRequest : IRequest<EmptyResponse> {
    public Guid UserId { get; set; }
}