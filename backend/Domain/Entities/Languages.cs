using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Common;
namespace Domain.Entities;

[Table("Languages")]
public class Languages : BaseEntity {
    [Column(TypeName = "varchar(20)")]
    public string Name { get; set; }

    [Column(TypeName = "varchar(5)")]
    public string Code { get; set; }

    public DateTime? SupportedAt { get; set; }
}