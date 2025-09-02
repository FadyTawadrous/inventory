using Microsoft.Extensions.Caching.Memory;

public class CachingService
{
    private readonly IMemoryCache _memoryCache;

    public CachingService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }
    public T? GetOrCreate<T>(string key, Func<ICacheEntry, T> createItem)
    {
        return _memoryCache.GetOrCreate(key, createItem);
    }
    public T? Get<T>(string key)
    {
        _memoryCache.TryGetValue(key, out T? value);
        return value;
    }

    public void Set<T>(string key, T value, TimeSpan? absoluteExpirationRelativeToNow = null)
    {
        var options = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow ?? TimeSpan.FromMinutes(5)
        };
        _memoryCache.Set(key, value, options);
    }

    public void Remove(string key)
    {
        _memoryCache.Remove(key);
    }
}