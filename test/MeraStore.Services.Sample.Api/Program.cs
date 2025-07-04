using MeraStore.Services.Sample.Api;
using MeraStore.Services.Sample.Api.Middlewares.Extensions;
using MeraStore.Shared.Kernel.Http;
using MeraStore.Shared.Kernel.Logging;
using MeraStore.Shared.Kernel.WebApi;
using MeraStore.Shared.Kernel.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHealthChecks()
  .AddCheck<ServiceHealthCheck>("sample_service_health_check");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();
//builder.Services.AddCustomSwagger(serviceName: Constants.ServiceName, "MeraStore.Services.Sample.Api.xml");

builder.AddApiServices(Constants.ServiceName, defaultLogging: true);

builder.Services.AddHttpServices(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseMeraStoreTracing();
app.UseMeraStoreErrorHandling();

app.UseCustomSwagger(Constants.ServiceName);

app.UseMeraStoreLogging();
app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();

