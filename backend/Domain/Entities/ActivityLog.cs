using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Domain.Entities;

public enum ActivityOperationType {
    SYSTEM,
    USER
}

[Table("ActivityLogs")]
public class ActivityLog {


    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [StringLength(36)]
    public string? UserId { get; set; }

    public ApplicationUser? User { get; set; }

    [Required]
    [StringLength(36)]
    public Guid EntityId { get; set; }

    [Required]
    [MaxLength(100)]
    public string EntityType { get; set; }

    [Required]
    [MaxLength(20)]
    public string OperationType { get; set; } = nameof(ActivityOperationType.USER);

    [Required]
    [MaxLength(500)]
    public string Activity { get; set; }

    [DataType(DataType.Text)]
    public string Details { get; set; }

    [Required]
    [MaxLength(100)]
    public string Status { get; set; }

    [MaxLength(30)]
    public string IpAddress { get; set; }

    [MaxLength(100)]
    public string UserAgent { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; }
}