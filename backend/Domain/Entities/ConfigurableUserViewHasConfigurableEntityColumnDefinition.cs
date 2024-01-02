using Domain.Entities.Common;
namespace Domain.Entities;

public class ConfigurableUserViewHasConfigurableEntityColumnDefinition : BaseEntityWithoutId {
    public int ConfigurableUserViewId { get; set; }
    public ConfigurableUserView ConfigurableUserView { get; set; }
    public int ConfigurableEntityColumnDefinitionId { get; set; }
    public ConfigurableEntityColumnDefinition ConfigurableEntityColumnDefinition { get; set; }
    public bool IsPinable { get; set; }
    public int Order { get; set; }
}