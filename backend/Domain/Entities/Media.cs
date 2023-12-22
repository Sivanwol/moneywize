using System.ComponentModel.DataAnnotations;
using Domain.Entities.Common;
namespace Domain.Entities;

public class Media : BaseEntity {
    [StringLength(500)]
    public string FileName { get; set; }

    [StringLength(500)]
    public string Path { get; set; }

    [StringLength(9)]
    public string Extension { get; set; }

    [StringLength(20)]
    public string MimeType { get; set; }

    public int Size { get; set; }
}