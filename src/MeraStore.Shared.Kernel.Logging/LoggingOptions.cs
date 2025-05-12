namespace MeraStore.Shared.Kernel.Logging;

public class LoggingOptions
{
  public bool UseConsole { get; set; } = true;
  public bool UseFile { get; set; } = false;
  public bool UseElasticsearch { get; set; } = false;
  public bool UseInfrastructureSink { get; set; } = false;
  public bool UseEntityFrameworkSink { get; set; } = false;
  public string ElasticsearchUrl { get; set; } = string.Empty;
}