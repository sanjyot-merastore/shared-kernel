using MeraStore.Shared.Kernel.Logging.Interfaces;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeraStore.Shared.Kernel.Logging;

public static class ServiceRegistrations
{
  public static IServiceCollection AddLoggingServices1(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddSingleton<ILogWriter, LogWriter>();
    var loggerBuilder = new LoggerBuilder(services);

    // Configure your sinks here
    loggerBuilder
      .AddConsoleSink()
      .AddFileSink(@"D:\logs.txt")
      .AddElasticsearchSink("http://localhost:9200", $"{Constants.Logging.Elasticsearch.DefaultIndexFormat}{DateTime.UtcNow:yyyy-MM}"); // Example Elasticsearch URL
      
    loggerBuilder.Build();
    return services;
  }

}