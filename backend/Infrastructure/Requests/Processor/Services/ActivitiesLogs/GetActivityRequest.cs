using Infrastructure.GQL;
using Infrastructure.GQL.Common;
using Infrastructure.GQL.Inputs.Common;
using MediatR;
namespace Infrastructure.Requests.Processor.Services.ActivitiesLogs;

public class GetActivityRequest : IRequest<EntityPage<Activities>> {
    public Guid LoggedInUserId { get; set; }
    public EntityPage PageControl { get; set; }
}