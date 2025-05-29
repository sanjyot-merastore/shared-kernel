using FluentAssertions;

using MeraStore.Shared.Kernel.Logging.Interfaces;

using Moq;
using Moq.Protected;

using System.Net;
using System.Text;

namespace MeraStore.Shared.Kernel.Http.Tests;
public class HttpRequestBuilderTests
{
    private readonly HttpClient _client;
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;

    public HttpRequestBuilderTests()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

        _client = new HttpClient(_httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri("https://test.local")
        };
    }

    //[Fact]
    //public async Task Should_Send_Get_Request_With_Default_Policies()
    //{
    //    // Arrange
    //    var responseContent = new StringContent("{\"message\":\"success\"}", Encoding.UTF8, "application/json");
    //    var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
    //    {
    //        Content = responseContent
    //    };

    //    _httpMessageHandlerMock.Protected()
    //        .Setup<Task<HttpResponseMessage>>(
    //            "SendAsync",
    //            ItExpr.IsAny<HttpRequestMessage>(),
    //            ItExpr.IsAny<CancellationToken>()
    //        )
    //        .ReturnsAsync(expectedResponse)
    //        .Verifiable();

    //    var builder = new HttpRequestBuilder()
    //        .WithMethod(HttpMethod.Get)
    //        .WithUri("https://test.local/api/data")
    //        .UseDefaultResilience()
    //        .WithLoggingField("client-id", "test-client")
    //        .Build();

    //    // Act
    //    var response = await builder.SendAsync();

    //    // Assert
    //    response.StatusCode.Should().Be(HttpStatusCode.OK);
    //    var content = await response.Content.ReadAsStringAsync();
    //    content.Should().Contain("success");

    //    _httpMessageHandlerMock.Protected().Verify(
    //        "SendAsync",
    //        Times.Once(),
    //        ItExpr.Is<HttpRequestMessage>(req =>
    //            req.Method == HttpMethod.Get &&
    //            req.RequestUri.ToString() == "https://test.local/api/data"
    //        ),
    //        ItExpr.IsAny<CancellationToken>()
    //    );
    //}

    //[Fact]
    //public async Task Should_Throw_When_Uri_Is_Missing()
    //{
    //    var builder = new HttpRequestBuilder()
    //        .WithMethod(HttpMethod.Get)
    //        .Build();

    //    Func<Task> act = () => builder.SendAsync();

    //    await act.Should().ThrowAsync<InvalidOperationException>()
    //        .WithMessage("Request URI is missing.");
    //}

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
    public void WithTimeout_ShouldAddTimeoutPolicy()
    {
        // Arrange
        var builder = new HttpRequestBuilder()
            .WithMethod(HttpMethod.Get)
            .WithUri("https://example.com")
            .WithTimeout(TimeSpan.FromSeconds(5));

        // Act
        var result = builder.Build();

        // Assert
        result.Policies.Should().Contain(p => p.GetType().Name.Contains("TimeoutPolicy"));
    }

    //[Fact]
    //public void MissingMethod_ShouldThrowException()
    //{
    //    // Arrange
    //    var builder = new HttpRequestBuilder()
    //        .WithUri("https://example.com");

    //    // Act
    //    var act = () => builder.Build();

    //    // Assert
    //    act.Should().Throw<MeraStore.Shared.Kernel.Common.Exceptions.Exceptions.ApiExceptions.MissingMethodException>();
    //}

    //[Fact]
    //public void MissingUri_ShouldThrowException()
    //{
    //    // Arrange
    //    var builder = new HttpRequestBuilder()
    //        .WithMethod(HttpMethod.Get);

    //    // Act
    //    var act = () => builder.Build();

    //    // Assert
    //    act.Should().Throw<MeraStore.Shared.Kernel.Common.Exceptions.Exceptions.ApiExceptions.MissingUriException>();
    //}

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
