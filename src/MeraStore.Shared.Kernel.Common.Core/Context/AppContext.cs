namespace MeraStore.Shared.Kernel.Common.Context;

/// <summary>
/// Holds per-request scoped context data such as correlation IDs, tenant info, etc.
/// </summary>
public class AppContext
{
  public string ServiceName { get; set; }
  public string CorrelationId { get; set; }
  public string TransactionId { get; set; }
  public string RequestId { get; set; }
  public string TraceId { get; set; }
  public string TenantId { get; set; }
  public string SessionId { get; set; }
  public string UserToken { get; set; } // Optional, useful for internal forwarding/logging
  public DateTime? RequestStartTimestamp { get; set; }

  public Dictionary<string, string> Headers { get; set; } = new(StringComparer.OrdinalIgnoreCase);

  public static AppContext Current => AppContextScope.Current;
}