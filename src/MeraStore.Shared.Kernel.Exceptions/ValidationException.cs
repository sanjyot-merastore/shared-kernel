namespace MeraStore.Shared.Kernel.Exceptions;

[ExcludeFromCodeCoverage]
public partial class ValidationException : BaseAppException
{
    public ValidationException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        ExceptionCategory category = ExceptionCategory.Validation,
        ExceptionSeverity severity = ExceptionSeverity.Minor) : base(Constants.ServiceIdentifiers.DataValidation,
        eventCode, errorCode, statusCode, message, category,
        severity)
    {
    }

    public ValidationException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        Exception? innerException,
        ExceptionCategory category = ExceptionCategory.Validation, ExceptionSeverity severity = ExceptionSeverity.Minor)
        : base(Constants.ServiceIdentifiers.DataValidation, eventCode, errorCode, statusCode, message, innerException,
            category, severity)
    {
    }

    public static ValidationException DataValidationError(string? message = null, Exception? innerException = null)
    {
        return new ValidationException(
            EventCodeRegistry.GetCode(Constants.EventCodes.DataValidationError),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.DataValidationError),
            HttpStatusCode.BadRequest,
            message ?? "Data validation failed.",
            innerException);
    }

    public static ValidationException MissingRequiredField(string fieldName, string? message = null, Exception? innerException = null)
    {
        return new ValidationException(
            EventCodeRegistry.GetCode(Constants.EventCodes.MissingParameter),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.MissingParameter),
            HttpStatusCode.BadRequest,
            message ?? $"The required field '{fieldName}' is missing.",
            innerException);
    }

    public static ValidationException InvalidFormat(string fieldName, string? message = null, Exception? innerException = null)
    {
        return new ValidationException(
            EventCodeRegistry.GetCode(Constants.EventCodes.InvalidParameter),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.InvalidParameter),
            HttpStatusCode.BadRequest,
            message ?? $"The field '{fieldName}' has an invalid format.",
            innerException);
    }

    public static ValidationException OutOfRange(string fieldName, string? message = null, Exception? innerException = null)
    {
        return new ValidationException(
            EventCodeRegistry.GetCode(Constants.EventCodes.InvalidParameter),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.InvalidParameter),
            HttpStatusCode.BadRequest,
            message ?? $"The field '{fieldName}' is out of the allowed range.",
            innerException);
    }

    
    public static ValidationException MissingParameter(string? message = null, Exception? innerException = null) =>
        new ValidationException(
            EventCodeRegistry.GetCode(Constants.EventCodes.MissingParameter),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.MissingParameter),
            HttpStatusCode.BadRequest,
            message ?? "A required parameter is missing.",
            innerException
        );

    public static ValidationException InvalidParameter(string? message = null, Exception? innerException = null) =>
        new ValidationException(
            EventCodeRegistry.GetCode(Constants.EventCodes.InvalidParameter),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.InvalidParameter),
            HttpStatusCode.BadRequest,
            message ?? "A parameter is invalid.",
            innerException
        );

    public static ValidationException InvalidRequest(string? message = null, Exception? innerException = null) =>
        new ValidationException(
            EventCodeRegistry.GetCode(Constants.EventCodes.InvalidRequest),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.InvalidRequest),
            HttpStatusCode.BadRequest,
            message ?? "The request is invalid.",
            innerException
        );

    public static ValidationException InvalidOperation(string? message = null, Exception? innerException = null) =>
        new ValidationException(
            EventCodeRegistry.GetCode(Constants.EventCodes.InvalidOperation),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.InvalidOperation),
            HttpStatusCode.BadRequest,
            message ?? "The operation is invalid in the current context.",
            innerException
        );

    public static ValidationException UnsupportedMediaType(string? message = null, Exception? innerException = null) =>
        new ValidationException(
            EventCodeRegistry.GetCode(Constants.EventCodes.UnsupportedMediaType),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.UnsupportedMediaType),
            HttpStatusCode.UnsupportedMediaType,
            message ?? "Unsupported media type.",
            innerException
        );

    public static ValidationException DataConversionError(string? message = null, Exception? innerException = null) =>
        new ValidationException(
            EventCodeRegistry.GetCode(Constants.EventCodes.DataConversionError),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.DataConversionError),
            HttpStatusCode.BadRequest,
            message ?? "Data conversion error during validation.",
            innerException
        );

    public static ValidationException DeserializationError(string? message = null, Exception? innerException = null) =>
        new ValidationException(
            EventCodeRegistry.GetCode(Constants.EventCodes.DeserializationError),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.DeserializationError),
            HttpStatusCode.BadRequest,
            message ?? "Deserialization error during validation.",
            innerException
        );

}


