using MeraStore.Shared.Kernel.Logging.Attributes;
using MeraStore.Shared.Kernel.Logging.Interfaces;
using MeraStore.Shared.Kernel.Logging.Loggers;

using Microsoft.AspNetCore.Routing;

using Moq;

namespace MeraStore.Shared.Kernel.Logging.Tests.Loggers;

public class ApiLogTests
{
    private readonly ApiLog _sut;

    public ApiLogTests()
    {
        _sut = new ApiLog("Test message");
        // Setup defaults to avoid null ref
        _sut.CorrelationId = Guid.NewGuid().ToString();
        _sut.Request = new Payload("{}");
        _sut.Response = new Payload("{}");
        _sut.RequestId = Guid.NewGuid().ToString();
        _sut.HttpMethod = "GET";
        _sut.RequestUrl = "https://test.url";
        _sut.RequestTimestamp = DateTime.UtcNow;
        _sut.MaskingFilters = new List<IMaskingFilter>(); // empty by default for now
    }

    [Fact]
    public void Constructor_ShouldInitializeWithMessage()
    {
        var log = new ApiLog("Hello");
        Assert.Equal("Hello", log.Message);
    }

    [Fact]
    public void GetLevel_ShouldReturnApi()
    {
        var level = _sut.GetLevel();
        Assert.Equal("api", level);
    }

    [Fact]
    public async Task PopulateLogFields_ShouldReturnFields()
    {
        // Arrange: no masking filters to skip external calls
        _sut.MaskingFilters.Clear();

        // Act
        var fields = await _sut.PopulateLogFields();

        // Assert basic fields are present (at least message)
        Assert.NotNull(fields);
        Assert.Contains("message", fields.Keys);
    }

    

    [Fact]
    public async Task PopulateLogFields_ShouldCatchExceptionDuringMasking()
    {
        // Arrange
        var maskerMock = new Mock<IMaskingFilter>();
        maskerMock.Setup(m => m.MaskRequestPayload(It.IsAny<byte[]>(), It.IsAny<string>())).Throws(new Exception("Masking error"));

        _sut.MaskingFilters.Add(maskerMock.Object);

        _sut.Request = new Payload("{\"foo\":\"bar\"}");
        _sut.Response = new Payload("{\"bar\":\"foo\"}");

        // Act & Assert - should NOT throw
        var ex = await Record.ExceptionAsync(() => _sut.PopulateLogFields());
        Assert.Null(ex);
    }
}
