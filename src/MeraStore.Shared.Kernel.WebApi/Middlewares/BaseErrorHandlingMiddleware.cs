using MeraStore.Shared.Kernel.Exceptions;
using MeraStore.Shared.Kernel.Exceptions.Codes.Services;
using MeraStore.Shared.Kernel.Exceptions.Core;
using MeraStore.Shared.Kernel.Logging.Attributes;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using MeraStore.Shared.Kernel.Exceptions.Codes.Events;
using MeraStore.Shared.Kernel.Exceptions.Codes.Http;
using ValidationException = FluentValidation.ValidationException;

namespace MeraStore.Shared.Kernel.WebApi.Middlewares;

/// <summary>
/// Middleware for centralized exception handling across HTTP requests.
/// Captures and processes different exception types—including FluentValidation and custom application exceptions—
/// and returns standardized <see cref="ProblemDetails"/> or <see cref="ValidationProblemDetails"/> responses.
/// </summary>
/// <remarks>
/// This middleware also logs detailed error information using structured logging,
/// enriches responses with traceability metadata (e.g., <c>traceId</c>, <c>errorCode</c>, etc.),
/// and ensures consistency across microservices.
/// </remarks>
/// <param name="next">The next middleware component in the pipeline.</param>
/// <param name="logger">The logger instance used for writing error logs.</param>
public class BaseErrorHandlingMiddleware(RequestDelegate next, ILogger<BaseErrorHandlingMiddleware> logger)
{
    protected readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        NullValueHandling = NullValueHandling.Ignore, // Ignore nulls to keep payloads clean
        Formatting = Formatting.Indented, // Use Formatting.Indented if you're in dev/debug
        MissingMemberHandling = MissingMemberHandling.Ignore, // Tolerate extra fields in input JSON
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore, // Avoid self-referencing nightmares
        DateFormatHandling = DateFormatHandling.IsoDateFormat, // Standard ISO 8601 formatting
        ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(), // Use camelCase in output
        Converters = { new Newtonsoft.Json.Converters.StringEnumConverter() } // Enums as strings
    };

    /// <summary>
    /// Core middleware logic that wraps the HTTP request pipeline in a try-catch block
    /// and handles different exception types gracefully.
    /// </summary>
    /// <param name="context">The current HTTP context.</param>
    public virtual async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            await HandleValidationExceptionAsync(context, ex);
        }
        catch (BaseAppException ex)
        {
            await HandleStructuredExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            var wrapped = new CommonException(
                EventCodeRegistry.GetCode(Constants.EventCodes.InternalServerError),
                ErrorCodeRegistry.GetCode(Constants.ErrorCodes.InternalServerError),
                HttpStatusCode.InternalServerError,
                "An unexpected server error occurred.",
                innerException: ex,
                category: ExceptionCategory.Operational,
                severity: ExceptionSeverity.Critical
            );

            await HandleStructuredExceptionAsync(context, wrapped);
        }
    }

    /// <summary>
    /// Handles <see cref="ValidationException"/>s by returning a <see cref="ValidationProblemDetails"/> response.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <param name="exception">The thrown <see cref="ValidationException"/>.</param>

    protected virtual async Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
    {
        logger.LogError(exception, "Validation error occurred: {@ValidationErrors}",
            exception.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray()));

        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = StatusCodes.Status400BadRequest;

        var validationErrors = exception.Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

        var problemDetails = new ValidationProblemDetails(validationErrors)
        {
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
            Status = context.Response.StatusCode,
            Title = "Validation failed.",
            Instance = context.TraceIdentifier
        };

        await context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails, JsonSerializerSettings));
    }

    /// <summary>
    /// Handles all <see cref="BaseAppException"/> types by returning structured <see cref="ProblemDetails"/>.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <param name="exception">The custom application exception.</param>
    protected virtual Task HandleStructuredExceptionAsync(HttpContext context, BaseAppException exception)
    {
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = (int)exception.StatusCode;

        var statusCode = context.Response.StatusCode;

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Type = GetProblemTypeUrl(statusCode),
            Title = "An error occurred.",
            Detail = exception.Message,
            Instance = context.TraceIdentifier
        };


        // Structured extensions
        problemDetails.Extensions["errorCode"] = exception.FullErrorCode;
        problemDetails.Extensions["category"] = exception.Category.ToString();
        problemDetails.Extensions["severity"] = exception.Severity.ToString();

        if (!string.IsNullOrWhiteSpace(exception.ServiceIdentifier) &&
            exception.ServiceIdentifier.All(char.IsDigit))
        {
            problemDetails.Extensions["service"] = ServiceCodeRegistry.GetKey(exception.ServiceIdentifier);
        }
        else
        {
            problemDetails.Extensions["service"] = exception.ServiceIdentifier;
        }

        // Optionally add correlation ID
        if (context.Request.Headers.TryGetValue("x-correlation-id", out var correlationId))
            problemDetails.Extensions["correlationId"] = correlationId.ToString();

        if (exception.InnerException is not null)
        {
            problemDetails.Extensions["innerException"] = new
            {
                message = exception.InnerException.Message,
                type = exception.InnerException.GetType().FullName,
#if DEBUG
                stackTrace = exception.InnerException.StackTrace
#endif
            };
        }

        logger.LogError(exception, "Structured error occurred: {Message} | Code: {Code} | Category: {Category}",
            exception.Message, exception.FullErrorCode, exception.Category);

        return context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails, JsonSerializerSettings));
    }


    /// <summary>
    /// Retrieves the event code metadata associated with the current request endpoint.
    /// Falls back to <c>InternalServerError</c> if none is found.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <returns>The resolved event code string.</returns>
    protected virtual string GetRequestEventCode(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        var code = endpoint?.Metadata.GetMetadata<EventCodeAttribute>()?.EventCode;

        return string.IsNullOrWhiteSpace(code)
            ? Constants.EventCodes.InternalServerError
            : code;
    }

    private static string GetProblemTypeUrl(int statusCode) =>
    statusCode switch
    {
        100 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.2.1", // Continue
        101 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.2.2", // Switching Protocols
        200 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.3.1", // OK
        201 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.3.2", // Created
        202 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.3.3", // Accepted
        204 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.3.5", // No Content
        301 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.4.2", // Moved Permanently
        302 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.4.3", // Found
        304 => "https://datatracker.ietf.org/doc/html/rfc7232#section-4.1",   // Not Modified
        307 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.4.7", // Temporary Redirect
        308 => "https://datatracker.ietf.org/doc/html/rfc7538#section-3",     // Permanent Redirect

        400 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1", // Bad Request
        401 => "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1",   // Unauthorized
        402 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.2", // Payment Required
        403 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3", // Forbidden
        404 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4", // Not Found
        405 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.5", // Method Not Allowed
        406 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.6", // Not Acceptable
        407 => "https://datatracker.ietf.org/doc/html/rfc7235#section-3.2",   // Proxy Authentication Required
        408 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.7", // Request Timeout
        409 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8", // Conflict
        410 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.9", // Gone
        411 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.10",// Length Required
        412 => "https://datatracker.ietf.org/doc/html/rfc7232#section-4.2",   // Precondition Failed
        413 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.11",// Payload Too Large
        414 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.12",// URI Too Long
        415 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.13",// Unsupported Media Type
        416 => "https://datatracker.ietf.org/doc/html/rfc7233#section-4.4",   // Range Not Satisfiable
        417 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.14",// Expectation Failed
        422 => "https://datatracker.ietf.org/doc/html/rfc4918#section-11.2",  // Unprocessable Entity (WebDAV)
        426 => "https://datatracker.ietf.org/doc/html/rfc2817#section-7",     // Upgrade Required

        500 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1", // Internal Server Error
        501 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.2", // Not Implemented
        502 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.3", // Bad Gateway
        503 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.4", // Service Unavailable
        504 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.5", // Gateway Timeout
        505 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.6", // HTTP Version Not Supported

        _ => "https://datatracker.ietf.org/doc/html/rfc7231"                  // Fallback
    };

}
