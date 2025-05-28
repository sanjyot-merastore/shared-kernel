using MeraStore.Shared.Kernel.Logging.Filters;
using MeraStore.Shared.Kernel.Logging.Interfaces;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MeraStore.Shared.Kernel.Logging;

public static class ServiceRegistrations
{
  /// <summary>
  /// Registers and configures Serilog as the logging provider with optional sinks such as Console, File, Elasticsearch,
  /// Infrastructure Sink, and Entity Framework Sink. Also registers masking services for sensitive data in request/response payloads.
  /// </summary>
  /// <param name="builder">The <see cref="WebApplicationBuilder"/> used to configure the application and services.</param>
  /// <param name="serviceName">A unique identifier for the service or application using the logger.</param>
  /// <param name="configureOptions">An action used to configure logging options such as sink selections and Elasticsearch settings.</param>
  /// <exception cref="InvalidOperationException">
  /// Thrown if <c>UseElasticsearch</c>, <c>UseInfrastructureSink</c>, or <c>UseEntityFrameworkSink</c> is enabled but the <c>ElasticsearchUrl</c> is not provided.
  /// </exception>

  public static void AddLoggingServices(this WebApplicationBuilder builder, string serviceName, Action<LoggingOptions> configureOptions)
  {
    var options = new LoggingOptions();
    configureOptions.Invoke(options);

    // Register Masking Services
    builder.Services.AddSingleton<IMaskingFilter, MaskingFilter>();
    builder.Services.AddSingleton<IMaskingFilterBuilder, MaskingFilterBuilder>();
    builder.Services.AddSingleton<IMaskingFieldFilter, JsonPayloadRequestFilter>();
    builder.Services.AddSingleton<IMaskingFieldFilter, JsonPayloadResponseFilter>();
    if (options.UseInfrastructureSink || options.UseEntityFrameworkSink)
      builder.Services.AddSerilog();

    if (options.UseElasticsearch && string.IsNullOrWhiteSpace(options.ElasticsearchUrl))
    {
      throw new InvalidOperationException("Elasticsearch URL is required when UseElasticsearch is true.");
    }

    try
    {
      var logBuilder = new LoggerBuilder(builder.Services)
        .WithServiceName(serviceName);

      if (options.UseConsole)
        logBuilder.AddConsoleSink();

      if (options.UseFile)
        logBuilder.AddFileSink();

      if (options.UseElasticsearch)
        logBuilder.AddElasticsearchSink(options.ElasticsearchUrl);

      if (options.UseInfrastructureSink)
        logBuilder.AddInfrastructureSink(options.ElasticsearchUrl);

      if (options.UseEntityFrameworkSink)
        logBuilder.AddEntityFrameworkSink(options.ElasticsearchUrl);

      Log.Logger = logBuilder.Build();
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Logging configuration failed: {ex.Message}");
      throw;
    }
  }
}