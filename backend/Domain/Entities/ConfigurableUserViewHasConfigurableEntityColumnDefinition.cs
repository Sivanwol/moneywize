using Domain.Entities.Common;
namespace Domain.Entities;

public class ConfigurableUserViewHasConfigurableEntityColumnDefinition : BaseEntityWithoutId {
    public int ConfigurableUserViewId { get; set; }
    public ConfigurableUserView ConfigurableUserView { get; set; }
    public int ConfigurableEntityColumnDefinitionId { get; set; }
    public ConfigurableEntityColumnDefinition ConfigurableEntityColumnDefinition { get; set; }

    public bool IsHidden { get; set; } = false; // will it display in the grid
    public bool IsFixed { get; set; }
    public int Order { get; set; }
}