using System.ComponentModel.DataAnnotations;
using Domain.Common;
namespace Domain.Entities;

public class ClientEmployeeHasTags : BaseEntityWithoutId {
    [Key]
    public int EmployeeId { get; set; }

    public ClientEmployees Employee { get; set; }
    public int TagId { get; set; }

    [Key]
    public Tags Tag { get; set; }
}