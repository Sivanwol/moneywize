using System.ComponentModel;
using Infrastructure.Requests.Processor.Services.Client;
namespace Infrastructure.GQL.Inputs.Client;

[Description("Client Create Object input")]
public class ClientInput {
    [Description("if this client is a child of another client")]
    public int? OwnClientId { get; set; }

    [Description("client name")]
    public string Name { get; set; }

    [Description("client description")]
    public string Description { get; set; }

    [Description("client website")]
    public string Website { get; set; }

    [Description("client Country Id")]
    public int CountryId { get; set; }

    [Description("client address")]
    public string? Address { get; set; }

    [Description("client city")]
    public string? City { get; set; }


    public CreateClientRequest ToProcessEntity(Guid loggedInUserId, string userAgent, string ipAddress) {
        return new CreateClientRequest {
            LoggedInUserId = loggedInUserId,
            OwnClientId = OwnClientId,
            Name = Name,
            Description = Description,
            Website = Website,
            CountryId = CountryId,
            Address = Address,
            City = City,
            UserAgent = userAgent,
            IpAddress = ipAddress
        };
    }
}