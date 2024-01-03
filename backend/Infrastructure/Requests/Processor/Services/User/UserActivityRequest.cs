using Infrastructure.GQL;
using Infrastructure.GQL.Common;
using Infrastructure.GQL.Inputs.Common;
using MediatR;
namespace Infrastructure.Requests.Processor.Services.User;

public class UserActivityRequest : IRequest<EntityPage<Activities>> {
    public Guid LoggedInUserId { get; set; }
    public Guid UserId { get; set; }
    public EntityPage PageControl { get; set; }
}