using System.Diagnostics.CodeAnalysis;

namespace MeraStore.Shared.Kernel.Logging.Attributes;

[ExcludeFromCodeCoverage]
public class EventCodeAttribute(string code): Attribute
{
    public string EventCode { get; } = code;
}
