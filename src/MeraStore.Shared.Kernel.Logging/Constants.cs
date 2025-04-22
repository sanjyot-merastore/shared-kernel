namespace MeraStore.Shared.Kernel.Logging;

public static class Constants
{
  public const string ServiceName = "merastore-sample-api";

  public static class Logging
  {
    public static class Elasticsearch
    {
      public const string Url = "ElasticSearchUrl";
      public const string DefaultIndexFormat = "app-logs-";
      public const string InfraIndexFormat = "infra-logs-";
      public const string EFCoreIndexFormat = "ef-logs-";
    }

    public static class Headers
    {
      // Tracing & Observability
      public const string CorrelationId = "ms-correlationid";
      public const string RequestId = "ms-request-id";
      public const string TransactionId = "ms-txn-id";
      public const string TraceId = "ms-traceid";
      public const string SpanId = "ms-spanid";
      public const string ParentSpanId = "ms-parent-spanid";
      public const string RequestStart = "ms-requeststart";
      public const string ServiceName = "ms-servicename";
      public const string ClientIp = "ms-clientIp";

      // Security & Identity
      public const string UserId = "ms-userid";
      public const string ClientId = "ms-clientid";
      public const string SessionId = "ms-sessionid";
      public const string TenantId = "ms-tenantid";
      public const string Roles = "ms-roles";
      public const string Scope = "ms-scope";
      public const string AuthMethod = "ms-auth-method";
      public const string DeviceId = "ms-deviceid";
      public const string AppVersion = "ms-appversion";
      public const string ApiKey = "ms-api-key";
      public const string UserToken = "ms-user-token";

      // Performance & Debugging
      public const string Region = "ms-region";
      public const string Environment = "ms-environment";
      public const string ApiVersion = "ms-apiversion";
      public const string FeatureFlag = "ms-feature-flag";
      public const string DebugMode = "ms-debug-mode";

      // ---------------------- Standard Request Headers ----------------------

      public const string UserAgent = "user-agent";
      public const string Authorization = "authorization";
      public const string ContentType = "content-type";
      public const string Accept = "accept";
      public const string AcceptEncoding = "accept-encoding";
      public const string AcceptLanguage = "accept-language";
      public const string Host = "host";
      public const string Referer = "referer";
      public const string Origin = "origin";
      public const string Connection = "connection";
      public const string CacheControl = "cache-control";
      public const string Pragma = "pragma";
      public const string XForwardedFor = "x-forwarded-for";
      public const string XForwardedProto = "x-forwarded-proto";
      public const string XForwardedPort = "x-forwarded-port";
      public const string XRequestId = "x-request-id";
      public const string XRealIp = "x-real-ip";
      public const string XApiKey = "x-api-key";
      public const string IfModifiedSince = "if-modified-since";
      public const string IfNoneMatch = "if-none-match";
      public const string Range = "range";
    }


    public static class LogFields
    {
      public const string Timestamp = "timestamp";
      public const string SourceContext = "source-context";
      public const string Level = "level";
      public const string Message = "message";
      public const string CorrelationId = "correlation-id";
      public const string TransactionId = "txn-id";
      public const string RequestId = "request-id";
      public const string RequestUrl = "request-url";
      public const string RequestBaseUrl = "request-base-url";
      public const string QueryString = "query-string";
      public const string HttpMethod = "http-method";
      public const string HttpVersion = "http-version";
      public const string StatusCode = "status-code";
      public const string ClientIp = "client-ip";
      public const string UserAgent = "user-agent";
      public const string RequestPath = "request-path";
      public const string TenantId = "tenantid";
      public const string TraceId = "traceid";
      public const string ServiceName = "service-name";
    }

    public static class SystemMetadata
    {
      public const string HostName = "host-name";
      public const string PodName = "pod-name";
      public const string ContainerId = "container-id";
      public const string ServerIp = "server-ip";
      public const string ClientIp = "client-ip";
      public const string Environment = "environment";
      public const string OsPlatform = "os-platform";
      public const string OsVersion = "os-version";
      public const string ProcessId = "process-id";
      public const string ThreadId = "thread-id";
    }

    public static class ExceptionFields
    {
      public const string Exception = "exception";
      public const string InnerException = "inner-exception";
      public const string ExceptionMessage = "ex-message";
      public const string StackTrace = "ex-stack-trace";
      public const string ExceptionType = "ex-type";
      public const string ExceptionDetails = "ex-details";
      public const string IsHandled = "is-handled-exception";
    }
  }
}