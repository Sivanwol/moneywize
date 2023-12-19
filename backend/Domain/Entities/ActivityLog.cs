using System.ComponentModel.DataAnnotations;
namespace Domain.Entities;

public enum ActivityOperationType {
    SYSTEM,
    USER
}

public class ActivityLog {
    public string? IpAddress;
    public string? UserAgent;

    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid? UserId { get; set; }
    public Guid EntityId { get; set; }

    public string OperationType { get; set; } = nameof(ActivityOperationType.USER);
    public string Activity { get; set; }
    public string Details { get; set; }
    public string EntityType { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
}