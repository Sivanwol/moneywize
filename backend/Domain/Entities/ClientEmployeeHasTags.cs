using System.ComponentModel.DataAnnotations;
using Domain.Entities.Common;
namespace Domain.Entities;

public class ClientEmployeeHasTags : BaseEntityWithoutId {
    [Key]
    public int EmployeeId { get; set; }

    public ClientEmployees Employee { get; set; }

    [Key]
    public int TagId { get; set; }

    public Tags Tag { get; set; }
}