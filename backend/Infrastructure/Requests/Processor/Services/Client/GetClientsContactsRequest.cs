using Infrastructure.GQL.Common;
using Infrastructure.GQL.Inputs.Common;
using MediatR;
namespace Infrastructure.Requests.Processor.Services.Client;

public class GetClientsContactsRequest : IRequest<EntityPage<GQL.ClientContact>> {
    public Guid LoggedInUserId { get; set; }
    public int ClientId { get; set; }
    public EntityPage PageControl { get; set; }
    public string IpAddress { get; set; }
    public string UserAgent { get; set; }
}