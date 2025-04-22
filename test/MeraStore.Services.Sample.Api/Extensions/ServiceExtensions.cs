using MeraStore.Shared.Kernel.Logging;
using MeraStore.Shared.Kernel.Logging.Filters;
using MeraStore.Shared.Kernel.Logging.Interfaces;
using Microsoft.Extensions.DependencyInjection.Extensions;
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
  /// <param name="serviceName">service name</param>
  /// <exception cref="InvalidOperationException">Thrown if the Elasticsearch URL is missing in configuration.</exception>
  public static void AddLoggingServices(this WebApplicationBuilder builder, string serviceName)
  {

    // Registering MaskingFilter
    builder.Services.AddSingleton<IMaskingFilter, MaskingFilter>();
    builder.Services.AddSingleton<IMaskingFilterBuilder, MaskingFilterBuilder>();

    // Registering Masking Filters
    builder.Services.AddSingleton<IMaskingFieldFilter, JsonPayloadRequestFilter>();
    builder.Services.AddSingleton<IMaskingFieldFilter, JsonPayloadResponseFilter>();


    var elasticsearchUrl = builder.Configuration.GetValue<string>(Shared.Kernel.Logging.Constants.Logging.Elasticsearch.Url);
    if (string.IsNullOrWhiteSpace(elasticsearchUrl))
    {
      throw new InvalidOperationException("Elasticsearch URL is missing in configuration.");
    }

    try
    {
      // Build and configure Serilog
      var logBuilder = new LoggerBuilder(builder.Services)
        .WithServiceName(serviceName)
        .AddConsoleSink()
        .AddElasticsearchSink(elasticsearchUrl)
        .AddInfrastructureSink(elasticsearchUrl)
        .AddEntityFrameworkSink(elasticsearchUrl);

      Log.Logger = logBuilder.Build();
      //builder.Host.UseSerilog(Log.Logger);
    }
    catch (Exception ex)
    {
      // Handle any errors during logger setup
      Console.WriteLine($"Logging configuration failed: {ex.Message}");
      throw;
    }
  }
}
