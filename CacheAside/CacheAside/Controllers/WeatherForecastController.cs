using CacheAside.Services;
using Microsoft.AspNetCore.Mvc;

namespace CacheAside.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly CacheAsideService _cacheService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, CacheAsideService cacheAsideService)
        {
            _logger = logger;
            _cacheService = cacheAsideService;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> GetAsync()
        {
            var weatherForecast =  Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            return await _cacheService.GetOrSetAsync("Get_WeatherForecastController", () => Task.FromResult(weatherForecast));
        }
    }
}
