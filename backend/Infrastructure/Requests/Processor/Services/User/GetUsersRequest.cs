using Infrastructure.GQL.Common;
using Infrastructure.GQL.Inputs.Common;
using MediatR;
namespace Infrastructure.Requests.Processor.Services.User;

public class GetUsersRequest : IRequest<EntityPage<GQL.User>> {
    public Guid LoggedInUserId { get; set; }
    public EntityPage PageControl { get; set; }
    public string UserAgent { get; set; }
    public string IpAddress { get; set; }
}