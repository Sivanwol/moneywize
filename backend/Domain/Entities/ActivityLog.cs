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
    public Guid Id { get; set; } = Guid.NewGuid();

    [StringLength(36)]
    public string? UserId { get; set; }

    public ApplicationUser? User { get; set; }
    public Guid EntityId { get; set; }

    public string OperationType { get; set; } = nameof(ActivityOperationType.USER);
    public string Activity { get; set; }
    public string Details { get; set; }
    public string EntityType { get; set; }
    public string Status { get; set; }
    public string IpAddress { get; set; }
    public string UserAgent { get; set; }
    public DateTime CreatedAt { get; set; }
}