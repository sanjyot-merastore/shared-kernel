using MeraStore.Shared.Kernel.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeraStore.Services.Sample.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoggingController(HttpClient httpClient) : ControllerBase
{
    /// <summary>
    /// Gets the request payload from the logging API for a fixed request ID.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Raw JSON response from logging API</returns>
    [HttpGet("request-payload/{id}")]
    public async Task<IActionResult> GetRequestPayload(string id, CancellationToken cancellationToken)
    {
        var url = $"http://logging-api.merastore.com:8101/api/v1.0/logs/requests/payload/{id}";

        try
        {
            var builder = new HttpRequestBuilder()
              .WithMethod(HttpMethod.Get)
              .WithUri(url)
              .WithRequestId(AppContext.Current.RequestId)
              .WithCorrelationId(AppContext.Current.CorrelationId)
              .WithTimeout(TimeSpan.FromSeconds(15))
              .UseDefaultResilience()
              .WithLoggingField("controller", "LoggingApi")
              .WithLoggingField("action", "Get_Request_Payload".ToLower())
              .Build();

            var response = await builder.SendAsync(cancellationToken: cancellationToken);

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, "Failed to retrieve logging payload.");

            var content = await response.Content.ReadAsStringAsync(cancellationToken);

            return Content(content, "application/json");
        }
        catch (OperationCanceledException)
        {
            return StatusCode(499, "Request was canceled.");
        }
        catch (Exception ex)
        {
            // Log exception here if you have a logging mechanism
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}