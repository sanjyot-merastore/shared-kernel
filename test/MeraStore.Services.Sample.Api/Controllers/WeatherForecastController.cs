using Microsoft.AspNetCore.Mvc;

namespace MeraStore.Services.Sample.Api.Controllers
{
  [ApiController]
  [Route("api/v1.0/[controller]")]
  public class WeatherForecastController(ILogger<WeatherForecastController> logger) : ControllerBase
  {

    private static readonly string[] Summaries =
    [
      "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];


    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
      logger.LogInformation($"Getting weather forecast - {AppContext.Current.SampleId}");
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
