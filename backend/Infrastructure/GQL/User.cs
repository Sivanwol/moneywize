namespace Infrastructure.GQL;

public class User {
    public string Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string Address { get; set; }
    public Country? Country { get; set; }
    public Language DisplayLanguage { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public ICollection<string> Roles { get; set; } = new List<string>();
}