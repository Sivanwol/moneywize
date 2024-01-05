namespace Infrastructure.GQL;

public class Client {
    public int Id { get; set; }
    public string OwnerUserId { get; set; }
    public Client? Parent { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Website { get; set; }
    public Country Country { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public DateTime CreatedAt { get; set; }
}