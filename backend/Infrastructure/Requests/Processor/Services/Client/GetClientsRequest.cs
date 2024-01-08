using Infrastructure.GQL.Common;
using Infrastructure.GQL.Inputs.Common;
using MediatR;
namespace Infrastructure.Requests.Processor.Services.Client;

public class GetClientsRequest : IRequest<EntityPage<GQL.Client>> {
    public Guid LoggedInUserId { get; set; }
    public EntityPage PageControl { get; set; }
    public string IpAddress { get; set; }
    public string UserAgent { get; set; }
}