using MeraStore.Shared.Kernel.Logging.Interfaces;

namespace MeraStore.Shared.Kernel.Logging.Sinks.ElasticSearch;

public class EntityFrameworkSink(string serviceName, string elasticsearchUrl)
  : BaseElasticsearchSink(serviceName, elasticsearchUrl, Constants.Logging.Elasticsearch.EFCoreIndexFormat)
{
  public override void Emit(LogEvent logEvent)
  {
    var logEntry = GetCommonLogFields(logEvent);
    var sourceContext = logEntry[Constants.Logging.LogFields.SourceContext]?.ToString();

    if (sourceContext == null || !sourceContext.Contains("Microsoft.EntityFrameworkCore"))
      return; // Ignore logs that don't belong here

    // Index the log entry asynchronously
    Task.Run(async () =>
    {
      try
      {
        await Client.IndexAsync(logEntry, idx => idx.Index($"{Constants.Logging.Elasticsearch.EFCoreIndexFormat}{DateTime.UtcNow:yyyy-MM}"));
      }
      catch (Exception ex)
      {
        // Log the exception if indexing fails
        Log.Error(ex, "Failed to index log entry to Elasticsearch.");
      }
    });
  }

  public override async Task WriteAsync(ILog logEntry)
  {
    throw new NotImplementedException();
  }
}