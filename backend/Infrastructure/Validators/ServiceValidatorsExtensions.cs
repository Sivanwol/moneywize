using FluentValidation;
using Infrastructure.Validators.Account;
using Microsoft.Extensions.DependencyInjection;
namespace Infrastructure.Validators;

public static class ServiceValidatorsExtensions {
    public static void AddValidatorsExtension(this IServiceCollection services) {

        #region Auth Account

        services.AddValidatorsFromAssemblyContaining<ForgetPasswordValidator>();
        services.AddValidatorsFromAssemblyContaining<RegisterUserValidator>();
        services.AddValidatorsFromAssemblyContaining<ResetPasswordValidator>();
        services.AddValidatorsFromAssemblyContaining<RefreshTokenValidator>();
        services.AddValidatorsFromAssemblyContaining<LoginUserValidator>();
        services.AddValidatorsFromAssemblyContaining<SendCodeToProviderValidator>();
        services.AddValidatorsFromAssemblyContaining<SendCodeFromProviderValidator>();
        services.AddValidatorsFromAssemblyContaining<RequestOtpValidator>();
        services.AddValidatorsFromAssemblyContaining<VerifyOTPValidator>();

        #endregion


    }
}