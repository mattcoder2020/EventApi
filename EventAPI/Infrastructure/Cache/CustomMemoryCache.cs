using Microsoft.Extensions.Caching.Memory;

namespace EventAPI.Infrastructure.Cache
{
    //This is a simplified custom memory cache implementation that is used in the EventController
    //
    public class CustomMemoryCache : IMemoryCache
    {
        private readonly MemoryCache _memoryCache;

        public CustomMemoryCache()
        {
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
        }

        public ICacheEntry CreateEntry(object key)
        {
            return _memoryCache.CreateEntry(key);
        }

        public void Dispose()
        {
            _memoryCache.Dispose();
        }

        public void Remove(object key)
        {
            _memoryCache.Remove(key);
        }

        public bool TryGetValue(object key, out object value)
        {
            return _memoryCache.TryGetValue(key, out value);
        }

        public void Set(object key, object value, TimeSpan absoluteExpirationRelativeToNow)
        {
            var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(absoluteExpirationRelativeToNow);
            _memoryCache.Set(key, value, cacheOptions);
        }
    }
}
