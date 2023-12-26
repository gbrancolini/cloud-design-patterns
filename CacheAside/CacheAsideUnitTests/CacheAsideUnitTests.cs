using Xunit;
using Moq;
using Microsoft.Extensions.Caching.Memory;
using CacheAside.Services;


namespace CacheAsideUnitTests
{
    public class CacheAsideUnitTests
    {
        [Fact]
        public async Task GetOrSetAsync_ReturnDataFromCache_IfAvailable()
        {
            // Arrange
            var cache = new MemoryCache(new MemoryCacheOptions());
            var cacheAsideService = new CacheAsideService(cache);
            string cacheKey = "TestKey";
            string expectedData = "TestData";
            cache.Set(cacheKey, expectedData);

            // Act
            string actualData = await cacheAsideService.GetOrSetAsync<string>(cacheKey, () => Task.FromResult("NewData"));

            // Assert
            Assert.Equal(expectedData, actualData);
        }

        [Fact]
        public async Task GetOrSetAsync_RetrievesDataAndStoresInCache_IfNotCache()
        {
            var cache = new MemoryCache(new MemoryCacheOptions());
            var cacheAsideService = new CacheAsideService(cache);
            string cacheKey = "TestKey";
            string expectedData = "TestData";

            // Act
            string actualData = await cacheAsideService.GetOrSetAsync<string>(cacheKey, () => Task.FromResult(expectedData));
            Assert.Equal(expectedData, actualData);
            Assert.True(cache.TryGetValue<string>(cacheKey, out string cachedData));
            Assert.Equal<string>(expectedData, cachedData);
        }
    }
}