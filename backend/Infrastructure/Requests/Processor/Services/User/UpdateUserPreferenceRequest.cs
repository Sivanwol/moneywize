using Infrastructure.GQL.Inputs.User;
using MediatR;
namespace Infrastructure.Requests.Processor.Services.User;

public class UpdateUserPreferenceRequest : IRequest<bool> {
    public Guid UserId { get; set; }
    public IList<UserPreferenceInput> Preferences { get; set; }
    public string IpAddress { get; set; }
    public string UserAgent { get; set; }
}