using Application.Configs;
using EasyCaching.Core;
using Newtonsoft.Json;
using Serilog;
namespace Infrastructure.Services.Cache;

public class CacheService : ICacheService {
    private readonly IRedisCachingProvider _cache;
    private readonly BackendApplicationConfig _config;
    public CacheService(IEasyCachingProviderFactory cache, BackendApplicationConfig config) {
        _cache = cache.GetRedisProvider(Application.Constraints.Cache.ProviderName);
        _config = config;
    }
    public async Task<T?> GetAsync<T>(string cacheKey) {
        if (!_config.EnableCache)
            return default(T);
        Log.Logger.Information($"FetchCache: {cacheKey}");

        if (await _cache.KeyExistsAsync(cacheKey) == false) {
            Log.Logger.Information($"FetchCache: {cacheKey} - Not Found");
            return default(T);
        }
        Log.Logger.Information($"FetchCache: {cacheKey} - Found");
        var cacheValue = await _cache.StringGetAsync(cacheKey);
        return !string.IsNullOrEmpty(cacheValue) ? JsonConvert.DeserializeObject<T>(cacheValue!)! : default(T);
    }

    public async Task RegisterAsync<T>(string cacheKey, T records, int? minutes = null) {
        if (!_config.EnableCache) {
            return;
        }
        Log.Logger.Information($"RegisterCache: {cacheKey}");
        if (await _cache.KeyExistsAsync(cacheKey)) {
            await RemoveAsync(cacheKey);
        }

        await _cache.StringSetAsync(cacheKey, JsonConvert.SerializeObject(records), TimeSpan.FromMinutes(minutes ?? Application.Constraints.Cache.EFCachingTimeInMinutes));
    }

    public async Task RemoveAsync(string key) {
        if (!_config.EnableCache) {
            return;
        }
        var cacheKey = Application.Constraints.Cache.GetKey(key);
        Log.Logger.Information($"RemoveCache: {cacheKey}");
        await _cache.KeyDelAsync(cacheKey);
    }

    public async Task<bool> IsExistAsync(string cacheKey) {
        if (!_config.EnableCache) {
            return false;
        }
        Log.Logger.Information($"IsExistAsync: {cacheKey}");
        var isExist = await _cache.KeyExistsAsync(cacheKey);
        return isExist;
    }
}