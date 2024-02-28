using System.ComponentModel;
namespace Infrastructure.GQL.Inputs.User;

public class UserPreferenceInput {
    [Description("Preference Key")]
    public string Key { get; set; }

    [Description("Preference Value")]
    public string Value { get; set; }
}