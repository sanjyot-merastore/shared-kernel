using Microsoft.AspNetCore.Mvc;

namespace MeraStore.Services.Sample.Api.Controllers
{
    /// <summary>
    /// Sample controller that demonstrates structured logging and mock weather data.
    /// </summary>
    [ApiController]
    [Route("api/v1.0/[controller]")]
    public class WeatherForecastController(ILogger<WeatherForecastController> logger) : ControllerBase
    {
        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild",
            "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        /// <summary>
        /// Retrieves a mock 5-day weather forecast.
        /// </summary>
        /// <returns>Returns an array of <see cref="WeatherForecast"/> records.</returns>
        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get()
        {
            var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            return Ok(result);
        }

        /// <summary>
        /// Accepts a student payload and returns a mock 5-day weather forecast.
        /// </summary>
        /// <param name="student">The student object sent in the request body.</param>
        /// <returns>Returns a mock weather forecast array.</returns>
        [HttpPost(Name = "Post")]
        public async Task<IActionResult> Post([FromBody] Student student)
        {
            var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            return Ok(result);
        }
    }
}
