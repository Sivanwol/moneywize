using Application.Configs;
using Application.Constraints;
using Application.Exceptions;
using Application.Utils;
using Domain.DTO.Client;
using Domain.Entities;
using Domain.Persistence.Repositories.Interfaces;
using Infrastructure.Enums;
using Infrastructure.Requests.Processor.Services.Client;
using Infrastructure.Services.Cache;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Serilog;
namespace Processor.Services.Client;

public class CreateClientHandler : IRequestHandler<CreateClientRequest> {
    private readonly IActivityLogRepository _activityLogRepository;
    private readonly IApplicationUserRepository _applicationUserRepository;
    private readonly ICacheService _cacheService;
    private readonly IClientRepository _clientRepository;
    private readonly BackendApplicationConfig _config;
    private readonly ICountriesRepository _countriesRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    public CreateClientHandler(
        IApplicationUserRepository userRepository,
        ICacheService cacheService,
        IActivityLogRepository activityRepository,
        ICountriesRepository countriesRepository,
        IClientRepository clientRepository,
        BackendApplicationConfig config,
        UserManager<ApplicationUser> userManager) {
        _applicationUserRepository = userRepository;
        _activityLogRepository = activityRepository;
        _userManager = userManager;
        _clientRepository = clientRepository;
        _cacheService = cacheService;
        _config = config;
        _countriesRepository = countriesRepository;
    }
    public async Task Handle(CreateClientRequest request, CancellationToken cancellationToken) {
        await ValidateRequest(request);
        Log.Logger.Information($"CreateClientHandler: Clear Cache");
        var cacheKey = Cache.GetKey($"Clients");
        await _cacheService.RemovePatternAsync(cacheKey);
        Log.Logger.Information($"CreateClientHandler: Register new client");
        var client = new CreateClient {
            OwnerUserId = request.LoggedInUserId.ToString(),
            Name = request.Name,
            Description = request.Description,
            Website = request.Website,
            CountryId = request.CountryId,
            Address = request.Address,
            City = request.City
        };
        var clientId = 0;
        if (request.OwnClientId.HasValue) {
            clientId = await _clientRepository.Add(request.OwnClientId.Value, client);
        } else {
            clientId = await _clientRepository.Add(client);
        }
        await _activityLogRepository.AddActivity(
            request.LoggedInUserId,
            clientId.ToString(),
            nameof(Clients),
            ActivityLogOperationType.ClientCreate,
            $"Client {client.Name} been create", $"Client {client.Name} been create by {request.LoggedInUserId} with id {clientId}",
            ActivityStatus.Success,
            request.IpAddress,
            request.UserAgent);
    }

    private async Task ValidateRequest(CreateClientRequest request) {
        if (Guid.Empty == request.LoggedInUserId) {
            Log.Logger.Error($"GetUserProfileHandler: Logged in User Id is Invalid");
            throw new EntityNotFoundException(nameof(ApplicationUser), request.LoggedInUserId.ToString());
        }
        var loggedUser = await _applicationUserRepository.GetById(request.LoggedInUserId);
        if (loggedUser == null) {
            Log.Logger.Error($"GetUserProfileHandler: Logged user not found");
            throw new EntityNotFoundException(nameof(ApplicationUser), request.LoggedInUserId.ToString());
        }
        var loggedRoles = await _userManager.GetRolesAsync(loggedUser);
        var matchedRoles = new List<string>() {
            Roles.Admin
        };
        if (!matchedRoles.Any(x => loggedRoles.Contains(x))) {
            Log.Logger.Error($"GetUserProfileHandler: User not authorized");
            throw new AuthorizationException();
        }

        var country = await _countriesRepository.GetById(request.CountryId);
        if (country == null) {
            Log.Logger.Error($"GetUserProfileHandler: Country not found");
            throw new EntityNotFoundException(nameof(Countries), request.CountryId.ToString());
        }
    }
}