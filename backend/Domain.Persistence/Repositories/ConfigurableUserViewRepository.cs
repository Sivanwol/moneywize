using Application.Exceptions;
using Domain.Entities;
using Domain.Persistence.Context;
using Domain.Persistence.Repositories.Common;
using Domain.Persistence.Repositories.Interfaces;
using Infrastructure.GQL;
using Infrastructure.GQL.Inputs.View;
using Infrastructure.Requests.Processor.Services.Views;
using Microsoft.EntityFrameworkCore;
using Serilog;
namespace Domain.Persistence.Repositories;

public class ConfigurableUserViewRepository : BaseRepository, IConfigurableUserViewRepository {
    public ConfigurableUserViewRepository(IDomainContext context) : base(context) { }
    public async Task<bool> HasViewExist(string userId, Guid viewClientId) {
        Log.Logger.Information("Checking if view {ViewClientId} exists for user {UserId}", viewClientId, userId);
        var userView = await Context.ConfigurableUserViews.FirstOrDefaultAsync(w =>
            w.UserId == userId && w.ClientUniqueKey == viewClientId && w.IsPredefined == false && w.DeletedAt == null);
        return userView != null;
    }
    public async Task<bool> HasViewDefault(Guid viewClientId) {
        Log.Logger.Information("Checking if view {ViewClientId} is default for system", viewClientId);
        var view = await Context.ConfigurableUserViews.FirstOrDefaultAsync(w => w.ClientUniqueKey == viewClientId && w.DeletedAt == null && w.IsPredefined == true);
        return view != null;
    }
    public async Task<ICollection<ViewColumn>> GetConfigurableViewColumns(ConfigurableUserView view, List<string>? permissions = null) {
        Log.Logger.Information("Fetching configurable user view columns for user view [{ViewUserId} ,{ViewId}]", view.UserId, view.Id);
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
                    Description = vc.ConfigurableEntityColumnDefinition.Description,
                    FieldFormatter = vc.ConfigurableEntityColumnDefinition.FieldFormatter,
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
                Description = vc.ConfigurableEntityColumnDefinition.Description,
                FieldFormatter = vc.ConfigurableEntityColumnDefinition.FieldFormatter,
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
            view = await GetViewModel(userId, viewClientId, permissions);
            columns = await GetConfigurableViewColumns(view, permissions);
        } else {
            var hasView = await HasViewExist(userId, viewClientId);
            if (!hasView) {
                Log.Logger.Error($"ConfigurableUserViewRepository: View not found");
                throw new EntityNotFoundException(nameof(ConfigurableUserView), $"[{userId}-{viewClientId}]");
            }
            view = await GetViewModel(userId, viewClientId, permissions);
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

    public async Task<View> GetView(string userId, Guid viewClientId, List<string>? permissions) {
        Log.Logger.Information($"Fetching configurable user view for user {userId} and view {viewClientId}");
        var view = await GetViewModel(userId, viewClientId, permissions);
        if (view == null) {
            Log.Logger.Error($"ConfigurableUserViewRepository: View not found");
            throw new EntityNotFoundException(nameof(ConfigurableUserView), $"[{userId}-{viewClientId}]");
        }
        var columns = await GetConfigurableViewColumns(view, permissions);
        return ToView(view, columns);
    }

    public async Task<View> CreateView(CreateViewRequest input, List<string>? permissions) {
        var view = await GetViewModel(input.UserId, input.ViewClientKey, permissions);
        if (view == null) {
            Log.Logger.Error($"ConfigurableUserViewRepository: View not found");
            throw new EntityNotFoundException(nameof(ConfigurableUserView), $"[{input.UserId}-{input.ViewClientKey}]");
        }
        var columns = await GetViewColumns(view);
        var hasAddedView = false;
        if (view.IsPredefined) {
            await addNewView(view, columns, input, permissions);
            hasAddedView = true;
        }
        if (view.IsShareAble && !view.IsPredefined) {
            await addNewView(view, columns, input, permissions);
            hasAddedView = true;
        }
        if (view.UserId == input.UserId && !view.IsShareAble) {
            await addNewView(view, columns, input, permissions);
            hasAddedView = true;
        }
        if (hasAddedView)
            return ToView(view, columns.OrderBy(vc => vc.Order).Select(vc => new ViewColumn() {
                DisplayName = vc.ConfigurableEntityColumnDefinition.DisplayName,
                EntityName = vc.ConfigurableEntityColumnDefinition.EntityName,
                FieldAlias = vc.ConfigurableEntityColumnDefinition.FieldAlias,
                FieldPath = vc.ConfigurableEntityColumnDefinition.FieldPath,
                Description = vc.ConfigurableEntityColumnDefinition.Description,
                FieldFormatter = vc.ConfigurableEntityColumnDefinition.FieldFormatter,
                ColumnName = vc.ConfigurableEntityColumnDefinition.ColumnName,
                IsFilterable = vc.ConfigurableEntityColumnDefinition.IsFilterable,
                IsSortable = vc.ConfigurableEntityColumnDefinition.IsSortable,
                DataType = vc.ConfigurableEntityColumnDefinition.DataType,
                FilterOperationType = vc.ConfigurableEntityColumnDefinition.FilterOperationType,
                IsHidden = vc.IsHidden,
                IsFixed = vc.IsFixed,
                Order = vc.Order
            }));
        Log.Logger.Error($"ConfigurableUserViewRepository: View not found");
        throw new EntityNotFoundException(nameof(ConfigurableUserView), $"[{input.UserId}-{input.ViewClientKey}]");
    }

    public async Task<View> UpdateView(UpdateViewRequest input) {
        var view = await GetViewModel(input.UserId, input.ViewClientKey, null);
        if (view == null) {
            Log.Logger.Error($"ConfigurableUserViewRepository: View not found");
            throw new EntityNotFoundException(nameof(ConfigurableUserView), $"[{input.UserId}-{input.ViewClientKey}]");
        }
        var columns = await GetViewColumns(view);
        view.Name = input.Name;
        view.Description = input.Description;
        view.IsShareAble = input.IsShareAble;
        view.Color = input.Color;
        Context.ConfigurableUserViews.Update(view);
        await Context.Instance.SaveChangesAsync();
        return ToView(view, columns.OrderBy(vc => vc.Order).Select(vc => new ViewColumn() {
            DisplayName = vc.ConfigurableEntityColumnDefinition.DisplayName,
            EntityName = vc.ConfigurableEntityColumnDefinition.EntityName,
            FieldAlias = vc.ConfigurableEntityColumnDefinition.FieldAlias,
            FieldPath = vc.ConfigurableEntityColumnDefinition.FieldPath,
            Description = vc.ConfigurableEntityColumnDefinition.Description,
            FieldFormatter = vc.ConfigurableEntityColumnDefinition.FieldFormatter,
            ColumnName = vc.ConfigurableEntityColumnDefinition.ColumnName,
            IsFilterable = vc.ConfigurableEntityColumnDefinition.IsFilterable,
            IsSortable = vc.ConfigurableEntityColumnDefinition.IsSortable,
            DataType = vc.ConfigurableEntityColumnDefinition.DataType,
            FilterOperationType = vc.ConfigurableEntityColumnDefinition.FilterOperationType,
            IsHidden = vc.IsHidden,
            IsFixed = vc.IsFixed,
            Order = vc.Order
        }));
    }

    public async Task<View> UpdateViewColumns(string userId, ViewUpdateColumnsInput input) {
        var view = await GetViewModel(userId, input.ViewClientKey, null);
        if (view == null) {
            Log.Logger.Error($"ConfigurableUserViewRepository: View not found");
            throw new EntityNotFoundException(nameof(ConfigurableUserView), $"[{userId}-{input.ViewClientKey}]");
        }
        view.Columns = new List<ConfigurableUserViewHasConfigurableEntityColumnDefinition>();
        foreach (var column in input.Columns) {
            var tColumn = await Context.ConfigurableUserViewHasConfigurableEntityColumnDefinitions.FirstOrDefaultAsync(w => w.ConfigurableEntityColumnDefinitionId == column && w.ConfigurableUserViewId == view.Id);
            if (tColumn == null) {
                Log.Logger.Error($"ConfigurableUserViewRepository: Column not found");
                throw new EntityNotFoundException(nameof(ConfigurableEntityColumnDefinition), $"[{column}]");
            }
            view.Columns.Append(new ConfigurableUserViewHasConfigurableEntityColumnDefinition() {
                ConfigurableEntityColumnDefinitionId = tColumn.ConfigurableEntityColumnDefinitionId,
                ConfigurableUserViewId = view.Id,
                IsHidden = tColumn.IsHidden,
                IsFixed = tColumn.IsFixed,
                Order = tColumn.Order
            });
        }
        if (!view.Columns.Any()) {
            Log.Logger.Error($"ConfigurableUserViewRepository: No columns to add to view");
            throw new EntityNotFoundException(nameof(ConfigurableEntityColumnDefinition), $"[{input.Columns}]");
        }
        Context.ConfigurableUserViews.Update(view);
        await Context.Instance.SaveChangesAsync();
        return ToView(view, view.Columns.OrderBy(vc => vc.Order).Select(vc => new ViewColumn() {
            DisplayName = vc.ConfigurableEntityColumnDefinition.DisplayName,
            EntityName = vc.ConfigurableEntityColumnDefinition.EntityName,
            FieldAlias = vc.ConfigurableEntityColumnDefinition.FieldAlias,
            FieldPath = vc.ConfigurableEntityColumnDefinition.FieldPath,
            Description = vc.ConfigurableEntityColumnDefinition.Description,
            FieldFormatter = vc.ConfigurableEntityColumnDefinition.FieldFormatter,
            ColumnName = vc.ConfigurableEntityColumnDefinition.ColumnName,
            IsFilterable = vc.ConfigurableEntityColumnDefinition.IsFilterable,
            IsSortable = vc.ConfigurableEntityColumnDefinition.IsSortable,
            DataType = vc.ConfigurableEntityColumnDefinition.DataType,
            FilterOperationType = vc.ConfigurableEntityColumnDefinition.FilterOperationType,
            IsHidden = vc.IsHidden,
            IsFixed = vc.IsFixed,
            Order = vc.Order
        }));
    }

    public async Task<bool> DeleteView(string userId, Guid viewClientId) {
        var view = await GetViewModel(userId, viewClientId, null);
        if (view == null) {
            Log.Logger.Error($"ConfigurableUserViewRepository: View not found");
            return false;
        }
        view.DeletedAt = DateTime.UtcNow;
        Context.ConfigurableUserViews.Update(view);
        await Context.Instance.SaveChangesAsync();
        return true;
    }

    private async Task addNewView(ConfigurableUserView view, IEnumerable<ConfigurableUserViewHasConfigurableEntityColumnDefinition> columns, CreateViewRequest input, List<string>? permissions) {
        var newView = new ConfigurableUserView() {
            ClientUniqueKey = Guid.NewGuid(),
            Name = input.Name,
            Description = input.Description,
            IsPredefined = false,
            IsShareAble = input.IsShareAble,
            Color = input.Color,
            UserId = input.UserId,
            Permissions = permissions ?? new List<string>()
        };
        newView.Columns = new List<ConfigurableUserViewHasConfigurableEntityColumnDefinition>();
        foreach (var column in columns) {
            if (permissions != null) {
                if (permissions.Any() && column.ConfigurableEntityColumnDefinition.Permissions.Any(p => permissions.Contains(p))) {
                    newView.Columns.Append(new ConfigurableUserViewHasConfigurableEntityColumnDefinition() {
                        ConfigurableEntityColumnDefinitionId = column.ConfigurableEntityColumnDefinitionId,
                        ConfigurableUserViewId = newView.Id,
                        IsHidden = column.IsHidden,
                        IsFixed = column.IsFixed,
                        Order = column.Order
                    });
                    continue;
                }
            }
            newView.Columns.Append(new ConfigurableUserViewHasConfigurableEntityColumnDefinition() {
                ConfigurableEntityColumnDefinitionId = column.ConfigurableEntityColumnDefinitionId,
                ConfigurableUserViewId = newView.Id,
                IsHidden = column.IsHidden,
                IsFixed = column.IsFixed,
                Order = column.Order
            });
        }
        await Context.ConfigurableUserViews.AddAsync(newView);
        await Context.Instance.SaveChangesAsync();
    }


    private async Task<ConfigurableUserView?> GetViewModel(string userId, Guid viewClientId, List<string>? permissions) {
        Log.Logger.Information("Fetching configurable user view for user {UserId} and view {ViewClientId}", userId, viewClientId);
        var fetchingView = (from view in Context.ConfigurableUserViews
            where view.ClientUniqueKey == viewClientId && view.DeletedAt == null
            select view);
        if (permissions != null && permissions.Any()) {
            fetchingView = fetchingView.Where(w => w.Permissions.Any(p => permissions.Contains(p)));
        }
        if (fetchingView == null) {
            Log.Logger.Error("View {ViewClientId} not found for user {UserId}", viewClientId, userId);
            throw new EntityNotFoundException(nameof(ConfigurableUserView), $"[{userId}]-[{viewClientId}]");
        }
        var tView = fetchingView.FirstOrDefault();
        return tView != null && tView.IsPredefined ? tView : fetchingView.FirstOrDefault(w => w.UserId == userId);
    }

    private async Task<IEnumerable<ConfigurableUserViewHasConfigurableEntityColumnDefinition>> GetViewColumns(ConfigurableUserView view) {
        Log.Logger.Information("Fetching configurable user view columns for user view [{ViewUserId} ,{ViewId}]", view.UserId, view.Id);
        var selectedColumns = await (from vc in Context.ConfigurableUserViewHasConfigurableEntityColumnDefinitions
            where vc.ConfigurableUserViewId == view.Id
            select vc).ToListAsync();
        return selectedColumns;
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