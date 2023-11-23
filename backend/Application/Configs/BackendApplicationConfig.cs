namespace Application.Configs;

public class BackendApplicationConfig : BaseConfig {
    public bool DisableHealthCheck { get; set; }
    public string AzureServiceBus { get; set; }
    public string AzureServiceBusUri { get; set; }

    #region JWT Configs

    public string JwtIssuer { get; set; }
    public string JwtAudience { get; set; }
    public string JwtSecret { get; set; }
    public int JwtAccessTokenExpired { get; set; }
    public int JwtRefreshTokenExpired { get; set; }

    #endregion

    #region RabbitMQ Configs

    public string RabbitMqHost { get; set; }
    public string RabbitMqVirtualHost { get; set; }
    public string RabbitMqUsername { get; set; }
    public string RabbitMqPassword { get; set; }

    #endregion

    #region Infou Sms Provider Configs

    public int SMSCodeExpiredInMinutes { get; set; }
    public int SMSOtpTotalRetriesWithinSession { get; set; }
    public string SMSProviderInfouToken { get; set; }
    public string SMSProviderInfouSender { get; set; }
    public string SMSProviderInfouUserName { get; set; }

    #endregion

}