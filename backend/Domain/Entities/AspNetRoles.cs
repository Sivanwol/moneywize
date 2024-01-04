using Microsoft.AspNetCore.Identity;
namespace Domain.Entities;

public class AspNetRoles : IdentityRole {
    public virtual ICollection<IdentityUserRole<string>> UserRoles { get; set; }
}