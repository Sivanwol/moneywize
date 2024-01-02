using Infrastructure.Enums;
namespace Infrastructure.GQL.Inputs.Common;

public class EntityFilterItem {
    public string FieldName { get; set; }
    public EntityColumnOperationType OperationType { get; set; }
    public FilterOperations FilterOperation { get; set; }
    public dynamic Value { get; set; }
    public ICollection<dynamic>? Values { get; set; }
}