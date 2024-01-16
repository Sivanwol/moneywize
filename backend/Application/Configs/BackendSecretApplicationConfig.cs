using DotEnv.Core;
namespace Application.Configs;

public class BackendSecretApplicationConfig {
    [EnvKey("JwtSecret")]
    public string JwtSecret { get; set; }

    [EnvKey("EMailPassword")]
    public string EMailPassword { get; set; }

    [EnvKey("EMailUser")]
    public string EMailUser { get; set; }

    [EnvKey("SMSProviderInfouToken")]
    public string SMSProviderInfouToken { get; set; }
}