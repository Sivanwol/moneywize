using Application.Configs.Providers;
using Infrastructure.Enums;
using Infrastructure.Utils.SMS.Providers;
using Microsoft.Extensions.Logging;
namespace Infrastructure.Utils.SMS;

public class SMSSender {
    static BaseSMSProvider _provider;
    static SMSSender _instance;
    private static ILoggerFactory _logger;
    private static InfoUSmsConfig _config;

    public static SMSSender Instance
    {
        get { return _instance ??= new SMSSender(); }
    }

    public void Load(ILoggerFactory logger, InfoUSmsConfig config) {
        _logger = logger;
        _config = config;
    }
    public int GetSMSExpireInMinutes() {
        return _config.ExpiredInMinutes;
    }
    public void Init(SMSProviders provider, string countryNumber, string phoneNumber, string messageTemplate) {
        switch (provider) {
            case SMSProviders.INFOU:
                _provider = new InfoUProvider(_logger, _config);
                break;
        }
        _provider.Init(countryNumber, phoneNumber, messageTemplate);
    }
    public void SendSMS(string code) {
        _provider.SendSMS(code);
    }
}