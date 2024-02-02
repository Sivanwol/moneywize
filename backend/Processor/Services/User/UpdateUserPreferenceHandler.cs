using Application.Configs;
using Application.Utils;
using Domain.Entities;
using Domain.Persistence.Repositories.Interfaces;
using Infrastructure.Enums;
using Infrastructure.Requests.Processor.Services.User;
using MediatR;
using Serilog;
namespace Processor.Services.User;

public class UpdateUserPreferenceHandler : IRequestHandler<UpdateUserPreferenceRequest, bool> {
    private readonly IActivityLogRepository _activityLogRepository;
    private readonly IApplicationUserRepository _applicationUserRepository;
    private readonly BackendApplicationConfig _config;
    public UpdateUserPreferenceHandler(IApplicationUserRepository applicationUserRepository,
        IActivityLogRepository activityLogRepository,
        BackendApplicationConfig config) {
        _applicationUserRepository = applicationUserRepository;
        _activityLogRepository = activityLogRepository;
        _config = config;
    }
    public async Task<bool> Handle(UpdateUserPreferenceRequest request, CancellationToken cancellationToken) {
        try {
            await _applicationUserRepository.UpdateUserPreference(request.UserId, request.Preferences);
            await _activityLogRepository.AddActivity(request.UserId, request.UserId.ToString(), nameof(ApplicationUserPreferences), ActivityLogOperationType.UserUpdatePreference, "User Update Preference", ActivityStatus.Success, request.IpAddress, request.UserAgent);
        }
        catch (Exception e) {
            Log.Logger.Error(e, e.Message);
            await _activityLogRepository.AddActivity(request.UserId, request.UserId.ToString(), nameof(ApplicationUserPreferences), ActivityLogOperationType.UserUpdatePreference, "User Update Preference", ActivityStatus.Failed, request.IpAddress, request.UserAgent);
            return false;
        }
        return true;
    }
}