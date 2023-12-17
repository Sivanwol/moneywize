namespace Domain.DTO.User;

public class ForgetPasswordMail {
    public string Title { get; set; }
    public string UserToken { get; set; }
    public string HomePageUrl { get; set; }
    public string HostImageUrl { get; set; }
}