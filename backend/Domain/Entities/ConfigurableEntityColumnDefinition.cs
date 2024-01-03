using System.ComponentModel.DataAnnotations.Schema;
using Domain.DTO.ConfigurableEntities;
using Domain.Entities.Common;
using Infrastructure.Enums;
namespace Domain.Entities;

public class ConfigurableEntityColumnDefinition : BaseEntity {
    [Column(TypeName = "nvarchar(100)")]
    public string EntityName { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public string? ColumnName { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public string DisplayName { get; set; }

    [Column(TypeName = "text")]
    public string Description { get; set; }

    [ForeignKey("CategoryId")]
    public int CategoryId { get; set; }

    public ConfigurableCategories Category { get; set; }
    public EntityColumnDataType DataType { get; set; } = EntityColumnDataType.String;
    public EntityColumnOperationType FilterOperationType { get; set; } = EntityColumnOperationType.StringFilterField;
    public bool IsHidden { get; set; } = false; // is this column can be updated
    public bool IsSortable { get; set; }
    public bool IsFilterable { get; set; }
    public DateTime? DisabledAt { get; set; }

    [Column(TypeName = "json")]
    public ICollection<string> Permissions { get; set; }

    public bool IsVirtualColumn { get; set; }

    [Column(TypeName = "json")]
    public ICollection<ConfigurableEntityMetaData> MetaData { get; set; }

    public int Order { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public string AffectedBy { get; set; }

    public IEnumerable<ConfigurableUserViewHasConfigurableEntityColumnDefinition>? Views { get; set; }
}