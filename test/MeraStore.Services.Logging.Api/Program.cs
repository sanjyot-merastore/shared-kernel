using MeraStore.Services.Logging.Api.Extensions;
using MeraStore.Services.Logging.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();



builder.Services.AddProblemDetails();
builder.AddLoggingServices("sample-api");

var app = builder.Build();


app.UseMiddleware<TracingMiddleware>();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();

app.Run();

