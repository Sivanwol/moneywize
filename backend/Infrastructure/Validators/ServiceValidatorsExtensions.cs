using FluentValidation;
using Infrastructure.Validators.Account;
using Infrastructure.Validators.Backoffice.Account;
using Microsoft.Extensions.DependencyInjection;
namespace Infrastructure.Validators;

public static class ServiceValidatorsExtensions {
    public static void AddValidatorsExtension(this IServiceCollection services) {

        #region Auth Backend

        services.AddValidatorsFromAssemblyContaining<ForgetPasswordValidator>();
        services.AddValidatorsFromAssemblyContaining<RegisterUserValidator>();
        services.AddValidatorsFromAssemblyContaining<ResetPasswordValidator>();
        services.AddValidatorsFromAssemblyContaining<RefreshTokenValidator>();
        services.AddValidatorsFromAssemblyContaining<LoginUserValidator>();
        services.AddValidatorsFromAssemblyContaining<SendCodeToProviderValidator>();
        services.AddValidatorsFromAssemblyContaining<SendCodeFromProviderValidator>();

        #endregion

        #region Auth Account

        services.AddValidatorsFromAssemblyContaining<RequestOtpValidator>();
        services.AddValidatorsFromAssemblyContaining<VerifyOTPValidator>();

        #endregion


    }
}