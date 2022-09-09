using Microsoft.AspNetCore.Mvc;

namespace ParkyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'WeatherForecastController'
    public class WeatherForecastController : ControllerBase
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'WeatherForecastController'
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

      public WeatherForecastController(ILogger<WeatherForecastController> logger)
       {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
      public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}