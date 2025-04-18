using MeraStore.Shared.Kernel.Logging.Attributes;
using Serilog.Events;

namespace MeraStore.Shared.Kernel.Logging.Abstraction;

public abstract class BaseLog : ILog
{
  // Dynamic collection to store LoggingFields (key-value pairs) and update them
  [LogField("correlation-id", isHeader: true)]
  public string CorrelationId { get; set; }

  [LogField("request-id", isHeader: true)]
  public string RequestId { get; set; }

  [LogField("txn-id", isHeader: true)] public string TransactionId { get; set; }

  [LogField("trace-id", isHeader: true)] public string TraceId { get; set; }

  [LogField("span-id")] public string SpanId { get; set; }

  [LogField("parent-span-id")]
  public string ParentSpanId { get; set; }

  [LogField("service-name")]
  public string ServiceName { get; set; }

  [LogField("environment")]
  public string Environment { get; set; }

  [LogField("host-name")]
  public string HostName { get; set; }

  [LogField("client-ip")]
  public string ClientIp { get; set; }

  [LogField("server-ip")]
  public string ServerIp { get; set; }

  [LogField("pod-name")]
  public string PodName { get; set; }

  [LogField("container-id")]
  public string ContainerId { get; set; }

  [LogField("tenant-id", isHeader: true)]
  public string TenantId { get; set; }

  [LogField("user-id", isHeader: true)]
  public string UserId { get; set; }

  [LogField("session-id", isHeader: true)]
  public string SessionId { get; set; }

  [LogField("device-id", isHeader: true)]
  public string DeviceId { get; set; }

  [LogField("user-agent")]
  public string UserAgent { get; set; }

  [LogField("http-method")]
  public string HttpMethod { get; set; }

  [LogField("message")]
  public string Message { get; set; }

  [LogField("request-url")]
  public string RequestUrl { get; set; }

  [LogField("request-timestamp")]
  public DateTime? RequestTimestamp { get; set; }

  [LogField("feature-flag", isHeader: true)]
  public string FeatureFlag { get; set; }

  [LogField("is-debug-mode", isHeader: true)]
  public bool? IsDebugMode { get; set; }

  [LogField("time-taken-ms")]
  public double? TimeTakenMs { get; set; }

  [LogField("level")]
  public LogEventLevel Level { get; }
  public Dictionary<string, string> LoggingFields { get; private set; } = new();

  // Method to add or update a log field
  public void TrySetLogField(string key, string value)
  {
    if (LoggingFields.ContainsKey(key))
    {
      // Update the existing field value
      LoggingFields[key] = value;
    }
    else
    {
      // Add new field
      LoggingFields.TryAdd(key, value);
    }
  }
}