using Microsoft.Extensions.Caching.Memory;

namespace CacheAside.Services
{
    public class CacheAsideService
    {
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _defaultCacheDuration = TimeSpan.FromMinutes(30);

        public CacheAsideService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetOrSetAsync<T>(string cacheKey, Func<Task<T>> retrieveData)
        {
            // tries to get the value from cache
            if (!_cache.TryGetValue<T>(cacheKey, out T data))
            {
                // if there is no cache, returns data from origina source
                data = await retrieveData();

                // sets data to cache
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = _defaultCacheDuration
                };

                _cache.Set(cacheKey, data, cacheEntryOptions);
            }

            return data;
        }
    }
}
