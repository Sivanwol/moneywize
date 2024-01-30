using Domain.Entities;
using Infrastructure.GQL;
using Infrastructure.GQL.Inputs.View;
namespace Domain.Persistence.Repositories.Interfaces;

public interface IConfigurableUserViewRepository {
    public Task<bool> HasViewExist(string userId, Guid viewClientId);
    public Task<bool> HasViewDefault(Guid viewClientId);
    public Task<ICollection<ViewColumn>> GetConfigurableViewColumns(ConfigurableUserView view, List<string>? permissions = null);
    public Task<View> GetViewDefinition(string userId, Guid viewClientId, List<string>? permissions = null);
    public Task<ICollection<View>> GetViews(string userId, List<string>? permissions = null);
    public Task<View> GetView(string userId, Guid viewClientId, List<string>? permissions = null);
    public Task<View> CreateView(string userId, ViewCreateInput input, List<string>? permissions = null);
    public Task<View> UpdateView(string userId, ViewUpdateInput input);
    public Task<View> UpdateViewColumns(string userId, ViewUpdateColumnsInput input);
    public Task<bool> DeleteView(string userId, Guid viewClientId);
}