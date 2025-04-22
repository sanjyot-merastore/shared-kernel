﻿using Serilog.Events;

namespace MeraStore.Services.Logging.Domain.LogSinks;

public class InfraLogsElasticsearchSink(string elasticsearchUrl)
  : BaseElasticsearchSink(elasticsearchUrl, Shared.Kernel.Logging.Constants.Logging.Elasticsearch.InfraIndexFormat)
{
  public override void Emit(LogEvent logEvent)
  {
    var logEntry = GetCommonLogFields(logEvent);
    var sourceContext = logEntry[Shared.Kernel.Logging.Constants.Logging.LogFields.SourceContext]?.ToString();

    if (sourceContext == null || !sourceContext.StartsWith("Microsoft.AspNetCore"))
      return; // Ignore logs that don't belong here

    Task.Run(async () => await Client.IndexAsync(logEntry, idx => idx.Index($"{Shared.Kernel.Logging.Constants.Logging.Elasticsearch.InfraIndexFormat}{DateTime.UtcNow:yyyy-MM-dd}")));
  }
}