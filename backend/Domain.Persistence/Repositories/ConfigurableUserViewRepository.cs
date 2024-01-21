using Application.Exceptions;
using Domain.Entities;
using Domain.Persistence.Context;
using Domain.Persistence.Repositories.Common;
using Domain.Persistence.Repositories.Interfaces;
using Infrastructure.GQL;
using Microsoft.EntityFrameworkCore;
using Serilog;
namespace Domain.Persistence.Repositories;

public class ConfigurableUserViewRepository : BaseRepository, IConfigurableUserViewRepository {
    public ConfigurableUserViewRepository(IDomainContext context) : base(context) { }
    public async Task<bool> HasViewExist(string userId, Guid viewClientId) {
        Log.Logger.Information($"Checking if view {viewClientId} exists for user {userId}");
        var userView = await Context.ConfigurableUserViews.FirstOrDefaultAsync(w =>
            w.UserId == userId && w.ClientUniqueKey == viewClientId && w.IsPredefined == false && w.DeletedAt == null);
        return userView != null;
    }
    public async Task<bool> HasViewDefault(Guid viewClientId) {
        Log.Logger.Information($"Checking if view {viewClientId} is default for system");
        var view = await Context.ConfigurableUserViews.FirstOrDefaultAsync(w => w.ClientUniqueKey == viewClientId && w.DeletedAt == null && w.IsPredefined == true);
        return view != null;
    }
    public async Task<ICollection<ViewColumn>> GetConfigurableViewColumns(ConfigurableUserView view, List<string>? permissions = null) {
        Log.Logger.Information($"Fetching configurable user view columns for user view [{view.UserId} ,{view.Id}]");
        List<ViewColumn> selectedColumns;
        if (permissions == null) {
            selectedColumns = (from vc in Context.ConfigurableUserViewHasConfigurableEntityColumnDefinitions
                where vc.ConfigurableUserViewId == view.Id && vc.ConfigurableEntityColumnDefinition.DisabledAt == null
                orderby vc.Order descending
                select new ViewColumn() {
                    DisplayName = vc.ConfigurableEntityColumnDefinition.DisplayName,
                    EntityName = vc.ConfigurableEntityColumnDefinition.EntityName,
                    FieldAlias = vc.ConfigurableEntityColumnDefinition.FieldAlias,
                    FieldPath = vc.ConfigurableEntityColumnDefinition.FieldPath,
                    ColumnName = vc.ConfigurableEntityColumnDefinition.ColumnName,
                    IsFilterable = vc.ConfigurableEntityColumnDefinition.IsFilterable,
                    IsSortable = vc.ConfigurableEntityColumnDefinition.IsSortable,
                    DataType = vc.ConfigurableEntityColumnDefinition.DataType,
                    FilterOperationType = vc.ConfigurableEntityColumnDefinition.FilterOperationType,
                    IsHidden = vc.IsHidden,
                    IsFixed = vc.IsFixed,
                    Order = vc.Order
                }).ToList();
            return selectedColumns;
        }
        selectedColumns = (from vc in Context.ConfigurableUserViewHasConfigurableEntityColumnDefinitions
            where vc.ConfigurableUserViewId == view.Id && vc.ConfigurableEntityColumnDefinition.DisabledAt == null &&
                  vc.ConfigurableEntityColumnDefinition.Permissions.Any(p => permissions.Contains(p))
            orderby vc.Order descending
            select new ViewColumn() {
                DisplayName = vc.ConfigurableEntityColumnDefinition.DisplayName,
                EntityName = vc.ConfigurableEntityColumnDefinition.EntityName,
                FieldAlias = vc.ConfigurableEntityColumnDefinition.FieldAlias,
                FieldPath = vc.ConfigurableEntityColumnDefinition.FieldPath,
                ColumnName = vc.ConfigurableEntityColumnDefinition.ColumnName,
                IsFilterable = vc.ConfigurableEntityColumnDefinition.IsFilterable,
                IsSortable = vc.ConfigurableEntityColumnDefinition.IsSortable,
                DataType = vc.ConfigurableEntityColumnDefinition.DataType,
                FilterOperationType = vc.ConfigurableEntityColumnDefinition.FilterOperationType,
                IsHidden = vc.IsHidden,
                IsFixed = vc.IsFixed,
                Order = vc.Order
            }).ToList();
        return selectedColumns;
    }

