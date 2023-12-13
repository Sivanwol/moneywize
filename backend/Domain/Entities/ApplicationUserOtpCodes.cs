using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
namespace Domain.Entities;

[Table("ApplicationUserOtpCodes")]
public class ApplicationUserOtpCodes : BaseEntity {

    [ForeignKey("ApplicationUser")]
    public Guid UserId { get; set; }

    public ApplicationUser User { get; set; }
    public string Token { get; set; }
    public string Code { get; set; }
    public DateTime ExpirationDate { get; set; }
    public DateTime? ComplateAt { get; set; }

    public static string GenerateCode() {
        var random = new Random();
        var code = random.Next(100000, 999999).ToString();
        return code;
    }
}