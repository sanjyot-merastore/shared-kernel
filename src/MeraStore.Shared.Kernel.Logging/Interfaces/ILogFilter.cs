namespace MeraStore.Shared.Kernel.Logging.Interfaces;

public interface ILogFilter
{
  bool ShouldInclude(string fieldName, string value);
}