    public async Task<View> GetViewDefinition(string userId, Guid viewClientId, List<string> permissions = null) {
        ICollection<ViewColumn> columns;
        var isDefaultView = await HasViewDefault(viewClientId);
        ConfigurableUserView? view;
        if (isDefaultView) {
            view = await GetView(viewClientId, permissions);
            columns = await GetConfigurableViewColumns(view, permissions);
        } else {
            var hasView = await HasViewExist(userId, viewClientId);
            if (!hasView) {
                Log.Logger.Error($"ConfigurableUserViewRepository: View not found");
                throw new EntityNotFoundException(nameof(ConfigurableUserView), $"[{userId}-{viewClientId}]");
            }
            view = await GetView(userId, viewClientId, permissions);
            columns = await GetConfigurableViewColumns(view, permissions);
        }
        return ToView(view, columns);
    }

    public async Task<ICollection<View>> GetViews(string userId, List<string>? permissions = null) {
        Log.Logger.Information($"Fetching configurable user views for user {userId}");
        List<ConfigurableUserView> views;
        ICollection<View> result = new List<View>();
        if (permissions == null) {
            views = await Context.ConfigurableUserViews
                .Where(w => (w.UserId == userId && w.DeletedAt == null && w.IsPredefined == false) || (w.DeletedAt == null && w.IsPredefined == true)).ToListAsync();
        } else {
            views = await Context.ConfigurableUserViews.Where(w =>
                (w.UserId == userId && w.DeletedAt == null && w.IsPredefined == false && w.Permissions.Any(p => permissions.Contains(p))) ||
                (w.DeletedAt == null && w.IsPredefined == true && w.Permissions.Any(p => permissions.Contains(p)))).ToListAsync();
        }
        foreach (var view in views) {
            var columns = await GetConfigurableViewColumns(view, permissions);
            result.Add(ToView(view, columns));
        }
        return result;
    }

    private async Task<ConfigurableUserView?> GetView(string userId, Guid viewClientId, List<string> permissions = null) {
        Log.Logger.Information($"Fetching configurable user view for user {userId} and view {viewClientId}");
        var fetchingView = (from view in Context.ConfigurableUserViews
            where view.ClientUniqueKey == viewClientId && view.UserId == userId && view.DeletedAt == null && view.IsPredefined == false &&
                  view.Permissions.Any(p => permissions.Contains(p))
            select view).FirstOrDefault();
        if (fetchingView == null) {
            Log.Logger.Error($"View {viewClientId} not found for user {userId}");
            throw new EntityNotFoundException(nameof(ConfigurableUserView), $"[{userId}]-[{viewClientId}]");
        }
        return fetchingView;
    }
    private async Task<ConfigurableUserView> GetView(Guid viewClientId, List<string>? permissions = null) {
        Log.Logger.Information($"Fetching configurable user view for system view {viewClientId}");
        ConfigurableUserView? fetchingView;
        if (permissions != null) {
            fetchingView = (from view in Context.ConfigurableUserViews
                where view.ClientUniqueKey == viewClientId && view.DeletedAt == null && view.IsPredefined == true && view.Permissions.Any(p => permissions.Contains(p))
                select view).FirstOrDefault();
        } else {
            fetchingView = (from view in Context.ConfigurableUserViews
                where view.ClientUniqueKey == viewClientId && view.DeletedAt == null && view.IsPredefined == true
                select view).FirstOrDefault();
        }
        if (fetchingView == null) {
            Log.Logger.Error($"View {viewClientId} not found for system");
            throw new EntityNotFoundException(nameof(ConfigurableUserView), $"[{viewClientId}]");
        }
        return fetchingView;

    }

    private View ToView(ConfigurableUserView view, IEnumerable<ViewColumn> columns) {
        return new View() {
            ClientKey = view.ClientUniqueKey,
            Name = view.Name,
            Description = view.Description,
            IsPredefined = view.IsPredefined,
            IsShareAble = view.IsShareAble,
            Color = view.Color,
            Columns = columns
        };
    }
    private View ToView(ConfigurableUserView view) {
        return new View() {
            ClientKey = view.ClientUniqueKey,
            Name = view.Name,
            Description = view.Description,
            IsPredefined = view.IsPredefined,
            IsShareAble = view.IsShareAble,
            Color = view.Color
        };
    }
}