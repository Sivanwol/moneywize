using System.ComponentModel.DataAnnotations;
namespace Domain.Entities;

public class ActivityLog {
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Activity { get; set; }
    public string UserId { get; set; }
    public string Details { get; set; }
    public int EntityId { get; set; }
    public string EntityType { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
}