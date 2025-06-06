using System.Reflection;
using Elastic.Clients.Elasticsearch;
using MeraStore.Shared.Kernel.Logging.Filters;
using MeraStore.Shared.Kernel.Logging.Interfaces;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using MeraStore.Shared.Kernel.Exceptions;

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

        // Elasticsearch Setup
        if (options.UseElasticsearch || options.UseInfrastructureSink || options.UseEntityFrameworkSink)
        {
            if (string.IsNullOrWhiteSpace(options.ElasticsearchUrl))
                throw LoggingServiceException.LogConfigurationMissing("Elasticsearch URL is required when Elasticsearch sink is enabled.");

            try
            {
                // Step 1: Create an Elastic client
                var elasticClient = new ElasticsearchClient(new ElasticsearchClientSettings(new Uri(options.ElasticsearchUrl)));

                // Step 2: Read field mappings from embedded resource
                var assembly = Assembly.GetExecutingAssembly();
                const string resourceName = "MeraStore.Shared.Kernel.Logging.elastic-field-mappings.json";

                using var stream = assembly.GetManifestResourceStream(resourceName)
                                   ?? throw new FileNotFoundException($"Embedded resource '{resourceName}' not found.");

                using var reader = new StreamReader(stream);
                var mappingJson = reader.ReadToEnd();
                using var doc = JsonDocument.Parse(mappingJson);

                var properties = doc.RootElement
                    .GetProperty("mappings")
                    .GetProperty("properties");

                var fieldMap = new Dictionary<string, string>();
                foreach (var prop in properties.EnumerateObject())
                {
                    if (prop.Value.TryGetProperty("type", out var typeProp))
                        fieldMap[prop.Name] = typeProp.GetString()!;
                }


                // Step 3: Push index template ONCE
                var client = new ElasticsearchClient(new ElasticsearchClientSettings(new Uri(options.ElasticsearchUrl)));

                var indexTemplates = new[]
                {
                    "log-service-template", "ef-logs-template", "infra-logs-template", "app-logs-template"
                };

                var indexPatterns = new[]
                {
                    "log-service-*", "ef-logs-*", "infra-logs-*", "app-logs-*"
                };

                for (var i = 0; i < indexTemplates.Length; i++)
                {
                    var pusher = new ElasticsearchTemplatePusher(client, fieldMap, indexTemplates[i], indexPatterns[i]);
                    pusher.PushAsync().GetAwaiter().GetResult();
                }
            }
            catch (Exception ex)
            {
                throw LoggingServiceException.LogInternalServerError($"Elasticsearch template setup failed: {ex.Message}", ex);
            }
        }

        // Configure Serilog Builder
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
            throw LoggingServiceException.LogInternalServerError($"Elasticsearch template setup failed: {ex.Message}", ex);
        }
    }
}
