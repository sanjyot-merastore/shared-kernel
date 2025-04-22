using MeraStore.Services.Logging.Api.Extensions;
using MeraStore.Services.Logging.Api.Middlewares;
using MeraStore.Services.Sample.Api;
using MeraStore.Services.Sample.Api.Controllers;
using MeraStore.Shared.Kernel.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHealthChecks()
  .AddCheck<ServiceHealthCheck>("sample_service_health_check");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddProblemDetails();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<WeatherForecastController>();
builder.AddLoggingServices(Constants.ServiceName);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseMiddleware<AppContextTracingMiddleware>();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<ApiLoggingMiddleware>();
app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();

