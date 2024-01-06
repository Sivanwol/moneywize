using MediatR;
namespace Infrastructure.Requests.Processor.Services.Client;

public class CreateClientRequest : IRequest {
    public Guid LoggedInUserId { get; set; }
    public int? OwnClientId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Website { get; set; }
    public int CountryId { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string UserAgent { get; set; }
    public string IpAddress { get; set; }
}