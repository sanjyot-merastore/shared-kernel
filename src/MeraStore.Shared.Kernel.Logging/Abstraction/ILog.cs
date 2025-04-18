using Serilog.Events;

namespace MeraStore.Shared.Kernel.Logging.Abstraction;

public interface ILog
{
 
  string CorrelationId { get; set; }
  string RequestId { get; set; }

  string TransactionId { get; set; }

  string TraceId { get; set; }

  string SpanId { get; set; }

  string ParentSpanId { get; set; }

  string ServiceName { get; set; }

  string Environment { get; set; }

  string HostName { get; set; }

  string ClientIp { get; set; }

  string ServerIp { get; set; }

  string PodName { get; set; }

  string ContainerId { get; set; }
  string TenantId { get; set; }

  string UserId { get; set; }

  string SessionId { get; set; }

  string DeviceId { get; set; }

  string UserAgent { get; set; }

  string HttpMethod { get; set; }

  string Message { get; set; }

  string RequestUrl { get; set; }

  DateTime? RequestTimestamp { get; set; }

  string FeatureFlag { get; set; }

  bool? IsDebugMode { get; set; }

  double? TimeTakenMs { get; set; }

  public LogEventLevel Level { get; }
}