using Application.Contract;
using Application.Exceptions;
using Application.Responses.Base;
using Domain.DTO.User;
using Domain.Entities;
using Domain.Persistence.Repositories.Interfaces;
using Infrastructure.Requests.Processor.Services.User;
using Infrastructure.Services.Email;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Serilog;
namespace Processor.Services.User;

public class ForgetPasswordHandler : IRequestHandler<ForgotPasswordRequest, EmptyResponse> {
    private readonly IAppUserRepository _appUserRepository;
    private readonly ICountriesRepository _countriesRepository;
    private readonly IMailService _mailService;
    private readonly IMediator _mediator;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITemplateService _templateService;
    private readonly UserManager<ApplicationUser> _userManager;

    public ForgetPasswordHandler(IMediator mediator,
        UserManager<ApplicationUser> userManager,
        IAppUserRepository appUserRepository,
        ICountriesRepository countriesRepository,
        IMailService mailService,
        ITemplateService templateService,
        SignInManager<ApplicationUser> signInManager) {
        _mediator = mediator;
        _signInManager = signInManager;
        _userManager = userManager;
        _appUserRepository = appUserRepository;
        _countriesRepository = countriesRepository;
        _mailService = mailService;
        _templateService = templateService;
    }

    public async Task<EmptyResponse> Handle(ForgotPasswordRequest request, CancellationToken cancellationToken) {
        Log.Logger.Information($"ForgetPasswordHandler: [{request.Email}, {request.CountryId}, {request.Phone}]");
        if (string.IsNullOrEmpty(request.Email) && string.IsNullOrEmpty(request.Phone) && (request.CountryId == null || request.CountryId == Guid.Empty)) {
            throw new InvalidRequestException("Email or Phone is required");
        }
        var country = await _countriesRepository.GetById(request.CountryId!.Value);
        if (country == null) {
            throw new EntityNotFoundException("Country", request.CountryId.Value.ToString());
        }
        ApplicationUser? user = null;
        // find user by email
        if (!string.IsNullOrEmpty(request.Email)) {
            user = await _appUserRepository.LocateUserByEmail(request.Email);
            if (user == null) {
                throw new EntityNotFoundException("User", request.Email);
            }
            var template = await _templateService.GetTemplateHtmlAsStringAsync("UserForgetPassword", new ForgetPasswordMail {
                UserToken = user.Token,
                Title = "Forget Password",
                HomePageUrl = request.HostUrl,
                HostImageUrl = request.HostUrl + "/assets/",
            });
            await _mailService.SendAsync(new MailData(new List<string> {
                request.Email
            }, "Forget Password", template), cancellationToken);
        }
        Log.Logger.Information("Reset password Email Sent -  {UserId}", user!.Id);
        return new EmptyResponse();
    }
}