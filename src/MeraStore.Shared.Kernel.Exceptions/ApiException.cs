namespace MeraStore.Shared.Kernel.Exceptions;

[ExcludeFromCodeCoverage]
public partial class ApiException : BaseAppException
{
    public ApiException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        ExceptionCategory category = ExceptionCategory.Network,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.ApiGateway, eventCode, errorCode, statusCode, message, category,
            severity)
    {
    }

    public ApiException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        Exception innerException,
        ExceptionCategory category = ExceptionCategory.Network,
        ExceptionSeverity severity = ExceptionSeverity.Critical)
        : base(Constants.ServiceIdentifiers.ApiGateway, eventCode, errorCode, statusCode, message, innerException,
            category, severity)
    {
    }

    public static ApiException MethodNotAllowed(string? message = null, Exception? innerException = null) =>
        new(
            EventCodeRegistry.GetCode(Constants.EventCodes.ApiError),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.MethodNotAllowedError),
            HttpStatusCode.MethodNotAllowed,
            message ?? "The HTTP method used is not allowed for the requested resource.",
            innerException
        );

    public static ApiException RequestTimeout(string? message = null, Exception? innerException = null) =>
        new(
            EventCodeRegistry.GetCode(Constants.EventCodes.ApiError),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.RequestTimeoutError),
            HttpStatusCode.RequestTimeout,
            message ?? "The request timed out before it could be processed.",
            innerException
        );

    public static ApiException Conflict(string? message = null, Exception? innerException = null) =>
        new(
            EventCodeRegistry.GetCode(Constants.EventCodes.ApiError),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.ConflictError),
            HttpStatusCode.Conflict,
            message ?? "A conflict occurred while processing the request.",
            innerException
        );

    public static ApiException UnsupportedMediaType(string? message = null, Exception? innerException = null) =>
        new(
            EventCodeRegistry.GetCode(Constants.EventCodes.ApiError),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.UnsupportedMediaTypeError),
            HttpStatusCode.UnsupportedMediaType,
            message ?? "The request contains an unsupported media type.",
            innerException
        );

    public static ApiException TooManyRequests(string? message = null, Exception? innerException = null) =>
        new(
            EventCodeRegistry.GetCode(Constants.EventCodes.ApiError),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.TooManyRequestsError),
            (HttpStatusCode)429, // Explicit cast to keep it sexy & RFC-compliant
            message ?? "Too many requests have been sent in a given amount of time.",
            innerException
        );

    public static ApiException JsonParsingError(string? message = null, Exception? innerException = null) =>
        new(
            EventCodeRegistry.GetCode(Constants.EventCodes.ApiError),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.JsonParsingError),
            HttpStatusCode.BadRequest,
            message ?? "The request payload contains invalid JSON.",
            innerException
        );

    public static ApiException IntegrationError(string? message = null, Exception? innerException = null) =>
        new(
            EventCodeRegistry.GetCode(Constants.EventCodes.ApiError),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.IntegrationError),
            HttpStatusCode.BadGateway,
            message ?? "An error occurred while integrating with an external system.",
            innerException
        );

    public static ApiException ContractViolationError(string? message = null, Exception? innerException = null) =>
        new(
            EventCodeRegistry.GetCode(Constants.EventCodes.ApiError),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.ContractViolationError),
            HttpStatusCode.BadRequest,
            message ?? "The API contract was violated—check the request or response schema.",
            innerException
        );
}