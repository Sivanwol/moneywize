using Application.Utils;
using FluentValidation;
using Infrastructure.Requests.Controllers.Auth;
namespace Infrastructure.Validators.Account;

public class ForgetPasswordValidator : AbstractValidator<ForgetPasswordRequest> {
    public ForgetPasswordValidator() {
        When(x => !string.IsNullOrEmpty(x.Email) && !(string.IsNullOrEmpty(x.Phone) && x.CountyId.HasValue), () => {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .MaximumLength(500)
                .WithMessage("Email must not exceed 500 characters")
                .EmailAddress()
                .WithMessage("Email is not valid");
        });
        When(x => string.IsNullOrEmpty(x.Email) && (!string.IsNullOrEmpty(x.Phone) && !x.CountyId.HasValue), () => {
            RuleFor(x => x.CountyId)
                .NotEmpty()
                .WithMessage("CountyId is required");
            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage("Phone is required")
                .MinimumLength(7)
                .WithMessage("Phone must not be less than 7 characters")
                .MaximumLength(15)
                .WithMessage("Phone must not exceed 15 characters")
                .MatchPhoneNumber();
        });
    }
}