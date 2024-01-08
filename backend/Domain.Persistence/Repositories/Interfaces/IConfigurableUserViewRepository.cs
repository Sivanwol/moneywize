using Domain.Entities;
using Infrastructure.GQL;
namespace Domain.Persistence.Repositories.Interfaces;

public interface IConfigurableUserViewRepository {
    public Task<bool> HasViewExist(string userId, Guid viewClientId);
    public Task<bool> HasViewDefault(Guid viewClientId);
    public Task<ICollection<ViewColumn>> GetConfigurableViewColumns(ConfigurableUserView view, List<string>? permissions = null);
    public Task<View> GetViewDefinition(string userId, Guid viewClientId, List<string>? permissions = null);
    public Task<ICollection<View>> GetViews(string userId, List<string>? permissions = null);
}