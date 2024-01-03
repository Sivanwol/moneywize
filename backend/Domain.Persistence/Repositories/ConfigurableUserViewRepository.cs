using Application.Exceptions;
using Domain.DTO.ConfigurableEntities;
using Domain.Entities;
using Domain.Persistence.Context;
using Domain.Persistence.Repositories.Common;
using Domain.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;
namespace Domain.Persistence.Repositories;

public class ConfigurableUserViewRepository : BaseRepository, IConfigurableUserViewRepository {
    public ConfigurableUserViewRepository(IDomainContext context) : base(context) { }
    public async Task<ConfigurableUserView?> GetConfigurableUserView(int viewId) {
        Log.Logger.Information($"Fetching configurable user view for system view {viewId}");
        var userView = await Context.ConfigurableUserViews.FirstOrDefaultAsync(w => w.Id == viewId && w.DeletedAt == null && w.IsPredefined == true);
        return userView;
    }
    public async Task<ConfigurableUserView?> GetConfigurableUserView(string userId, int viewId) {
        Log.Logger.Information($"Fetching configurable user view for user {userId} and view {viewId}");
        var userView = await Context.ConfigurableUserViews.FirstOrDefaultAsync(w => w.UserId == userId && w.Id == viewId && w.DeletedAt == null && w.IsPredefined == false);
        return userView;
    }
    public async Task<bool> HasViewExist(string userId, int viewId) {
        Log.Logger.Information($"Checking if view {viewId} exists for user {userId}");
        var userView = await Context.ConfigurableUserViews.FirstOrDefaultAsync(w => w.UserId == userId && w.Id == viewId && w.IsPredefined == false && w.DeletedAt == null);
        return userView != null;
    }
    public async Task<bool> HasViewDefault(int viewId) {
        Log.Logger.Information($"Checking if view {viewId} is default for system");
        var view = await Context.ConfigurableUserViews.FirstOrDefaultAsync(w => w.Id == viewId && w.DeletedAt == null && w.IsPredefined == true);
        return view != null;
    }
    public async Task<IEnumerable<ConfigurableUserViewColumn>> GetConfigurableViewColumns(string userId, int viewId) {
        Log.Logger.Information($"Fetching configurable user view columns for user {userId} and view {viewId}");
        var fetchingView = (from view in Context.ConfigurableUserViews
            where view.Id == viewId && view.UserId == userId && view.DeletedAt == null && view.IsPredefined == false
            select view).FirstOrDefault();
        if (fetchingView == null) {
            Log.Logger.Error($"View {viewId} not found for user {userId}");
            throw new EntityNotFoundException(nameof(ConfigurableUserView), $"[{userId}]-[{viewId}]");
        }
        var selectedColumns = (from vc in Context.ConfigurableUserViewHasConfigurableEntityColumnDefinitions
            where vc.ConfigurableUserViewId == fetchingView.Id && vc.ConfigurableEntityColumnDefinition.DisabledAt == null
            orderby vc.Order descending
            select new ConfigurableUserViewColumn() {
                ColumnName = vc.ConfigurableEntityColumnDefinition.ColumnName,
                Permissions = vc.ConfigurableEntityColumnDefinition.Permissions,
                IsFilterable = vc.ConfigurableEntityColumnDefinition.IsFilterable,
                IsSortable = vc.ConfigurableEntityColumnDefinition.IsSortable,
                IsHidden = vc.ConfigurableEntityColumnDefinition.IsHidden,
                DataType = vc.ConfigurableEntityColumnDefinition.DataType,
                FilterOperationType = vc.ConfigurableEntityColumnDefinition.FilterOperationType,
                Order = vc.Order
            }).ToList();
        return selectedColumns;
    }

    public async Task<IEnumerable<ConfigurableUserViewColumn>> GetConfigurableViewColumns(int viewId) {
        Log.Logger.Information($"Fetching configurable user view columns for system view {viewId}");
        var fetchingView = (from view in Context.ConfigurableUserViews
            where view.Id == viewId && view.DeletedAt == null && view.IsPredefined == true
            select view).FirstOrDefault();
        if (fetchingView == null) {
            Log.Logger.Error($"View {viewId} not found for system");
            throw new EntityNotFoundException(nameof(ConfigurableUserView), $"[{viewId}]");
        }
        var selectedColumns = (from vc in Context.ConfigurableUserViewHasConfigurableEntityColumnDefinitions
            where vc.ConfigurableUserViewId == fetchingView.Id && vc.ConfigurableEntityColumnDefinition.DisabledAt == null
            orderby vc.Order descending
            select new ConfigurableUserViewColumn() {
                ColumnName = vc.ConfigurableEntityColumnDefinition.ColumnName,
                Permissions = vc.ConfigurableEntityColumnDefinition.Permissions,
                IsFilterable = vc.ConfigurableEntityColumnDefinition.IsFilterable,
                IsSortable = vc.ConfigurableEntityColumnDefinition.IsSortable,
                IsHidden = vc.ConfigurableEntityColumnDefinition.IsHidden,
                DataType = vc.ConfigurableEntityColumnDefinition.DataType,
                FilterOperationType = vc.ConfigurableEntityColumnDefinition.FilterOperationType,
                Order = vc.Order
            }).ToList();
        return selectedColumns;
    }
}