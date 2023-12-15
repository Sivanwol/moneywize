using Domain.DTO.OTP;
using Domain.Entities;
using Domain.Persistence.Mock.Configs;
using Domain.Persistence.Mock.Services.Interfaces;
using Infrastructure.Enums;
namespace Domain.Persistence.Mock.Services;

public class MockOTPServices : IOTPMockService {
    public MockOTPServices() {
        FakerRequester = new RequestOTPMockConfig();
        FakerVerify = new VerifyOTPMockConfig();
    }
    private RequestOTPMockConfig FakerRequester { get; set; }
    private VerifyOTPMockConfig FakerVerify { get; set; }
    private ApplicationUser User { get; set; }

    public RequestOTPResponseData RequestOTP(string phoneNumber, int countryId, MFAProvider provider) {
        return FakerRequester.Generate(1).First();
    }

    public VerifyOTPResponseData VerifyOTP(string code, string OTPtoken, string UserToken) {
        return FakerVerify.Generate(1).First();
    }
}