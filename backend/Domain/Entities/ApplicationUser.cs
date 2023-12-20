using System.ComponentModel.DataAnnotations;
using Application.Queries;
using Microsoft.AspNetCore.Identity;
namespace Domain.Entities;

public class ApplicationUser : IdentityUser, ISoftDeletable {
    [ProtectedPersonalData] public Countries Country { get; set; }
    [ProtectedPersonalData] public Languages DisplayLanguage { get; set; }

    [StringLength(128)]
    [ProtectedPersonalData]
    public string Token { get; set; }

    [PersonalData] public string FirstName { get; set; }
    [PersonalData] public string LastName { get; set; }
    [ProtectedPersonalData] public string Address { get; set; }
    [PersonalData] public double Latitude { get; set; }
    [PersonalData] public double Longitude { get; set; }
    public bool TermsApproved { get; set; }
    public ICollection<Clients> Clients { get; set; }
    public ICollection<ActivityLog> ActivityLogs { get; set; }
    public DateTime? DeletedAt { get; }
}