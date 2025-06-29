using MeraStore.Shared.Kernel.Exceptions.Codes.Http;

namespace MeraStore.Shared.Kernel.Exceptions.Tests.Codes.Http;

public class ErrorCodeRegistryTests
{
    [Fact]
    public void GetCode_ShouldReturnCoreCode_WhenKeyExistsInCore()
    {
        // Arrange
        var key = "InvalidOperation"; // Must exist in ErrorCodes.Codes

        // Act
        var code = ErrorCodeRegistry.GetCode(key);

        // Assert
        Assert.False(string.IsNullOrWhiteSpace(code));
    }

    [Fact]
    public void Register_ShouldAddAdditionalCodeSuccessfully()
    {
        // Arrange
        var key = "CustomValidationError";
        var code = "ERR-777";

        // Act
        ErrorCodeRegistry.Register(key, code);
        var result = ErrorCodeRegistry.GetCode(key);

        // Assert
        Assert.Equal(code, result);
    }

    [Fact]
    public void Register_ShouldThrowInvalidOperationException_WhenKeyAlreadyExists()
    {
        // Arrange
        var key = "DuplicateErrorKey";
        var code1 = "ERR-101";
        var code2 = "ERR-202";

        ErrorCodeRegistry.Register(key, code1);

        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() =>
            ErrorCodeRegistry.Register(key, code2));

        Assert.Contains("already exists", ex.Message);
    }

    [Theory]
    [InlineData(null, "ERR-301")]
    [InlineData("   ", "ERR-302")]
    [InlineData("SomeValidKey", null)]
    [InlineData("SomeValidKey", "   ")]
    public void Register_ShouldThrowArgumentException_WhenKeyOrCodeIsInvalid(string key, string code)
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            ErrorCodeRegistry.Register(key, code));

        Assert.Contains("Invalid error code or key", ex.Message);
    }

}