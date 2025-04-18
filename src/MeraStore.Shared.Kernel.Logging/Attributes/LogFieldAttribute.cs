namespace MeraStore.Shared.Kernel.Logging.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class LogFieldAttribute(string name, bool isPrefix = false, bool isHeader = false) : Attribute
{
  public string Name { get; } = name;
  public bool IsPrefix { get; } = isPrefix;
  public bool IsHeader { get; } = isHeader;
}