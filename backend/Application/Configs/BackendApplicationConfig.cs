namespace Application.Configs;

public class BackendApplicationConfig : BaseConfig {
    public bool DisableHealthCheck { get; set; }
    public string RabbitMqHost { get; set; }
    public string RabbitMqUsername { get; set; }
    public string RabbitMqPassword { get; set; }
    public string RabbitMqVirtualHost { get; set; }
    public string JwtIssuer { get; set; }
    public string JwtAudience { get; set; }
    public string JwtSecret { get; set; }
    public int JwtAccessTokenExpired { get; set; }
    public int JwtRefreshTokenExpired { get; set; }
}