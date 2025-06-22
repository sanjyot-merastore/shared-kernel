using MeraStore.Shared.Kernel.Exceptions;
using MeraStore.Shared.Kernel.Exceptions.Core;
using MeraStore.Shared.Kernel.Exceptions.Helpers;
using MeraStore.Shared.Kernel.Logging.Attributes;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using System.Net;
using ValidationException = FluentValidation.ValidationException;

namespace MeraStore.Shared.Kernel.WebApi.Middleware;

/// <summary>
/// Base error-handling middleware that captures exceptions and converts them into structured ProblemDetails.
/// Extend this middleware to customize behavior in your service.
/// </summary>
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
            Type = GetRequestEventCode(context),
            Status = context.Response.StatusCode,
            Title = "Validation failed.",
            Instance = context.TraceIdentifier
        };

        await context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails, JsonSerializerSettings));
    }

    protected virtual Task HandleStructuredExceptionAsync(HttpContext context, BaseAppException exception)
    {
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = (int)exception.StatusCode;

        var problemDetails = new ProblemDetails();
        problemDetails.Status = context.Response.StatusCode;
        problemDetails.Type = exception.EventCode ?? GetRequestEventCode(context);
        problemDetails.Title = "An error occurred while processing your request.";
        problemDetails.Detail = exception.Message;
        problemDetails.Instance = context.TraceIdentifier;

        problemDetails.Extensions["errorCode"] = exception.FullErrorCode;
        problemDetails.Extensions["category"] = exception.Category.ToString();
        problemDetails.Extensions["severity"] = exception.Severity.ToString();
        problemDetails.Extensions["service"] = ServiceCodeRegistry.GetKey(exception.ServiceIdentifier);
        problemDetails.Extensions["traceId"] = context.TraceIdentifier;

        logger.LogError(exception, "Structured error occurred: {Message} | Code: {Code} | Category: {Category}",
            exception.Message, exception.FullErrorCode, exception.Category);

        return context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails, JsonSerializerSettings));
    }

    protected virtual string GetRequestEventCode(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        var code = endpoint?.Metadata.GetMetadata<EventCodeAttribute>()?.EventCode;

        return string.IsNullOrWhiteSpace(code)
            ? Constants.EventCodes.InternalServerError
            : code;
    }
}
