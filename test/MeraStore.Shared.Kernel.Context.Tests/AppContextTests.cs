using Microsoft.AspNetCore.Http;

namespace MeraStore.Shared.Kernel.Context.Tests
{
  public class AppContextTests
  {
    [Fact]
    public void Clone_ShouldCreateDeepCopyOfContext()
    {
      var original = new AppContextBase("TestService")
      {
        CorrelationId = "cid-123",
        TransactionId = "txn-456",
        RequestId = "req-789",
        TraceId = "trace-abc",
        TenantId = "tenant-xyz",
        SessionId = "sess-111",
        UserToken = "Bearer xyz",
        RequestStartTimestamp = DateTime.UtcNow,
        Headers = new Dictionary<string, string> { { "x-test", "value" } }
      };

      var clone = original.Clone();

      Assert.Equal(original.ServiceName, clone.ServiceName);
      Assert.Equal(original.CorrelationId, clone.CorrelationId);
      Assert.Equal(original.TransactionId, clone.TransactionId);
      Assert.Equal(original.RequestId, clone.RequestId);
      Assert.Equal(original.TraceId, clone.TraceId);
      Assert.Equal(original.TenantId, clone.TenantId);
      Assert.Equal(original.SessionId, clone.SessionId);
      Assert.Equal(original.UserToken, clone.UserToken);
      Assert.Equal(original.RequestStartTimestamp, clone.RequestStartTimestamp);
      Assert.Equal(original.Headers["x-test"], clone.Headers["x-test"]);
      Assert.NotSame(original.Headers, clone.Headers); // deep copy
    }

    [Fact]
    public void AppContextScope_ShouldPushAndPopContextCorrectly()
    {
      var context = new AppContextBase("TestService") { CorrelationId = "scope-123" };

      using (AppContextScope.BeginScope(context))
      {
        Assert.Equal("scope-123", AppContextBase.Current.CorrelationId);
      }

      Assert.Null(AppContextBase.Current.CorrelationId); // fallback context
    }

    [Fact]
    public void FromHttpContext_ShouldMapHeadersCorrectly()
    {
      var context = new DefaultHttpContext();
      var headers = context.Request.Headers;

      headers["ms-correlationid"] = "corr-id";
      headers["ms-txn-id"] = "txn-id";
      headers["ms-sessionid"] = "session-id";
      headers["ms-tenantid"] = "tenant-id";
      headers["Authorization"] = "Bearer token-value";

      var result = AppContextBase.FromHttpContext(context, "TestService");

      Assert.Equal("corr-id", result.CorrelationId);
      Assert.Equal("txn-id", result.TransactionId);
      Assert.Equal("session-id", result.SessionId);
      Assert.Equal("tenant-id", result.TenantId);
      Assert.Equal("Bearer token-value", result.UserToken);
      Assert.Equal("TestService", result.ServiceName);
      Assert.NotNull(result.RequestId);
      Assert.NotNull(result.TraceId);
      Assert.NotNull(result.RequestStartTimestamp);
      Assert.True(result.Headers.ContainsKey("Authorization"));
    }

    [Fact]
    public void FromHttpContext_ShouldGenerateDefaultsIfMissing()
    {
      var context = new DefaultHttpContext();

      var result = AppContextBase.FromHttpContext(context, "FallbackService");

      Assert.False(string.IsNullOrEmpty(result.CorrelationId));
      Assert.False(string.IsNullOrEmpty(result.TransactionId));
      Assert.False(string.IsNullOrEmpty(result.RequestId));
      Assert.False(string.IsNullOrEmpty(result.TraceId));
      Assert.Equal("FallbackService", result.ServiceName);
    }
  }

}
