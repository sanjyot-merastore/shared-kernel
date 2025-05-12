using Elastic.Clients.Elasticsearch;
using MeraStore.Shared.Kernel.Logging.Interfaces;

namespace MeraStore.Shared.Kernel.Logging.Sinks.ElasticSearch;

public class ApplicationSink : BaseElasticsearchSink
{
  private ElasticsearchClient _client;

  public ApplicationSink(string serviceName, string elasticsearchUrl, string indexFormat = null)
    : this(serviceName, elasticsearchUrl)
  {
    ServiceName = serviceName;
    var settings = new ElasticsearchClientSettings(new Uri(elasticsearchUrl));
    _client = new ElasticsearchClient(settings);
    IndexFormat = indexFormat ?? $"{Constants.Logging.Elasticsearch.DefaultIndexFormat}{DateTime.UtcNow:yyyy-MM}";
  }

  public ApplicationSink(string serviceName, string elasticsearchUrl) : base(serviceName, elasticsearchUrl, Constants.Logging.Elasticsearch.DefaultIndexFormat)
  {
    var settings = new ElasticsearchClientSettings(new Uri(elasticsearchUrl));
    _client = new ElasticsearchClient(settings);
    if(string.IsNullOrEmpty(IndexFormat))
      IndexFormat = $"{Constants.Logging.Elasticsearch.DefaultIndexFormat}{DateTime.UtcNow:yyyy-MM}";
    ServiceName = serviceName;
  }

  public override void Emit(LogEvent logEvent)
  {
    Log(logEvent);
  }

  public override async Task WriteAsync(ILog logEntry)
  {
    var logFields = new Dictionary<string, object>();

    foreach (var field in logEntry.LoggingFields)
    {
      logFields[field.Key] = field.Value;
    }

    // Add timestamp and other necessary fields
    logFields["timestamp"] = DateTime.UtcNow;
    logFields["level"] = logEntry.Level.ToString();
    logFields["message"] = logEntry.Message;
    logFields[Constants.Logging.LogFields.ServiceName] = ServiceName;

    await Client.IndexAsync(logFields, idx => idx.Index(IndexFormat));
  }

  private void Log(LogEvent logEvent)
  {
    var logEntry = GetCommonLogFields(logEvent);

    logEntry[Constants.Logging.LogFields.RequestUrl] = GetFormattedValue(logEvent.Properties, Constants.Logging.LogFields.RequestUrl);
    logEntry[Constants.Logging.LogFields.RequestBaseUrl] = GetFormattedValue(logEvent.Properties, Constants.Logging.LogFields.RequestBaseUrl);
    logEntry[Constants.Logging.LogFields.QueryString] = GetFormattedValue(logEvent.Properties, Constants.Logging.LogFields.QueryString);
    logEntry[Constants.Logging.LogFields.HttpMethod] = GetFormattedValue(logEvent.Properties, Constants.Logging.LogFields.HttpMethod);
    logEntry[Constants.Logging.LogFields.HttpVersion] = GetFormattedValue(logEvent.Properties, Constants.Logging.LogFields.HttpVersion);
    logEntry[Constants.Logging.LogFields.StatusCode] = GetFormattedValue(logEvent.Properties, Constants.Logging.LogFields.StatusCode);
    logEntry[Constants.Logging.LogFields.ClientIp] = GetFormattedValue(logEvent.Properties, Constants.Logging.LogFields.ClientIp);
    logEntry[Constants.Logging.LogFields.UserAgent] = GetFormattedValue(logEvent.Properties, Constants.Logging.LogFields.UserAgent);
    logEntry[Constants.Logging.LogFields.RequestPath] = GetFormattedValue(logEvent.Properties, Constants.Logging.LogFields.RequestPath);
    logEntry[Constants.Logging.LogFields.ServiceName] = ServiceName;

    // Ensure logs belong to the service name
    var sourceContext = logEntry[Constants.Logging.LogFields.SourceContext]?.ToString();
    if (sourceContext == null || !sourceContext.StartsWith("MeraStore"))
      return;


    // Index the log entry asynchronously
    Task.Run(async () =>
    {
      try
      {
        await Client.IndexAsync(logEntry, idx => idx.Index($"{Constants.Logging.Elasticsearch.DefaultIndexFormat}{DateTime.UtcNow:yyyy-MM}"));
      }
      catch (Exception ex)
      {
        // Log the exception if indexing fails
        Serilog.Log.Error(ex, "Failed to index log entry to Elasticsearch.");
      }
    });
  }
}