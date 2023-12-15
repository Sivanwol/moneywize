using Infrastructure.Responses.Controllers.Auth;
using MediatR;
namespace Infrastructure.Requests.Processor.Services.User;

public class LoginWebUserRequest : IRequest<LoginResponse> {
    public string Email { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}