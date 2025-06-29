using MeraStore.Shared.Kernel.Exceptions.Codes.Events;

namespace MeraStore.Shared.Kernel.Exceptions.Tests.Codes.Events;

public class EventCodeRegistryTests
{
    [Fact]
    public void GetCode_ShouldReturnCoreCode_WhenKeyExistsInCore()
    {
        // Arrange
        var key = "InvalidOperation"; // Assuming this key exists in EventCodes.Codes

        // Act
        var code = EventCodeRegistry.GetCode(key);

        // Assert
        Assert.False(string.IsNullOrWhiteSpace(code));
    }

    [Fact]
    public void Register_ShouldAddAdditionalCodeSuccessfully()
    {
        // Arrange
        var key = "MyCustomEventKey";
        var code = "EVT-999";

        // Act
        EventCodeRegistry.Register(key, code);
        var result = EventCodeRegistry.GetCode(key);

        // Assert
        Assert.Equal(code, result);
    }

    [Fact]
    public void Register_ShouldThrowException_WhenKeyAlreadyExists()
    {
        // Arrange
        var key = "DuplicateKey";
        var code1 = "EVT-001";
        var code2 = "EVT-002";

        EventCodeRegistry.Register(key, code1);

        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() =>
            EventCodeRegistry.Register(key, code2));

        Assert.Contains("already exists", ex.Message);
    }

    [Theory]
    [InlineData(null, "EVT-100")]
    [InlineData("   ", "EVT-101")]
    [InlineData("ValidKey", null)]
    [InlineData("ValidKey", "   ")]
    public void Register_ShouldThrowArgumentException_WhenKeyOrCodeIsInvalid(string key, string code)
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            EventCodeRegistry.Register(key, code));

        Assert.Contains("Invalid event code or key", ex.Message);
    }
}