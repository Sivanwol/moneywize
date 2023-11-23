namespace Application.Exceptions;

public class MFAProviderImplementedException : BaseException {

    public MFAProviderImplementedException(string providerName) : base(providerName, StatusCodeErrors.NotFound,
        $"MFA provider {providerName} Not Found") { }
}