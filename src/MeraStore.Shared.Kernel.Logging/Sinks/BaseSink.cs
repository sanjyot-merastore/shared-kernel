using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

using Elastic.Clients.Elasticsearch;

using Serilog.Core;
using Serilog.Events;

namespace MeraStore.Services.Logging.Domain.LogSinks;

[SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer")]
public abstract class BaseElasticsearchSink : ILogEventSink
{
  protected readonly ElasticsearchClient Client;
  protected readonly string HostName;
  protected readonly string OsPlatform;
  protected readonly string OsVersion;
  protected readonly int ProcessId;

  protected BaseElasticsearchSink(string elasticsearchUrl, string indexFormat)
  {
    var settings = new ElasticsearchClientSettings(new Uri(elasticsearchUrl))
      .DefaultIndex($"{indexFormat}{DateTime.UtcNow:yyyy-MM-dd}");
    Client = new ElasticsearchClient(settings);
    HostName = Environment.MachineName;
    OsPlatform = RuntimeInformation.OSDescription;
    OsVersion = Environment.OSVersion.ToString();
    ProcessId = Environment.ProcessId;
  }

  public abstract void Emit(LogEvent logEvent);

  protected static string GetFormattedValue(IReadOnlyDictionary<string, LogEventPropertyValue> properties, string key,
    string defaultValue = "")
  {
    if (properties.TryGetValue(key, out var value))
    {
      return value is ScalarValue { Value: string strValue } ? strValue : value.ToString()?.Trim('"');
    }

    return defaultValue;
  }

  protected Dictionary<string, object> GetCommonLogFields(LogEvent logEvent)
  {
    var sourceContext = logEvent.Properties.TryGetValue("SourceContext", out var contextValue)
      ? contextValue.ToString()?.Trim('"')
      : string.Empty;
    var fields = new Dictionary<string, object>();
    fields[Shared.Kernel.Logging.Constants.Logging.LogFields.Timestamp] = logEvent.Timestamp.UtcDateTime;
    fields[Shared.Kernel.Logging.Constants.Logging.LogFields.Level] = logEvent.Level.ToString();
    fields["service-name"] = "sample-app";
    fields[Shared.Kernel.Logging.Constants.Logging.LogFields.SourceContext] = sourceContext;
    fields[Shared.Kernel.Logging.Constants.Logging.SystemMetadata.MachineName] = HostName;
    fields[Shared.Kernel.Logging.Constants.Logging.SystemMetadata.OsPlatform] = OsPlatform;
    fields[Shared.Kernel.Logging.Constants.Logging.SystemMetadata.OsVersion] = OsVersion;
    fields[Shared.Kernel.Logging.Constants.Logging.SystemMetadata.ProcessId] = ProcessId;
    fields[Shared.Kernel.Logging.Constants.Logging.SystemMetadata.ThreadId] = Environment.CurrentManagedThreadId;
    fields[Shared.Kernel.Logging.Constants.Logging.SystemMetadata.Environment] =
      Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
    fields[Shared.Kernel.Logging.Constants.Logging.LogFields.Message] = logEvent.RenderMessage();

    fields[Shared.Kernel.Logging.Constants.Logging.LogFields.CorrelationId] = GetFormattedValue(logEvent.Properties,
      Shared.Kernel.Logging.Constants.Logging.LogFields.CorrelationId);
    fields[Shared.Kernel.Logging.Constants.Logging.LogFields.TransactionId] = GetFormattedValue(logEvent.Properties,
      Shared.Kernel.Logging.Constants.Logging.LogFields.TransactionId);
    fields[Shared.Kernel.Logging.Constants.Logging.LogFields.RequestId] = GetFormattedValue(logEvent.Properties,
      Shared.Kernel.Logging.Constants.Logging.LogFields.RequestId);

    if (logEvent.Exception == null)
    {
      return fields;
    }

    fields[Shared.Kernel.Logging.Constants.Logging.ExceptionFields.Exception] = logEvent.Exception.ToString();
    fields[Shared.Kernel.Logging.Constants.Logging.ExceptionFields.InnerException] =
      logEvent.Exception.InnerException?.ToString();
    fields[Shared.Kernel.Logging.Constants.Logging.ExceptionFields.ExceptionMessage] = logEvent.Exception.Message;
    fields[Shared.Kernel.Logging.Constants.Logging.ExceptionFields.StackTrace] = logEvent.Exception.StackTrace;
    fields[Shared.Kernel.Logging.Constants.Logging.ExceptionFields.ExceptionType] =
      logEvent.Exception.GetType().FullName;
    fields[Shared.Kernel.Logging.Constants.Logging.ExceptionFields.ExceptionDetails] = logEvent.Exception.ToString();

    return fields;
  }
}