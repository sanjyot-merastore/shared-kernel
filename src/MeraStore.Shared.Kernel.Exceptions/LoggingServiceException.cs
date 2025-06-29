using MeraStore.Shared.Kernel.Exceptions.Codes.Services;

namespace MeraStore.Shared.Kernel.Exceptions;

[ExcludeFromCodeCoverage]
public partial class LoggingServiceException : BaseAppException
{
    public LoggingServiceException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        ExceptionCategory category = ExceptionCategory.CrossCutting,
        ExceptionSeverity severity = ExceptionSeverity.Minor)
        : base(ServiceCodeRegistry.GetCode(Constants.ServiceIdentifiers.LoggingService), eventCode, errorCode, statusCode, message, category,
            severity)
    {
    }

    public LoggingServiceException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        Exception? innerException,
        ExceptionCategory category = ExceptionCategory.CrossCutting,
        ExceptionSeverity severity = ExceptionSeverity.Minor)
        : base(ServiceCodeRegistry.GetCode(Constants.ServiceIdentifiers.LoggingService), eventCode, errorCode, statusCode, message,
            innerException, category, severity)
    {
    }

    public static LoggingServiceException LogInternalServerError(string? message = null, Exception? innerException = null)
    {
        return new LoggingServiceException(
            EventCodeRegistry.GetCode(Constants.EventCodes.InternalServerError),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.InternalServerError),
            HttpStatusCode.InternalServerError,
            message ?? "Internal server error in logging service.",
            innerException
        );
    }

    public static LoggingServiceException LogConfigurationMissing(string? message = null, Exception? innerException = null)
    {
        return new LoggingServiceException(
            EventCodeRegistry.GetCode(Constants.EventCodes.MissingConfiguration),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.MissingConfiguration),
            HttpStatusCode.InternalServerError,
            message ?? "Logging service configuration is missing.",
            innerException
        );
    }

    public static LoggingServiceException LogConnectionLost(string? message = null, Exception? innerException = null)
    {
        return new LoggingServiceException(
            EventCodeRegistry.GetCode(Constants.EventCodes.CacheConnectionLost),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.CacheConnectionLost),
            HttpStatusCode.ServiceUnavailable,
            message ?? "Logging service cache connection lost.",
            innerException
        );
    }

    public static LoggingServiceException LogSerializationError(string? message = null, Exception? innerException = null)
    {
        return new LoggingServiceException(
            EventCodeRegistry.GetCode(Constants.EventCodes.SerializationError),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.SerializationError),
            HttpStatusCode.InternalServerError,
            message ?? "Serialization error in logging service.",
            innerException
        );
    }

    public static LoggingServiceException LogDeserializationError(string? message = null, Exception? innerException = null)
    {
        return new LoggingServiceException(
            EventCodeRegistry.GetCode(Constants.EventCodes.DeserializationError),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.DeserializationError),
            HttpStatusCode.BadRequest,
            message ?? "Deserialization error in logging service.",
            innerException
        );
    }
    public static LoggingServiceException SinkWriteFailed(string? message = null, Exception? innerException = null)
    {
        return new LoggingServiceException(
            EventCodeRegistry.GetCode(Constants.EventCodes.FileWriteFailed),  // Using FileWriteFailed as proxy for sink write failures
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.FileWriteFailed),
            HttpStatusCode.ServiceUnavailable,
            message ?? "Failed to write logs to the sink.",
            innerException
        );
    }

    public static LoggingServiceException SinkRetryExhausted(string? message = null, Exception? innerException = null)
    {
        return new LoggingServiceException(
            EventCodeRegistry.GetCode(Constants.EventCodes.HttpClientRetryExhausted),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.HttpClientRetryExhausted),
            HttpStatusCode.ServiceUnavailable,
            message ?? "Logging sink retry attempts exhausted.",
            innerException
        );
    }
}