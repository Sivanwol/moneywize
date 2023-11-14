using Microsoft.AspNetCore.Identity;
namespace Domain.Entities;

public class ApplicationUser : IdentityUser {
    [ProtectedPersonalData] public int CountryId { get; set; }
    [ProtectedPersonalData] public string Token { get; set; }
    [PersonalData] public string FirstName { get; set; }
    [PersonalData] public string LastName { get; set; }
    [ProtectedPersonalData] public string Address { get; set; }
    [PersonalData] public int DisplayLanguageId { get; set; }
    [PersonalData] public double Latitude { get; set; }
    [PersonalData] public double Longitude { get; set; }
    public bool TermsApproved { get; set; }
}