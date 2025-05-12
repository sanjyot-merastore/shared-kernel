using MeraStore.Shared.Kernel.Logging.Attributes;
using MeraStore.Shared.Kernel.Logging.Interfaces;
using System.Net;
using System.Reflection;
using MeraStore.Shared.Kernel.Logging.Filters;
using System.Runtime.InteropServices;

namespace MeraStore.Shared.Kernel.Logging.Loggers;

public abstract class BaseLog : ILog
{
  // Constructor to initialize the message and category (with default values for others)
  protected BaseLog(string message, string category = null)
  {
    Message = message;
    Category = category ?? string.Empty;

    // Default fields initialized in the constructor

    Environment = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"; // Default to "Production"
    HostName = Dns.GetHostName();
    ServerIp = GetLocalIPAddress();  // Assume server and client IP are the same for this example
    PodName = System.Environment.GetEnvironmentVariable("POD_NAME"); // Example for Kubernetes Pod name
    ContainerId = System.Environment.GetEnvironmentVariable("CONTAINER_ID"); // Example for Docker container ID
    FeatureFlag = "None"; // You can dynamically populate this if needed
    IsDebugMode = false; // Set it as needed
    OsPlatform = RuntimeInformation.OSDescription;
    OsVersion = System.Environment.OSVersion.ToString();
    ProcessId = System.Environment.ProcessId;
  }

  // Default field properties with `LogField` attribute
  [LogField("correlation-id", isHeader: true)]
  public string CorrelationId { get; set; }

  [LogField("request-id", isHeader: true)]
  public string RequestId { get; set; }

  [LogField("app-domain", isHeader: true)]
  public string AppDomain { get; set; }

  [LogField("txn-id", isHeader: true)]
  public string TransactionId { get; set; }

  [LogField("trace-id", isHeader: true)]
  public string TraceId { get; set; }

  [LogField("span-id")]
  public string SpanId { get; set; }

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

  [LogField("category")]
  public string Category { get; set; } = string.Empty;

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


  [LogField("feature-flag", isHeader: true)]
  public string FeatureFlag { get; set; }

  [LogField("is-debug-mode", isHeader: true)]
  public bool? IsDebugMode { get; set; }

  [LogField("time-taken-ms")]
  public double? TimeTakenMs { get; set; }

  [LogField("os-platform")]
  public string OsPlatform { get; set; }

  [LogField("os-version")]
  public string OsVersion { get; set; }

  [LogField("process-id")]
  public int ProcessId { get; set; }

  [LogField("level")]
  public string Level => GetLevel();

  public abstract string GetLevel();

  // Filters for sensitive data exclusion (you can customize the list)
  public ICollection<ILogFilter> Filters { get; } = new List<ILogFilter>
  {
    new ExcludeSensitiveDataFilter(new List<string> { "password", "secrets", "credentials" })
  };

  // Dictionary to store log fields dynamically
  public Dictionary<string, string> LoggingFields { get; private set; } = new();

  // Method to try adding or updating log field
  public void TrySetLogField(string key, string value)
  {
    if (LoggingFields.ContainsKey(key))
    {
      LoggingFields[key] = value; // Update existing field
    }
    else
    {
      LoggingFields.TryAdd(key, value); // Add new field
    }
  }

  // Method to populate the log fields with dynamic values
  public virtual async Task<Dictionary<string, string>> PopulateLogFields()
  {
    return await SetLoggingFields();
  }

  // Set logging fields dynamically using reflection and custom attributes
  private async Task<Dictionary<string, string>> SetLoggingFields()
  {
    var logFields = new Dictionary<string, string>();
    var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

    foreach (var prop in properties)
    {
      var attr = prop.GetCustomAttribute<LogFieldAttribute>();
      if (attr == null) continue;

      var value = prop.GetValue(this);
      if (value == null) continue;

      if (value is string stringVal && string.IsNullOrWhiteSpace(stringVal))
        continue; // Skip null or whitespace strings

      // Handle dictionary values (e.g., for additional structured fields)
      if (value is IDictionary<string, string> dictValue)
      {
        foreach (var kvp in dictValue)
        {
          if (string.IsNullOrWhiteSpace(kvp.Value)) continue;

          var key = attr.IsPrefix ? $"{attr.Name}.{kvp.Key}" : kvp.Key;
          if (ShouldInclude(key, kvp.Value))
          {
            logFields[key] = kvp.Value;
          }
        }
      }
      else
      {
        string stringValue = value switch
        {
          DateTime dt => dt.ToString("o"),
          bool b => b.ToString().ToLowerInvariant(),
          Enum e => e.ToString(),
          _ => value.ToString()
        };

        if (!string.IsNullOrEmpty(stringValue) && ShouldInclude(attr.Name, stringValue))
        {
          logFields[attr.Name] = stringValue;
        }
      }
    }

    foreach (var kvp in logFields)
    {
      LoggingFields.TryAdd(kvp.Key, kvp.Value);
    }

    return await Task.FromResult(logFields); // Return the result as a Task
  }



  // Method to check if a field should be included based on the filters
  private bool ShouldInclude(string fieldName, string value)
  {
    if (Filters == null || !Filters.Any()) return true;

    return Filters.All(filter => filter.ShouldInclude(fieldName, value));
  }

  // Helper method to get the local machine IP address
  private string GetLocalIPAddress()
  {
    string localIP = string.Empty;
    try
    {
      var host = Dns.GetHostEntry(Dns.GetHostName());
      var ipAddress = host.AddressList.FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
      localIP = ipAddress?.ToString() ?? "Unknown";
    }
    catch (Exception)
    {
      localIP = "Unknown";
    }
    return localIP;
  }
}