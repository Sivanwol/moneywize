using Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Domain.Persistence.Context;

public interface IDomainContext : IDisposable {
    DbContext Instance { get; }
    DbSet<Countries> Countries { get; set; }
    DbSet<Languages> Languages { get; set; }
    DbSet<ApplicationUser> Users { get; set; }
    DbSet<ApplicationUserOtpCodes> UserOtpCodes { get; set; }
}