namespace Domain.Entities.OTP;

public class VerifyOTPResponseData {
    public ApplicationUser? User { get; set; }
    public bool IsValid { get; set; }
}