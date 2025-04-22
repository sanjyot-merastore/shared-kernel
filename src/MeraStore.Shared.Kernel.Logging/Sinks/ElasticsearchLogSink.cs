using Elastic.Clients.Elasticsearch;
using MeraStore.Shared.Kernel.Logging.Interfaces;
using Serilog.Core;
using Serilog.Events;

using Constants = MeraStore.Shared.Kernel.Logging.Constants;

public class ElasticsearchLogSink : ILogSink, ILogEventSink
{
  private readonly ElasticsearchClient _client;
  private readonly string _indexFormat;

  public ElasticsearchLogSink(string elasticsearchUrl, string indexFormat = null)
  {
    var settings = new ElasticsearchClientSettings(new Uri(elasticsearchUrl));
    _client = new ElasticsearchClient(settings);
    _indexFormat = indexFormat ?? $"{Constants.Logging.Elasticsearch.DefaultIndexFormat}{DateTime.UtcNow:yyyy-MM}";
  }

  public async Task WriteAsync(ILog logEntry)
  {
    // Convert the log entry to a structure that Elasticsearch understands
    var logFields = new Dictionary<string, object>();

    foreach (var field in logEntry.LoggingFields)
    {
      logFields[field.Key] = field.Value;
    }

    // Add timestamp and other necessary fields
    logFields["timestamp"] = DateTime.UtcNow;
    logFields["level"] = logEntry.Level.ToString();
    logFields["message"] = logEntry.Message;

    // Index document into Elasticsearch
    var indexName = string.Format(_indexFormat, DateTime.UtcNow);
    await Task.Run(async () => await _client.IndexAsync(logFields, idx => idx.Index(_indexFormat)));


  }

  public void Emit(LogEvent logEvent)
  {
    try
    {
      var sourceContext = logEvent.Properties.TryGetValue("SourceContext", out var contextValue)
        ? contextValue.ToString()?.Trim('"')
        : string.Empty;

      if (string.IsNullOrEmpty(sourceContext) || !sourceContext.Contains("MeraStore"))
        return;

      // Prepare log fields
      var logFields = new Dictionary<string, object>
      {
        ["@timestamp"] = logEvent.Timestamp.UtcDateTime,
        ["level"] = logEvent.Level.ToString(),
        ["message"] = logEvent.RenderMessage()
      };

      foreach (var property in logEvent.Properties)
      {
        // Optionally strip quotes or keep raw string
        logFields[property.Key] = property.Value.ToString();
      }

      // Format index name
      var indexName = _indexFormat.Contains("{0}")
        ? string.Format(_indexFormat, DateTime.UtcNow)
        : _indexFormat;

      var response = _client.IndexAsync(logFields, idx => idx.Index(indexName))
        .GetAwaiter()
        .GetResult();

      if (!response.IsValidResponse)
      {
        Console.WriteLine($"[ElasticsearchLogSink] Failed to index log. Error: {response.ElasticsearchServerError?.Error?.Reason}");
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine($"[ElasticsearchLogSink] Emit failed: {ex.Message}");
    }
  }

}