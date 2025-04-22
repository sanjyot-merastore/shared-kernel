using MeraStore.Services.Logging.Api.Middlewares;
using MeraStore.Services.Logging.Domain.LogSinks;
using MeraStore.Shared.Kernel.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var elasticsearchUrl = builder.Configuration.GetValue<string>(Constants.Logging.Elasticsearch.Url);
var logger = new LoggerConfiguration()
  .Enrich.FromLogContext()
  .WriteTo.Console(formatProvider: System.Globalization.CultureInfo.InvariantCulture) // Structured logging
  //.WriteTo.Sink(new AppLogsElasticsearchSink(elasticsearchUrl))
  .WriteTo.Sink(new InfraLogsElasticsearchSink(elasticsearchUrl))
  //.WriteTo.Sink(new EfLogsElasticsearchSink(elasticsearchUrl))
  .CreateLogger();

// Assign Serilog as the logging provider
Log.Logger = logger;
builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<TracingMiddleware>();
app.UseMiddleware<ErrorHandlingMiddleware>();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
  var forecast = Enumerable.Range(1, 5).Select(index =>
      new WeatherForecast
      (
          DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
          Random.Shared.Next(-20, 55),
          summaries[Random.Shared.Next(summaries.Length)]
      ))
      .ToArray();
  return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
  public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
