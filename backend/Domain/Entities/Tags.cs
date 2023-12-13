using System.ComponentModel.DataAnnotations;
using Domain.Common;
namespace Domain.Entities;

public class Tags : BaseEntity {
    [StringLength(50)]
    public string Name { get; set; }

    public ICollection<ClientTags> Clients { get; set; }
    public ICollection<ClientEmployeeTags> ClientEmployees { get; set; }
    public ICollection<ClientNetworkTags> ClientNetworks { get; set; }
    public ICollection<ClientServerTags> ClientServers { get; set; }
}