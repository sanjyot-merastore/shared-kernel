using MeraStore.Shared.Kernel.Logging.Interfaces;

namespace MeraStore.Shared.Kernel.Logging.Filters;

public class ExcludeFieldsFilter(IEnumerable<string> excludedFields) : ILogFilter
{
  private readonly HashSet<string> _excludedFields = [..excludedFields];

  public bool ShouldInclude(string fieldName, string value)
    => !_excludedFields.Contains(fieldName);
}