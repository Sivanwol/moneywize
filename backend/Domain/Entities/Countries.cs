using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Common;
using Infrastructure.Enums;
namespace Domain.Entities;

[Table("Countries")]
public class Countries : BaseEntity {
    [Column(TypeName = "varchar(20)")]
    public string CountryName { get; set; }

    [Column(TypeName = "varchar(5)")]
    public string CountryCode { get; set; }

    [Column(TypeName = "varchar(5)")]
    public string CountryNumber { get; set; }

    public DateTime? SupportedAt { get; set; }
    public SMSProviders? Provider { get; set; }
}