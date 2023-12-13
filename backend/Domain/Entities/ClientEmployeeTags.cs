using Domain.Common;
namespace Domain.Entities;

public class ClientEmployeeTags : BaseEntityWithoutId {
    public Guid EmployeeId { get; set; }
    public ClientEmployees Employee { get; set; }
    public Guid TagId { get; set; }
    public Tags Tag { get; set; }
}