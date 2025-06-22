using MeraStore.Shared.Kernel.Http;

using Microsoft.AspNetCore.Mvc;

namespace MeraStore.Services.Sample.Api.Controllers;

/// <summary>
/// Controller responsible for interacting with the centralized logging service.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class LoggingController(HttpClient httpClient) : ControllerBase
{
    /// <summary>
    /// Retrieves the request payload from the Logging API for the specified request ID.
    /// </summary>
    /// <param name="id">The unique request identifier.</param>
    /// <param name="cancellationToken">Cancellation token to abort the operation.</param>
    /// <returns>Returns the raw JSON payload of the request log from the logging API.</returns>
    [HttpGet("request-payload/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(499)]
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
                .WithLoggingField("controller", nameof(LoggingController))
                .WithLoggingField("action", "get_request_payload")
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
            // TODO: Inject ILogger<LoggingController> and log the exception for observability
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
