using Application.Responses.Base;
using MediatR;
namespace Infrastructure.Requests.Processor.Services.User;

public class ConfirmEmailRequest : IRequest<EmptyResponse> {
    public Guid LoggedUserId { get; set; }
    public Guid UserId { get; set; }
    public string UserToken { get; set; }
    public string Code { get; set; }
}