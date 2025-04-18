namespace MeraStore.Shared.Kernel.Logging.Abstraction;

public interface ILogFilter
{
  bool ShouldInclude(string fieldName, string value);
}