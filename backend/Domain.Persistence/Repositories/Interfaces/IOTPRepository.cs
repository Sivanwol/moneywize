using Domain.Entities;
using Domain.Entities.OTP;
using Domain.Persistence.Repositories.Common.Interfaces;
using Infrastructure.Enums;
namespace Domain.Persistence.Repositories.Interfaces;

public interface IOTPRepository : IGenericEmptyRepository<ApplicationUser> {
    Task<RequestOTPResponseData> RequestOTP(Countries country, ApplicationUser user, ApplicationUserOtpCodes codeEntity, MFAProvider provider);
    Task<VerifyOTPResponseData> VerifyOTP(ApplicationUser user, ApplicationUserOtpCodes codeEntity, string code);
}