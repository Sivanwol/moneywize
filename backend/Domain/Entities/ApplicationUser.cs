using System.ComponentModel.DataAnnotations;
using Application.Queries;
using Infrastructure.GQL;
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
    public bool TermsApproved { get; set; } = true;
    public ICollection<Clients> Clients { get; set; }
    public ICollection<ActivityLog> ActivityLogs { get; set; }
    public DateTime? RegisterCompletedAt { get; set; }
    public virtual ICollection<IdentityUserRole<string>> UserRoles { get; set; }
    public DateTime? DeletedAt { get; }
    public User ToGql(ICollection<string> roles) {
        return new() {
            Id = Id,
            Email = Email,
            FirstName = FirstName,
            LastName = LastName,
            PhoneNumber = PhoneNumber,
            Address = Address,
            Country = Country?.ToGql(),
            DisplayLanguage = DisplayLanguage?.ToGql(),
            Roles = roles,
            RegisterCompletedAt = RegisterCompletedAt.GetValueOrDefault()
        };
    }

    public User ToGql() {
        return new() {
            Id = Id,
            Email = Email,
            FirstName = FirstName,
            LastName = LastName,
            PhoneNumber = PhoneNumber,
            Address = Address,
            Country = Country?.ToGql(),
            DisplayLanguage = DisplayLanguage?.ToGql(),
            Roles = new List<string>(),
            RegisterCompletedAt = RegisterCompletedAt.GetValueOrDefault()
        };
    }
}