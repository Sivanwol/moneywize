namespace Infrastructure.Services.Cache;

public interface ICacheService {
    Task<T?> GetAsync<T>(string key);
    Task RegisterAsync<T>(string key, T value, int? minutes = null);
    Task RemoveAsync(string key);
    Task<bool> IsExistAsync(string key);
}