using Domain.Persistence.Repositories.Interfaces;
using Infrastructure.Requests.Processor.Services.User;
using Infrastructure.Responses.Controllers.User;
using MediatR;
using Serilog;
namespace Processor.Services.User;

public class GetUserProfileHandler : IRequestHandler<GetUserProfileRequest, ProfileResponse> {
    private readonly IAppUserRepository _appUserRepository;
    private readonly IMediator _mediator;

    public GetUserProfileHandler(IMediator mediator,
        IAppUserRepository appUserRepository) {
        _appUserRepository = appUserRepository;
        _mediator = mediator;
    }

    public async Task<ProfileResponse> Handle(GetUserProfileRequest request, CancellationToken cancellationToken) {
        Log.Logger.Information($"GetUserProfileHandler: {request.UserId}");
        //     var result = await _appUserRepository.GetUserProfile(request.UserId);
        //     return await Task.FromResult(new ProfileResponse {
        //         ID = result.User.Id,
        //         FirstName = result.User.FirstName,
        //         LastName = result.User.LastName,
        //         Email = result.User.Email!,
        //         PhoneNumber = result.User.PhoneNumber!,
        //         CountryId = result.User.CountryId,
        //         Latitude = result.User.Latitude,
        //         Longitude = result.User.Longitude,
        //         TermsApproved = result.User.TermsApproved,
        //         DisplayLanguageId = result.User.DisplayLanguageId,
        //     });
        // }
        return null;
    }
}