using MeraStore.Shared.Kernel.Logging;
using MeraStore.Shared.Kernel.Logging.Sinks.ElasticSearch;

namespace MeraStore.Services.Logging.Domain.LogSinks
{
  public class ApplicationSink(string serviceName, string elasticsearchUrl)
    : BaseElasticsearchSink(serviceName, elasticsearchUrl, Constants.Logging.Elasticsearch.DefaultIndexFormat)
  {
    

    public override void Emit(LogEvent logEvent)
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

      // Ensure logs belong to the service name
      var sourceContext = logEntry[Constants.Logging.LogFields.SourceContext]?.ToString();
      if (sourceContext == null || !sourceContext.StartsWith("MeraStore"))
        return; // Ignore logs that don't belong here


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
          Log.Error(ex, "Failed to index log entry to Elasticsearch.");
        }
      });
    }
  }
}
