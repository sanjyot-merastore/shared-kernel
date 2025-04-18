using MeraStore.Shared.Kernel.Logging.Abstraction;

namespace MeraStore.Shared.Kernel.Logging.Filters;

public class DebugModeFilter(bool isDebug) : ILogFilter
{
  public bool ShouldInclude(string fieldName, string value)
  {
    return isDebug || fieldName != "user-agent";
  }
}