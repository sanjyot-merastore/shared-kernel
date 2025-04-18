﻿using MeraStore.Shared.Kernel.Logging.Attributes;
using Serilog.Events;

namespace MeraStore.Shared.Kernel.Logging.Abstraction;

public interface ILogFields
{
  [LogField("correlation-id", isHeader: true)]
  string CorrelationId { get; set; }

  [LogField("request-id", isHeader: true)]
  string RequestId { get; set; }

  [LogField("txn-id", isHeader: true)]
  string TransactionId { get; set; }

  [LogField("trace-id", isHeader: true)]
  string TraceId { get; set; }

  [LogField("span-id")]
  string SpanId { get; set; }

  [LogField("parent-span-id")]
  string ParentSpanId { get; set; }

  [LogField("service-name")]
  string ServiceName { get; set; }

  [LogField("environment")]
  string Environment { get; set; }

  [LogField("host-name")]
  string HostName { get; set; }

  [LogField("client-ip")]
  string ClientIp { get; set; }

  [LogField("server-ip")]
  string ServerIp { get; set; }

  [LogField("pod-name")]
  string PodName { get; set; }

  [LogField("container-id")]
  string ContainerId { get; set; }

  [LogField("category")]
  string Category { get; set; }

  [LogField("tenant-id", isHeader: true)]
  string TenantId { get; set; }

  [LogField("user-id", isHeader: true)]
  string UserId { get; set; }

  [LogField("session-id", isHeader: true)]
  string SessionId { get; set; }

  [LogField("device-id", isHeader: true)]
  string DeviceId { get; set; }

  [LogField("user-agent")]
  string UserAgent { get; set; }

  [LogField("http-method")]
  string HttpMethod { get; set; }

  [LogField("message")]
  string Message { get; set; }

  [LogField("request-url")]
  string RequestUrl { get; set; }

  [LogField("request-timestamp")]
  DateTime? RequestTimestamp { get; set; }

  [LogField("feature-flag", isHeader: true)]
  string FeatureFlag { get; set; }

  [LogField("is-debug-mode", isHeader: true)]
  bool? IsDebugMode { get; set; }

  [LogField("time-taken-ms")]
  double? TimeTakenMs { get; set; }

  [LogField("level")]
  public LogEventLevel Level { get; }
}