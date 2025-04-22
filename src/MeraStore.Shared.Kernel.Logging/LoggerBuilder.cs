using MeraStore.Services.Logging.Domain.LogSinks;
using MeraStore.Shared.Kernel.Logging.Interfaces;
using MeraStore.Shared.Kernel.Logging.Sinks;

using Microsoft.Extensions.DependencyInjection;

using Serilog;

namespace MeraStore.Shared.Kernel.Logging
{
  public class LoggerBuilder(IServiceCollection services)
  {
    private readonly LoggerConfiguration _loggerConfiguration = new LoggerConfiguration().Enrich.FromLogContext();

    // Add Console Sink
    public LoggerBuilder AddConsoleSink()
    {
      _loggerConfiguration.WriteTo.Console();
      services.AddSingleton<ILogSink, ConsoleLogSink>();
      return this;
    }

    // Add File Sink
    public LoggerBuilder AddFileSink(string filePath = "logs/log.txt")
    {
      _loggerConfiguration.WriteTo.File(filePath);
      services.AddSingleton<ILogSink, FileLogSink>();
      return this;
    }

    public LoggerBuilder AddElasticsearchSink(string elasticsearchUrl, string indexFormat =null)
    {
      services.AddSingleton<ILogSink>(new ElasticsearchLogSink(elasticsearchUrl, indexFormat));
      _loggerConfiguration.WriteTo.Sink(new ElasticsearchLogSink(elasticsearchUrl));
      _loggerConfiguration.WriteTo.Sink(new InfraLogsElasticsearchSink(elasticsearchUrl));
      return this;
    }


    // Configure LogWriter
    public LoggerBuilder AddLogWriter()
    {
      services.AddSingleton<ILogWriter, LogWriter>();
      return this;
    }

    // Build the logger and register it in the DI container
    public void Build()
    {
      _loggerConfiguration.WriteTo.Console();
      // Finalize the logger configuration
      Log.Logger = _loggerConfiguration
                    .CreateLogger();

    }
  }
}
