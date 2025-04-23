using MeraStore.Shared.Kernel.Logging.Interfaces;
using MeraStore.Shared.Kernel.Logging.Sinks;
using MeraStore.Shared.Kernel.Logging.Sinks.ElasticSearch;

using Microsoft.Extensions.DependencyInjection;

using Serilog;

namespace MeraStore.Shared.Kernel.Logging
{
  public class LoggerBuilder
  {
    private readonly string _serviceName;
    private readonly LoggerConfiguration _loggerConfiguration = new LoggerConfiguration().Enrich.FromLogContext();
    private readonly List<ILogSink> _logSinks = new List<ILogSink>();

    public LoggerBuilder(IServiceCollection services, string serviceName)
    {
      _serviceName = serviceName;
    }

    // Add Console Sink
    public LoggerBuilder AddConsoleSink()
    {
      _loggerConfiguration.WriteTo.Console();
      var sink = new ConsoleLogSink();
      _logSinks.Add(sink);
      return this;
    }

    // Add File Sink
    public LoggerBuilder AddFileSink(string filePath = "logs/log.txt")
    {
      _loggerConfiguration.WriteTo.File(filePath);
      var sink = new FileLogSink(filePath);
      _logSinks.Add(sink);
      return this;
    }

    // Add Elasticsearch Sink
    public LoggerBuilder AddElasticsearchSink(string elasticsearchUrl, string? indexFormat = null)
    {
      var sink = new ApplicationSink(_serviceName, elasticsearchUrl);
      _loggerConfiguration.WriteTo.Sink(sink);
      _logSinks.Add(sink);
      return this;
    }

    // Add LogWriter to the DI container
    public LoggerBuilder AddLogWriter()
    {
      LogWriter.Configure(_logSinks.ToArray());  // Configure LogWriter with the added sinks
      return this;
    }

    // Build and return the Serilog Logger
    public ILogger Build()
    {
      // Configure LogWriter before building the logger
      LogWriter.Configure(_logSinks.ToArray());

      // Return the final logger
      return _loggerConfiguration.CreateLogger();
    }
  }
}
