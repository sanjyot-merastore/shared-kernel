using System.Diagnostics;
using System.Text;

using MeraStore.Shared.Kernel.Logging;
using MeraStore.Shared.Kernel.Logging.Interfaces;
using MeraStore.Shared.Kernel.Logging.Loggers;

using AppContext = MeraStore.Services.Sample.Api.AppContext;

namespace MeraStore.Services.Logging.Api.Middlewares;

public class ApiLoggingMiddleware(RequestDelegate next, IMaskingFilter maskingFilter)
{
  public async Task InvokeAsync(HttpContext context)
  {
    var stopwatch = Stopwatch.StartNew();
    var request = context.Request;
    var response = context.Response;

    // Clone the request body for logging
    context.Request.EnableBuffering();
    var requestBody = await ReadRequestBodyAsync(request);
    context.Request.Body.Position = 0;

    // Replace response stream with memory stream to capture response
    var originalBodyStream = response.Body;
    await using var responseBody = new MemoryStream();
    response.Body = responseBody;

    try
    {
      await next(context); // Call the next middleware
    }
    finally
    {
      stopwatch.Stop();

      // Mask and log request payload
      var maskedRequestBody = maskingFilter.MaskRequestPayload(requestBody);
      var maskedResponseBody = await MaskAndReadResponseBody(responseBody);

      var apiLog = new ApiLog("app_request")
      {
        CorrelationId = AppContext.Current.CorrelationId,
        HttpMethod = request.Method,
        RequestUrl = $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}",
        ResponseStatusCode = response.StatusCode,
        Request = new Payload(Encoding.UTF8.GetString(maskedRequestBody)),
        Response = new Payload(Encoding.UTF8.GetString(maskedResponseBody)),
        TimeTakenMs = stopwatch.ElapsedMilliseconds,
        RequestTimestamp = DateTime.UtcNow,
        RequestId = AppContext.Current.RequestId
      };

      // Log the complete request and response information
      await Logger.LogAsync(apiLog);

      // Copy the response back to the original stream
      response.Body.Seek(0, SeekOrigin.Begin);
      await response.Body.CopyToAsync(originalBodyStream);
    }
  }

  private async Task<byte[]> ReadRequestBodyAsync(HttpRequest request)
  {
    using var ms = new MemoryStream();
    await request.Body.CopyToAsync(ms);
    return ms.ToArray();
  }

  private async Task<byte[]> MaskAndReadResponseBody(Stream responseStream)
  {
    responseStream.Seek(0, SeekOrigin.Begin);
    using var ms = new MemoryStream();
    await responseStream.CopyToAsync(ms);
    return maskingFilter.MaskResponsePayload(ms.ToArray());
  }
}