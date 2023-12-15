namespace Infrastructure.Requests.Processor.Services.User;

public class ForgotPasswordRequest {
    public string Email { get; set; }
    public string Phone { get; set; }
}