using MeraStore.Shared.Kernel.Exceptions.Core;
using MeraStore.Shared.Kernel.WebApi.Middlewares;
using Microsoft.AspNetCore.Mvc;

namespace MeraStore.Services.Sample.Api.Middlewares;

/// <summary>
/// Application-specific error handling middleware that extends the shared base.
/// Override hook methods to inject custom handling logic.
/// </summary>
public class ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    : BaseErrorHandlingMiddleware(next, logger)
{
    /// <summary>
    /// Handles structured exceptions derived from <see cref="BaseAppException"/> and transforms them into
    /// RFC 7807-compliant <see cref="ProblemDetails"/> responses.
    /// Also logs the exception details using structured logging for diagnostics.
    /// </summary>
    /// <param name="context">HTTP context for the current request.</param>
    /// <param name="exception">The structured application exception instance.</param>
    /// <returns>A task that represents the asynchronous operation of writing the problem response.</returns>
    protected override Task HandleStructuredExceptionAsync(HttpContext context, BaseAppException exception)
    {
        return base.HandleStructuredExceptionAsync(context, exception);
    }
}