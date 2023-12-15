using Domain.DTO.OTP;
using Infrastructure.Enums;
namespace Domain.Persistence.Mock.Services.Interfaces;

public interface IOTPMockService {
    RequestOTPResponseData RequestOTP(string phoneNumber, int countryId, MFAProvider provider);
    VerifyOTPResponseData VerifyOTP(string code, string OTPtoken, string UserToken);
}