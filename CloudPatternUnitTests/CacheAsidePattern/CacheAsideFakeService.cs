using Microsoft.Extensions.Caching.Memory;

namespace CloudPatternUnitTests.CacheAsidePattern
{
    internal class CacheAsideFakeService
    {
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _defaultCacheDuration = TimeSpan.FromMinutes(30);

        public CacheAsideFakeService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetOrSetAsync<T>(string cacheKey, Func<Task<T>> retrieveData)
        {
            if (!_cache.TryGetValue(cacheKey, out T value))
            {
                value = await retrieveData();

                var cacheEntryOptions = new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = _defaultCacheDuration };

                _cache.Set(cacheKey, value, cacheEntryOptions);
            }

            return value;
        }
    }
}
