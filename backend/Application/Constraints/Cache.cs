namespace Application.Constraints;

public static class Cache {
    public static string ProviderName = "Redis";
    public static string CachePrefix = "APP_CACHE_EF_";
    public static string SerializerName = "APP_CACHE_EF_PACK";

    public static string GetKey(string key) {
        return $"{CachePrefix}{key}";
    }
}