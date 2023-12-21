using Application.Responses.Base;
using MediatR;
namespace Infrastructure.Requests.Processor.Services.User;

public class ExportUserRequest : IRequest<EmptyResponse> {
    public Guid UserId { get; set; }
}