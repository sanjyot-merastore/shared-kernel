using FluentAssertions;
using MeraStore.Shared.Kernel.Logging.Interfaces;
using Moq;

namespace MeraStore.Shared.Kernel.Http.Tests;
public class HttpRequestBuilderTests
{
    private readonly HttpClient _client;

    public HttpRequestBuilderTests()
    {
        var httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

        _client = new HttpClient(httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri("https://test.local")
        };
    }


    [Fact]
    public void Build_WithRequiredFields_ShouldReturnConfiguredHttpRequest()
    {
        // Arrange
        var builder = new HttpRequestBuilder()
            .WithMethod(HttpMethod.Post)
            .WithUri("https://example.com/api/test");

        // Act
        var result = builder.Build();

        // Assert
        result.Should().NotBeNull();
        result.Request.Should().NotBeNull();
        result.Request.Method.Should().Be(HttpMethod.Post);
        result.Request.RequestUri.Should().Be("https://example.com/api/test");
    }

    [Fact]
    public void WithJsonContent_ShouldSetProperContentType()
    {
        // Arrange
        var payload = new { Name = "Boss", Level = 100 };
        var builder = new HttpRequestBuilder()
            .WithMethod(HttpMethod.Post)
            .WithUri("https://example.com")
            .WithJsonContent(payload);

        // Act
        var result = builder.Build();

        // Assert
        result.Request.Content?.Headers?.ContentType?.MediaType.Should().Be("application/json");
    }

    [Fact]
    public void WithHeader_ShouldAddCustomHeader()
    {
        // Arrange
        var builder = new HttpRequestBuilder()
            .WithMethod(HttpMethod.Get)
            .WithUri("https://example.com")
            .WithHeader("X-Custom-Header", "value");

        // Act
        var request = builder.Build().Request;

        // Assert
        request.Headers.Contains("X-Custom-Header").Should().BeTrue();
    }

    [Fact]
    public void WithLoggingField_ShouldAddCustomLoggingData()
    {
        // Arrange
        var builder = new HttpRequestBuilder()
            .WithMethod(HttpMethod.Get)
            .WithUri("https://example.com")
            .WithLoggingField("userId", "1234");

        // Act
        var result = builder.Build();

        // Assert
        result.LoggingFields.Should().ContainKey("userId").WhoseValue.Should().Be("1234");
    }

    [Fact]
    public void WithMaskingFilters_ShouldApplyMaskingFilters()
    {
        // Arrange
        var mockFilter = new Mock<IMaskingFilter>();
        var builder = new HttpRequestBuilder()
            .WithMethod(HttpMethod.Get)
            .WithUri("https://example.com")
            .WithMaskingFilters(mockFilter.Object);

        // Act
        var result = builder.Build();

        // Assert
        result.MaskingFilters.Should().ContainSingle().Which.Should().Be(mockFilter.Object);
    }

    [Fact]
    public void WithHttpClient_ShouldUseProvidedClient()
    {
        // Arrange
        var client = new HttpClient();
        var builder = new HttpRequestBuilder()
            .WithMethod(HttpMethod.Get)
            .WithUri("https://example.com")
            .WithHttpClient(client);

        // Act
        var result = builder.Build();

        // Assert
        result.Client.Should().Be(client);
    }
}
