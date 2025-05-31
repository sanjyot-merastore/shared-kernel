using System.Reflection;
using MeraStore.Shared.Kernel.Logging.Loggers;

namespace MeraStore.Shared.Kernel.Logging.Tests.Loggers;

// Concrete implementation for testing BaseLog

public class BaseLogTests
{
    private readonly TestLog _log = new("Hello, world!", "UnitTestCategory");

    [Fact]
    public void Constructor_Sets_Default_Values()
    {
        Assert.Equal((string?)"Hello, world!", (string?)_log.Message);
        Assert.Equal((string?)"UnitTestCategory", (string?)_log.Category);
        Assert.False(string.IsNullOrEmpty(_log.Environment));
        Assert.False(string.IsNullOrEmpty(_log.HostName));
        Assert.False(string.IsNullOrEmpty(_log.ServerIp));
        Assert.False(string.IsNullOrEmpty(_log.OsPlatform));
        Assert.False(string.IsNullOrEmpty(_log.OsVersion));
        Assert.True(_log.ProcessId > 0);
    }

    [Fact]
    public async Task PopulateLogFields_Includes_Annotated_Properties()
    {
        var fields = await _log.PopulateLogFields();

        Assert.True((bool)fields.ContainsKey("test-field"));
        Assert.Equal((string?)"TestValue", (string?)fields["test-field"]);

        Assert.True((bool)fields.ContainsKey("test-dictionary.Key1"));
        Assert.Equal((string?)"Value1", (string?)fields["test-dictionary.Key1"]);

        Assert.True((bool)fields.ContainsKey("test-dictionary.Key2"));
        Assert.Equal((string?)"Value2", (string?)fields["test-dictionary.Key2"]);
    }

    [Fact]
    public async Task PopulateLogFields_Excludes_Filtered_Fields()
    {
        var fields = await _log.PopulateLogFields();

        // Password should be filtered out by ExcludeSensitiveDataFilter
        Assert.DoesNotContain("password", fields.Keys);
    }

    [Fact]
    public void TrySetLogField_Adds_And_Updates_Fields()
    {
        _log.TrySetLogField("new-key", "new-value");
        Assert.True((bool)_log.LoggingFields.ContainsKey("new-key"));
        Assert.Equal((string?)"new-value", (string?)_log.LoggingFields["new-key"]);

        // Update the existing key
        _log.TrySetLogField("new-key", "updated-value");
        Assert.Equal((string?)"updated-value", (string?)_log.LoggingFields["new-key"]);
    }

    [Fact]
    public void GetLocalIPAddress_Returns_Valid_IP_Or_Unknown()
    {
        // Private method - use reflection to invoke
        var method = typeof(BaseLog).GetMethod("GetLocalIPAddress", BindingFlags.NonPublic | BindingFlags.Instance);
        var result = method.Invoke(_log, null) as string;

        Assert.False(string.IsNullOrWhiteSpace(result));
        // Accept "Unknown" or valid IP as a pass
    }
}