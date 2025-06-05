using MeraStore.Shared.Kernel.Logging.Attributes;
using MeraStore.Shared.Kernel.Logging.Loggers;

namespace MeraStore.Shared.Kernel.Logging.Tests.Loggers;

public class TestLog(string message, string category = null) : BaseLog(message, category)
{
    public override string GetLevel() => "TestLevel";

    [LogField("test-field")]
    public string TestField { get; set; } = "TestValue";

    [LogField("test-dictionary", isPrefix: true)]
    public Dictionary<string, string> TestDict { get; set; } = new()
    {
        { "Key1", "Value1" },
        { "Key2", "Value2" }
    };

    [LogField("password")]
    public string Password { get; set; } = "SuperSecret123";
}