using Application.Constraints;
using Application.Exceptions;
using Domain.DTO.Filters;
using Domain.DTO.Filters.Fields;
using Domain.Entities;
using Domain.Persistence.Repositories.Interfaces;
using Infrastructure.Enums;
using Infrastructure.GQL;
using Infrastructure.GQL.Common;
using Infrastructure.Requests.Processor.Services.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Serilog;
namespace Processor.Services.User;

public class UserActivityHandler : IRequestHandler<UserActivityRequest, EntityPage<Activity>> {
    private readonly IActivityLogRepository _activityRepository;
    private readonly IApplicationUserRepository _applicationUserRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    public UserActivityHandler(
        IApplicationUserRepository userRepository,
        IActivityLogRepository activityRepository,
        UserManager<ApplicationUser> userManager) {
        _activityRepository = activityRepository;
        _applicationUserRepository = userRepository;
        _userManager = userManager;
    }
    public async Task<EntityPage<Activity>> Handle(UserActivityRequest request, CancellationToken cancellationToken) {
        var user = await _applicationUserRepository.GetById(request.UserId);
        if (user == null) {
            throw new EntityNotFoundException(nameof(ApplicationUser), request.UserId.ToString());
        }
        if (request.UserId != request.LoggedInUserId) {
            var loggedUser = await _applicationUserRepository.GetById(request.LoggedInUserId);
            if (loggedUser == null) {
                Log.Logger.Error($"GetUserProfileHandler: Logged user not found");
                throw new EntityNotFoundException(nameof(ApplicationUser), request.LoggedInUserId.ToString());
            }
            var loggedRoles = await _userManager.GetRolesAsync(loggedUser);
            var matchedRoles = new List<string>() {
                Roles.Admin,
                Roles.IT
            };
            if (!matchedRoles.Any(x => loggedRoles.Contains(x))) {
                Log.Logger.Error($"GetUserProfileHandler: User not authorized");
                throw new AuthorizationException();
            }
        }
        var pageFilter = BuildPageFilter(request);
        var activities = await _activityRepository.GetActivities(pageFilter);
        var totalPages = await _activityRepository.GetActivitiesTotalPages(pageFilter);
        var totalRecords = await _activityRepository.GetActivitiesCount(pageFilter);
        return new EntityPage<Activity>() {
            TotalPages = totalPages,
            TotalRecords = totalRecords,
            HasPreviousPage = (totalPages > 1 && pageFilter.Page > 1),
            HasNextPage = (totalPages > pageFilter.Page),
            Records = activities.Select(x => x.ToGql())
        };
    }

    private RecordFilterPagination<ActivityLogFilters> BuildPageFilter(UserActivityRequest request) {
        var pageFilter = new RecordFilterPagination<ActivityLogFilters>() {
            Page = request.PageControl.Page,
            PageSize = (int)request.PageControl.PageSize,
            SortByField = request.PageControl.SortByField!,
            SortDirection = request.PageControl.SortDirection,
            FilterCollectionOperation = request.PageControl.FilterCollectionOperation
        };

        pageFilter.Filters.UserIdFilters.Add(new StringFilterField("UserId", FilterStringOperation.Equal) {
            FilterValue = request.UserId.ToString()
        });
        foreach (var filter in request.PageControl.Filters) {
            if (IsMultiValueFilter(filter.FilterOperation)) {
                switch (filter.FieldName) {
                    case "IpAddress":
                        pageFilter.Filters.IpAddressFilters.Add(new StringFilterField(filter.FieldName, (FilterStringOperation)filter.FilterOperation) {
                            FilterValues = filter.Values
                        });
                        break;
                    case "UserAgent":
                        pageFilter.Filters.UserAgentFilters.Add(new StringFilterField(filter.FieldName, (FilterStringOperation)filter.FilterOperation) {
                            FilterValues = filter.Values
                        });
                        break;
                    case "EntityId":
                        pageFilter.Filters.EntityIdFilters.Add(new StringFilterField(filter.FieldName, (FilterStringOperation)filter.FilterOperation) {
                            FilterValues = filter.Values
                        });
                        break;
                    case "OperationType":
                        pageFilter.Filters.OperationTypeFilters.Add(new StringFilterField(filter.FieldName, (FilterStringOperation)filter.FilterOperation) {
                            FilterValues = filter.Values
                        });
                        break;
                    case "Activity":
                        pageFilter.Filters.ActivityFilters.Add(new StringFilterField(filter.FieldName, (FilterStringOperation)filter.FilterOperation) {
                            FilterValues = filter.Values
                        });
                        break;
                    case "Status":
                        pageFilter.Filters.StatusFilters.Add(new StringFilterField(filter.FieldName, (FilterStringOperation)filter.FilterOperation) {
                            FilterValues = filter.Values
                        });
                        break;
                    case "Id":
                        pageFilter.Filters.IdFilters.Add(new IdFilterField(filter.FieldName, (FilterIDOperation)filter.FilterOperation) {
                            FilterValues = filter.Values?.Select(x => Convert.ToInt32(x)).ToList()
                        });
                        break;

                    case "CreatedAt":
                        pageFilter.Filters.CreatedAtFilters.Add(new DateFilterField(filter.FieldName, (FilterDateOperation)filter.FilterOperation) {
                            FilterValues = filter.Values?.Select(x => Convert.ToDateTime(x)).ToList()
                        });
                        break;
                }
            } else {
                switch (filter.FieldName) {
                    case "IpAddress":
                        pageFilter.Filters.IpAddressFilters.Add(new StringFilterField(filter.FieldName, (FilterStringOperation)filter.FilterOperation) {
                            FilterValue = filter.Value
                        });
                        break;
                    case "UserAgent":
                        pageFilter.Filters.UserAgentFilters.Add(new StringFilterField(filter.FieldName, (FilterStringOperation)filter.FilterOperation) {
                            FilterValue = filter.Value
                        });
                        break;
                    case "EntityId":
                        pageFilter.Filters.EntityIdFilters.Add(new StringFilterField(filter.FieldName, (FilterStringOperation)filter.FilterOperation) {
                            FilterValue = filter.Value
                        });
                        break;
                    case "OperationType":
                        pageFilter.Filters.OperationTypeFilters.Add(new StringFilterField(filter.FieldName, (FilterStringOperation)filter.FilterOperation) {
                            FilterValue = filter.Value
                        });
                        break;
                    case "Activity":
                        pageFilter.Filters.ActivityFilters.Add(new StringFilterField(filter.FieldName, (FilterStringOperation)filter.FilterOperation) {
                            FilterValue = filter.Value
                        });
                        break;
                    case "Status":
                        pageFilter.Filters.StatusFilters.Add(new StringFilterField(filter.FieldName, (FilterStringOperation)filter.FilterOperation) {
                            FilterValue = filter.Value
                        });
                        break;
                    case "Id":
                        pageFilter.Filters.IdFilters.Add(new IdFilterField(filter.FieldName, (FilterIDOperation)filter.FilterOperation) {
                            FilterValue = Convert.ToInt32(filter.Value)
                        });
                        break;

                    case "CreatedAt":
                        pageFilter.Filters.CreatedAtFilters.Add(new DateFilterField(filter.FieldName, (FilterDateOperation)filter.FilterOperation) {
                            FilterValue = Convert.ToDateTime(filter.Value)
                        });
                        break;
                }
            }
        }
        return pageFilter;
    }
    private bool IsMultiValueFilter(FilterOperations filterOperation) {
        return filterOperation is FilterOperations.In or FilterOperations.NotIn or FilterOperations.Between or FilterOperations.NotBetween;
    }
}