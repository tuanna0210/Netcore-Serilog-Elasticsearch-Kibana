using Microsoft.AspNetCore.Mvc;
using Serilog.Context;

namespace WebAPI.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            using (LogContext.PushProperty("Let's see", 12345))
            {
                _logger.LogInformation("Hello {@ABC}", new
                {
                    Name = "Tuan",
                    Age = 31
                });
                _logger.LogCritical("Fatal");
            }

            try
            {
                throw new Exception("Test exception");
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Something went wrong");
            }
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
