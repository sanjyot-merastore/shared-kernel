namespace MeraStore.Shared.Kernel.Exceptions;

[ExcludeFromCodeCoverage]
public partial class CommonException : BaseAppException
{

    public CommonException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        ExceptionCategory category = ExceptionCategory.General, ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.General, eventCode, errorCode, statusCode, message, category, severity)
    {

    }

    public CommonException(string eventCode, string errorCode, HttpStatusCode statusCode, string message, Exception? innerException,
        ExceptionCategory category = ExceptionCategory.General, ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.General, eventCode, errorCode, statusCode, message, innerException, category, severity)
    {

    }

    public static CommonException InvalidOperation(string? message = null, Exception? innerException = null)
    {
        return new CommonException(
            EventCodeRegistry.GetCode(Constants.EventCodes.InvalidOperation),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.InvalidOperation),
            HttpStatusCode.BadRequest,
            message ?? "The operation is invalid in the current context.",
            innerException
        );
    }

    public static CommonException DataValidationFailed(string? message = null, Exception? innerException = null)
    {
        return new CommonException(
            EventCodeRegistry.GetCode(Constants.EventCodes.DataValidationError),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.ValidationError),
            HttpStatusCode.BadRequest,
            message ?? "The provided data did not pass validation rules.",
            innerException
        );
    }

    public static CommonException ResourceNotFound(string? message = null, Exception? innerException = null)
    {
        return new CommonException(
            EventCodeRegistry.GetCode(Constants.EventCodes.ResourceNotFound),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.NotFoundError),
            HttpStatusCode.NotFound,
            message ?? "The requested resource was not found.",
            innerException
        );
    }

    public static CommonException UnauthorizedAccess(string? message = null, Exception? innerException = null)
    {
        return new CommonException(
            EventCodeRegistry.GetCode(Constants.EventCodes.UnauthorizedAccess),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.UnauthorizedError),
            HttpStatusCode.Unauthorized,
            message ?? "Access denied due to insufficient permissions.",
            innerException
        );
    }

    public static CommonException InternalServerError(string? message = null, Exception? innerException = null)
    {
        return new CommonException(
            EventCodeRegistry.GetCode(Constants.EventCodes.InternalServerError),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.InternalServerError),
            HttpStatusCode.InternalServerError,
            message ?? "An unexpected server error occurred.",
            innerException
        );
    }

    public static CommonException DataIntegrityViolation(string? message = null, Exception? innerException = null) =>
        new(
            EventCodeRegistry.GetCode(Constants.EventCodes.DataIntegrityViolation),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.DataIntegrityViolation),
            HttpStatusCode.Conflict,
            message ?? "Data integrity violation occurred.",
            innerException
        );

    public static CommonException MissingParameter(string? message = null, Exception? innerException = null) =>
        new(
            EventCodeRegistry.GetCode(Constants.EventCodes.MissingParameter),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.MissingParameter),
            HttpStatusCode.BadRequest,
            message ?? "A required parameter is missing.",
            innerException
        );

    public static CommonException InvalidParameter(string? message = null, Exception? innerException = null) =>
        new(
            EventCodeRegistry.GetCode(Constants.EventCodes.InvalidParameter),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.InvalidParameter),
            HttpStatusCode.BadRequest,
            message ?? "One or more parameters are invalid.",
            innerException
        );

    public static CommonException SerializationError(string? message = null, Exception? innerException = null) =>
        new(
            EventCodeRegistry.GetCode(Constants.EventCodes.SerializationError),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.SerializationError),
            HttpStatusCode.InternalServerError,
            message ?? "Error serializing data.",
            innerException
        );

    public static CommonException DeserializationError(string? message = null, Exception? innerException = null) =>
        new(
            EventCodeRegistry.GetCode(Constants.EventCodes.DeserializationError),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.DeserializationError),
            HttpStatusCode.InternalServerError,
            message ?? "Error deserializing data.",
            innerException
        );

    public static CommonException InvalidRequest(string? message = null, Exception? innerException = null) =>
        new(
            EventCodeRegistry.GetCode(Constants.EventCodes.InvalidRequest),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.InvalidRequest),
            HttpStatusCode.BadRequest,
            message ?? "The request is invalid.",
            innerException
        );

    public static CommonException OperationFailed(string? message = null, Exception? innerException = null) =>
        new(
            EventCodeRegistry.GetCode(Constants.EventCodes.OperationFailed),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.OperationFailed),
            HttpStatusCode.InternalServerError,
            message ?? "The operation failed to complete.",
            innerException
        );

    public static CommonException Forbidden(string? message = null, Exception? innerException = null) =>
        new(
            EventCodeRegistry.GetCode(Constants.EventCodes.Forbidden),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.ForbiddenError),
            HttpStatusCode.Forbidden,
            message ?? "You do not have permission to perform this action.",
            innerException
        );

    public static CommonException NotImplemented(string? message = null, Exception? innerException = null) =>
        new(
            EventCodeRegistry.GetCode(Constants.EventCodes.NotImplemented),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.NotImplemented),
            HttpStatusCode.NotImplemented,
            message ?? "This feature is not implemented.",
            innerException
        );
}