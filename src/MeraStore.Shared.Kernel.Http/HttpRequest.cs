using MeraStore.Shared.Kernel.Logging.Interfaces;

using Polly;

namespace MeraStore.Shared.Kernel.Http;

public class HttpRequest(HttpRequestMessage request, List<IAsyncPolicy<HttpResponseMessage>> policies)
{
    public string CorrelationId { get; init; }
    public string RequestId { get; init; }
    public HttpMethod Method { get; init; }
    public Dictionary<string, string> LoggingFields { get; init; } = new();
    public List<IMaskingFilter> MaskingFilters { get; init; } = [];
    public HttpClient? Client { get; init; }
    public HttpRequestMessage Request { get; } = request ?? throw new ArgumentNullException(nameof(request));
    public IAsyncPolicy<HttpResponseMessage> FaultPolicy => Policy.WrapAsync(_policies.ToArray());

    private readonly List<IAsyncPolicy<HttpResponseMessage>> _policies = policies ?? throw new ArgumentNullException(nameof(policies));
}