using MeraStore.Shared.Kernel.Logging.Interfaces;

namespace MeraStore.Shared.Kernel.Logging.Filters;

public class ExcludeSensitiveDataFilter(IEnumerable<string> excludedFields = null) : ILogFilter
{
  // List of field names to be excluded
  private readonly HashSet<string> _excludedFields = excludedFields == null ? [] : new HashSet<string>(excludedFields, StringComparer.OrdinalIgnoreCase);

  // Constructor to initialize with multiple field names to exclude
  // If excludedFields is null, use an empty HashSet

  // Implement the ShouldInclude method
  public bool ShouldInclude(string fieldName, string fieldValue)
  {
    // Exclude any fields that are in the _excludedFields list
    return !_excludedFields.Contains(fieldName);
  }
}