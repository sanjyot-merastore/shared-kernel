using MeraStore.Services.Logging.Domain.LogSinks;
using MeraStore.Shared.Kernel.Logging.Sinks.ElasticSearch;
using Serilog;


namespace MeraStore.Services.Logging.Api.Extensions;

/// <summary>
/// Provides extension methods for configuring logging services in the application.
/// </summary>
public static class ServiceExtensions
{
  /// <summary>
  /// Configures Serilog as the logging provider with multiple sinks, including Elasticsearch.
  /// </summary>
  /// <param name="builder">The <see cref="WebApplicationBuilder"/> used to configure services.</param>
  /// <exception cref="InvalidOperationException">Thrown if the Elasticsearch URL is missing in configuration.</exception>
  public static void AddLoggingServices(this WebApplicationBuilder builder, string serviceName)
  {
    // Retrieve Elasticsearch URL from configuration
    var elasticsearchUrl = builder.Configuration.GetValue<string>("ElasticSearchUrl");

    if (string.IsNullOrWhiteSpace(elasticsearchUrl))
    {
      throw new InvalidOperationException("Elasticsearch URL is missing in configuration.");
    }

    // Configure Serilog
    var logger = new LoggerConfiguration()
      .Enrich.FromLogContext()
      .WriteTo.Console(formatProvider: System.Globalization.CultureInfo.InvariantCulture) // Structured logging
      .WriteTo.Sink(new ApplicationSink(serviceName, elasticsearchUrl))
      .WriteTo.Sink(new InfrastructureSink(serviceName, elasticsearchUrl))
      .WriteTo.Sink(new EntityFrameworkSink(serviceName, elasticsearchUrl))
      .CreateLogger();

    // Assign Serilog as the logging provider
    Log.Logger = logger;
    builder.Host.UseSerilog();
  }
}
