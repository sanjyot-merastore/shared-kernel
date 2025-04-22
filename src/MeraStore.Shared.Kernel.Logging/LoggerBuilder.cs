using MeraStore.Shared.Kernel.Logging.Interfaces;
using MeraStore.Shared.Kernel.Logging.Sinks;
using MeraStore.Shared.Kernel.Logging.Sinks.ElasticSearch;

using Microsoft.Extensions.DependencyInjection;
using ILogger = Serilog.ILogger;

namespace MeraStore.Shared.Kernel.Logging
{
  public class LoggerBuilder(IServiceCollection services, string serviceName)
  {
    private readonly string _serviceName = serviceName;
    private readonly LoggerConfiguration _loggerConfiguration = new LoggerConfiguration().Enrich.FromLogContext();

    private readonly List<ILogSink> _logSinks = [];

    public LoggerBuilder AddConsoleSink()
    {
      _loggerConfiguration.WriteTo.Console();
      var sink = new ConsoleLogSink();
      _logSinks.Add(sink);
      services.AddSingleton<ILogSink>(sink);
      return this;
    }

    public LoggerBuilder AddFileSink(string filePath = "logs/log.txt")
    {
      _loggerConfiguration.WriteTo.File(filePath);
      var sink = new FileLogSink(filePath);
      _logSinks.Add(sink);
      services.AddSingleton<ILogSink>(sink);
      return this;
    }

    public LoggerBuilder AddElasticsearchSink(string elasticsearchUrl, string? indexFormat = null)
    {
      var sink = new ApplicationSink(_serviceName, elasticsearchUrl);
      _loggerConfiguration.WriteTo.Sink(sink);
      _logSinks.Add(sink);
      services.AddSingleton<ILogSink>(sink);
      return this;
    }

    public LoggerBuilder AddLogWriter()
    {
      var writer = new LogWriter();
     
      services.AddSingleton(writer);
      return this;
    }

    public ILogger Build()
    {
      LogWriter.Configure(_logSinks.ToArray());
      return _loggerConfiguration.CreateLogger();
    }
  }
}