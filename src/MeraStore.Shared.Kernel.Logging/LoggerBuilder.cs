using MeraStore.Shared.Kernel.Exceptions;
using MeraStore.Shared.Kernel.Logging.Interfaces;
using MeraStore.Shared.Kernel.Logging.Sinks;
using MeraStore.Shared.Kernel.Logging.Sinks.ElasticSearch;

using Microsoft.Extensions.DependencyInjection;

namespace MeraStore.Shared.Kernel.Logging;

public class LoggerBuilder
{
  private string _serviceName;
  private readonly LoggerConfiguration _loggerConfig;
  private readonly List<ILogSink> _customSinks;

  public LoggerBuilder(IServiceCollection services)
  {
    _customSinks = [];
    _loggerConfig = new LoggerConfiguration().Enrich.FromLogContext();
  }

  public LoggerBuilder WithServiceName(string serviceName)
  {
    if (string.IsNullOrWhiteSpace(serviceName))
      throw CommonException.MissingParameter("Service name must not be null, empty, or whitespace.");

    _serviceName = serviceName;
    _loggerConfig.Enrich.WithProperty(Constants.Logging.LogFields.ServiceName, serviceName);
    return this;
  }

  public LoggerBuilder AddConsoleSink()
  {
    var outputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}";
    _loggerConfig.WriteTo.Console(outputTemplate: outputTemplate);
    _customSinks.Add(new ConsoleLogSink());
    return this;
  }

  public LoggerBuilder AddFileSink(string filePath = "logs/log.txt")
  {
    _loggerConfig.WriteTo.File(filePath);
    _customSinks.Add(new FileLogSink(filePath));
    return this;
  }

  public LoggerBuilder AddElasticsearchSink(string elasticsearchUrl, string? indexFormat = null)
  {
    var elasticSink = new ApplicationSink(_serviceName, elasticsearchUrl);
    _loggerConfig.WriteTo.Sink(elasticSink);
    _customSinks.Add(elasticSink);
    return this;
  }

  public LoggerBuilder AddInfrastructureSink(string elasticsearchUrl, string? indexFormat = null)
  {
    var infrastructureSink = new InfrastructureSink(_serviceName, elasticsearchUrl);
    _loggerConfig.WriteTo.Sink(infrastructureSink);
    return this;
  }
  public LoggerBuilder AddEntityFrameworkSink(string elasticsearchUrl, string? indexFormat = null)
  {
    var logEventSink = new EntityFrameworkSink(_serviceName, elasticsearchUrl);
    _loggerConfig.WriteTo.Sink(logEventSink);
    return this;
  }

  public ILogger Build()
  {
    LogWriter.Configure(_customSinks.ToArray());
    return _loggerConfig.CreateLogger();
  }
}