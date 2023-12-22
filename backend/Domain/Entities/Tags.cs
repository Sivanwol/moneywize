using System.ComponentModel.DataAnnotations;
using Domain.Entities.Common;
namespace Domain.Entities;

public class Tags : BaseEntity {
    [StringLength(50)]
    public string Name { get; set; }

    public ICollection<ClientEmployeeHasTags> ClientEmployees { get; set; }
    public ICollection<ClientNetworkHasTags> ClientNetworks { get; set; }
    public ICollection<ClientServerHasTags> ClientServers { get; set; }
}