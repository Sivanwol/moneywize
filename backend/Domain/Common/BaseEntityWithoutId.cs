namespace Domain.Common;

public abstract class BaseEntityWithoutId {
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}