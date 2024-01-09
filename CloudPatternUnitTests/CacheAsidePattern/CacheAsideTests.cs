using Microsoft.Extensions.Caching.Memory;

namespace CloudPatternUnitTests.CacheAsidePattern
{
    /// <summary>
    /// Load data on demand into a cache from a data store. This pattern can improve performance and also helps to maintain consistency between 
    /// data held in the cache and the data in the underlying data store.
    /// </summary>
    /// <remarks>
    /// If you use this patter in a ASP.net core app, remember to include this in your program main:
    ///      //  add memory service cache.
    ///      builder.Services.AddMemoryCache();
    ///      builder.Services.AddScoped<CacheAsideService>();
    /// </remarks>
    public class CacheAsideTests
    {
        [Fact]
        public async Task GetOrSetAsync_ReturnDataFromCache_IfAvailable()
        {
            // Arrange
            var cache = new MemoryCache(new MemoryCacheOptions());
            var cacheAsideService = new CacheAsideFakeService(cache);
            string cacheKey = "TestKey";
            string expectedData = "TestData";
            cache.Set(cacheKey, expectedData);

            // Act
            string actualData = await cacheAsideService.GetOrSetAsync(cacheKey, () => Task.FromResult("NewData"));

            // Assert
            Assert.Equal(expectedData, actualData);
        }

        [Fact]
        public async Task GetOrSetAsync_RetrievesDataAndStoresInCache_IfNotCache()
        {
            // Arrange
            var cache = new MemoryCache(new MemoryCacheOptions());
            var cacheAsideService = new CacheAsideFakeService(cache);
            string cacheKey = "TestKey";
            string expectedData = "TestData";

            // Act
            string actualData = await cacheAsideService.GetOrSetAsync(cacheKey, () => Task.FromResult(expectedData));

            // Assert
            Assert.Equal(expectedData, actualData);
            Assert.True(cache.TryGetValue(cacheKey, out string cachedData));
            Assert.Equal(expectedData, cachedData);
        }
    }
}
