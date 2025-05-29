using MeraStore.Shared.Kernel.Common.Exceptions.Exceptions;
using MeraStore.Shared.Kernel.Logging.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Polly;
using System.Text;
namespace MeraStore.Shared.Kernel.Http;

/// <summary>
/// A fluent builder for constructing and sending HTTP requests with optional resilience, logging, masking, and custom settings.
/// </summary>
public class HttpRequestBuilder
{
    private readonly HttpRequestMessage _request = new();
    private readonly Dictionary<string, string> _loggingFields = new();
    private readonly List<IMaskingFilter> _maskingFilters = [];
    private readonly List<IAsyncPolicy<HttpResponseMessage>> _policies = [];
    private IAsyncPolicy<HttpResponseMessage> _timeoutPolicy;

    private string _cid;
    private string _requestId;
    private HttpMethod _method;
    private HttpClient _client;

    private static readonly JsonSerializerSettings DefaultJsonSettings = new()
    {
        NullValueHandling = NullValueHandling.Ignore,
        Formatting = Formatting.None,
        ContractResolver = new CamelCasePropertyNamesContractResolver(),
        Converters = { new StringEnumConverter(new CamelCaseNamingStrategy()) }
    };

    private static readonly List<IAsyncPolicy<HttpResponseMessage>> DefaultPolicies =
    [
        HttpResiliencePolicies.RetryPolicy(),
        HttpResiliencePolicies.AdvancedCircuitBreakerPolicy
    ];

    /// <summary>Sets the HTTP method (GET, POST, etc.).</summary>
    public HttpRequestBuilder WithMethod(HttpMethod method)
    {
        _method = method;
        _request.Method = method;
        return this;
    }

    /// <summary>Adds a Polly policy to the request.</summary>
    public HttpRequestBuilder AddPolicy(IAsyncPolicy<HttpResponseMessage> policy)
    {
        _policies.Add(policy);
        return this;
    }

    /// <summary>Applies default retry and circuit breaker policies.</summary>
    public HttpRequestBuilder UseDefaultResilience()
    {
        _policies.AddRange(DefaultPolicies);
        return this;
    }

    /// <summary>Applies a custom Polly policy using a factory method.</summary>
    public HttpRequestBuilder UsePolicy(Func<IAsyncPolicy<HttpResponseMessage>> policyFactory)
    {
        _policies.Add(policyFactory());
        return this;
    }

    /// <summary>Sets the request timeout. Default is 60 seconds.</summary>
    public HttpRequestBuilder WithTimeout(TimeSpan? timeout = null)
    {
        var resolvedTimeout = timeout ?? TimeSpan.FromSeconds(60);
        _timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(resolvedTimeout);
        _policies.Add(_timeoutPolicy);
        return this;
    }

    /// <summary>Sets the request URI.</summary>
    public HttpRequestBuilder WithUri(string uri)
    {
        _request.RequestUri = new Uri(uri);
        return this;
    }

    /// <summary>Adds a custom header.</summary>
    public HttpRequestBuilder WithHeader(string name, string value)
    {
        _request.Headers.Add(name, value);
        return this;
    }

    /// <summary>Sets a raw HttpContent (advanced use).</summary>
    public HttpRequestBuilder WithContent(HttpContent content)
    {
        _request.Content = content;
        return this;
    }

    /// <summary>Sets byte array content (e.g., file uploads).</summary>
    public HttpRequestBuilder WithContent(byte[] data)
    {
        _request.Content = new ByteArrayContent(data);
        return this;
    }

    /// <summary>Sets form URL encoded content (e.g., application/x-www-form-urlencoded).</summary>
    public HttpRequestBuilder WithFormUrlEncodedContent(Dictionary<string, string> formData)
    {
        _request.Content = new FormUrlEncodedContent(formData);
        return this;
    }

    /// <summary>Sets stream content (e.g., file or memory streams).</summary>
    public HttpRequestBuilder WithContent(Stream stream)
    {
        _request.Content = new StreamContent(stream);
        return this;
    }

    /// <summary>Sets ReadOnlyMemory content.</summary>
    public HttpRequestBuilder WithContent(ReadOnlyMemory<byte> data)
    {
        _request.Content = new ReadOnlyMemoryContent(data);
        return this;
    }

    /// <summary>Sets string content with optional media type (default is text/plain).</summary>
    public HttpRequestBuilder WithContent(string content, string mediaType = "text/plain", Encoding? encoding = null)
    {
        _request.Content = new StringContent(content, encoding ?? Encoding.UTF8, mediaType);
        return this;
    }

    /// <summary>Sets multipart form data content (e.g., file uploads with metadata).</summary>
    public HttpRequestBuilder WithContent(MultipartFormDataContent multipartContent)
    {
        _request.Content = multipartContent;
        return this;
    }

    /// <summary>Sets multipart content (e.g., for MIME style body parts).</summary>
    public HttpRequestBuilder WithContent(MultipartContent multipartContent)
    {
        _request.Content = multipartContent;
        return this;
    }

    /// <summary>Sets JSON content from an object using default or custom serializer settings.</summary>
    public HttpRequestBuilder WithJsonContent<T>(T payload, JsonSerializerSettings? settings = null)
    {
        if (payload == null)
            return this;

        var serializerSettings = settings ?? DefaultJsonSettings;
        var json = JsonConvert.SerializeObject(payload, serializerSettings);
        _request.Content = new StringContent(json, Encoding.UTF8, "application/json");

        return this;
    }

    /// <summary>Sets the correlation ID for logging and tracing.</summary>
    public HttpRequestBuilder WithCorrelationId(string correlationId)
    {
        _cid = correlationId;
        return this;
    }

    /// <summary>Sets the request ID for logging and tracing.</summary>
    public HttpRequestBuilder WithRequestId(string requestId)
    {
        _requestId = requestId;
        return this;
    }

    /// <summary>Adds a single custom field to the API log.</summary>
    public HttpRequestBuilder WithLoggingField(string key, string value)
    {
        _loggingFields[key] = value;
        return this;
    }

    /// <summary>Adds multiple custom logging fields.</summary>
    public HttpRequestBuilder WithLoggingFields(Dictionary<string, string> fields)
    {
        foreach (var field in fields)
            _loggingFields[field.Key] = field.Value;

        return this;
    }

    /// <summary>Adds masking filters for sensitive log data.</summary>
    public HttpRequestBuilder WithMaskingFilters(params IMaskingFilter[] filters)
    {
        _maskingFilters.AddRange(filters);
        return this;
    }

    /// <summary>Allows overriding the default internal HttpClient.</summary>
    public HttpRequestBuilder WithHttpClient(HttpClient client)
    {
        _client = client;
        return this;
    }

    /// <summary>
    /// Builds the internal <see cref="HttpRequest"/> object containing all configurations, ready to be sent.
    /// </summary>
    public HttpRequest Build()
    {
        _request.Method = _method ?? throw new ApiExceptions.MissingMethodException();
        _ = _request.RequestUri ?? throw new ApiExceptions.MissingUriException();

        _timeoutPolicy ??= Policy.TimeoutAsync<HttpResponseMessage>(60);
        _policies.Add(_timeoutPolicy);

        var context = new HttpRequest(_request, _policies)
        {
            CorrelationId = _cid ?? Guid.NewGuid().ToString(),
            RequestId = _requestId ?? Guid.NewGuid().ToString(),
            LoggingFields = new Dictionary<string, string>(_loggingFields),
            MaskingFilters = [.. _maskingFilters],
            Method = _method,
            Client = _client ?? new HttpClient()
        };

        return context;
    }
}
