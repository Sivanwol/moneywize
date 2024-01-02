using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Common;
using Infrastructure.Enums;
using Infrastructure.GQL;
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

    public Country ToGql() {
        return new() {
            Id = Id,
            CountryName = CountryName,
            CountryCode = CountryCode,
            CountryNumber = CountryNumber
        };
    }
}