using MeraStore.Shared.Kernel.Http;
using MeraStore.Shared.Kernel.Logging.Interfaces;
using Moq;
using MeraStore.Shared.Kernel.Exceptions;

public class HttpRequestBuilderTests
{
    [Fact]
    public void Build_Throws_WhenMethodNotSet()
    {
        var builder = new HttpRequestBuilder()
            .WithUri("https://example.com");

        var ex = Assert.Throws<ApiException>(() => builder.Build());
    }

    [Fact]
    public void Build_Throws_WhenUriNotSet()
    {
        var builder = new HttpRequestBuilder()
            .WithMethod(HttpMethod.Get);

        var ex = Assert.Throws<ApiException>(() => builder.Build());
    }

    [Fact]
    public void Build_SetsDefaultTimeoutPolicy()
    {
        var builder = new HttpRequestBuilder()
            .WithMethod(HttpMethod.Get)
            .WithUri("https://example.com");

        var request = builder.Build();

        Assert.NotNull(request);
        Assert.NotNull(request.FaultPolicy);
    }

    [Fact]
    public void WithCorrelationId_SetsCorrelationId()
    {
        var cid = Guid.NewGuid().ToString();
        var builder = new HttpRequestBuilder()
            .WithMethod(HttpMethod.Get)
            .WithUri("https://example.com")
            .WithCorrelationId(cid);

        var request = builder.Build();

        Assert.Equal(cid, request.CorrelationId);
    }

    [Fact]
    public void WithRequestId_SetsRequestId()
    {
        var reqId = Guid.NewGuid().ToString();
        var builder = new HttpRequestBuilder()
            .WithMethod(HttpMethod.Get)
            .WithUri("https://example.com")
            .WithRequestId(reqId);

        var request = builder.Build();

        Assert.Equal(reqId, request.RequestId);
    }


    
    [Fact]
    public void WithLoggingField_AddsLoggingField()
    {
        var builder = new HttpRequestBuilder()
            .WithMethod(HttpMethod.Get)
            .WithUri("https://example.com")
            .WithLoggingField("user", "darling");

        var request = builder.Build();

        Assert.True(request.LoggingFields.ContainsKey("user"));
        Assert.Equal("darling", request.LoggingFields["user"]);
    }

    [Fact]
    public void WithMaskingFilters_AddsMaskingFilters()
    {
        var mockFilter = new Mock<IMaskingFilter>();
        var builder = new HttpRequestBuilder()
            .WithMethod(HttpMethod.Get)
            .WithUri("https://example.com")
            .WithMaskingFilters(mockFilter.Object);

        var request = builder.Build();

        Assert.Contains(mockFilter.Object, request.MaskingFilters);
    }
}
