namespace Domain.Entities;

public class AspNetRole {
    public string Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<ApplicationUser> Users { get; set; }
}