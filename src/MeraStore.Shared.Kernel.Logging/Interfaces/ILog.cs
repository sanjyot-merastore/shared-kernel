namespace MeraStore.Shared.Kernel.Logging.Interfaces;

public interface ILog : ILogFields
{
  // Optional filters to apply when generating log fields
  ICollection<ILogFilter> Filters { get; }

  // Holds the final, filtered key-value log fields
  Dictionary<string, string> LoggingFields { get; }

  // Allows manual setting/updating of fields dynamically
  void TrySetLogField(string key, string value);

  // Should populate LoggingFields using the properties, attributes, and filters
  Task<Dictionary<string, string>> PopulateLogFields();
}