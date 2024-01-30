using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GraphQL.AspNet.Attributes;
namespace Infrastructure.GQL.Inputs.View;

[Description("View Create Object input")]
public class ViewCreateInput {

    [Description("View client key that is used to based the new view on")]
    [GraphField(TypeExpression = "Guid!")]
    [Required]
    public Guid FromViewClientKey { get; set; }

    [Description("View name")]
    [GraphField(TypeExpression = "String!")]
    [Required]
    public string Name { get; set; }

    [Description("View Description")]
    [GraphField(TypeExpression = "String")]
    public string? Description { get; set; }

    [Description("Is view shareable")]
    [GraphField(TypeExpression = "Boolean!")]
    public bool IsShareAble { get; set; }

    [Description("View Color (hex) (e.g. #000000) this will show as small dot in the view list")]
    [GraphField(TypeExpression = "String")]
    public string? Color { get; set; }
}