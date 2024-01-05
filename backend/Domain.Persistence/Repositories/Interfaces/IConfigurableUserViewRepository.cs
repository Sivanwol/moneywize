using Domain.DTO.ConfigurableEntities;
using Domain.Entities;
namespace Domain.Persistence.Repositories.Interfaces;

public interface IConfigurableUserViewRepository {
    public Task<ConfigurableUserView?> GetConfigurableUserView(string userId, int viewId);
    public Task<ConfigurableUserView?> GetConfigurableUserView(int viewId);
    public Task<bool> HasViewExist(string userId, int viewId);
    public Task<bool> HasViewDefault(int viewId);
    public Task<IEnumerable<ConfigurableUserViewColumn>> GetConfigurableViewColumns(string userId, int viewId);
    public Task<IEnumerable<ConfigurableUserViewColumn>> GetConfigurableViewColumns(int viewId);
    public Task<IEnumerable<ConfigurableUserViewColumn>> GetViewColumns(string userId, int viewId);
}