using MeraStore.Shared.Kernel.Exceptions.Codes.Services;

namespace MeraStore.Shared.Kernel.Exceptions;

[ExcludeFromCodeCoverage]
public partial class MaskingServiceException : BaseAppException
{
    public MaskingServiceException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        ExceptionCategory category = ExceptionCategory.CrossCutting,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(ServiceCodeRegistry.GetCode(Constants.ServiceIdentifiers.Masking), eventCode, errorCode, statusCode, message, category, severity)
    {
    }

    public MaskingServiceException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        Exception? innerException,
        ExceptionCategory category = ExceptionCategory.CrossCutting,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(ServiceCodeRegistry.GetCode(Constants.ServiceIdentifiers.Masking), eventCode, errorCode, statusCode, message, innerException,
            category, severity)
    {
    }

    public static MaskingServiceException DataMaskingFailed(string? message = null, Exception? innerException = null)
    {
        return new MaskingServiceException(
            EventCodeRegistry.GetCode(Constants.EventCodes.DataMaskingFailed),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.DataMaskingFailed),
            HttpStatusCode.InternalServerError,
            message ?? "Data masking operation failed unexpectedly.",
            innerException
        );
    }

    public static MaskingServiceException MaskingFailed(string? message = null, Exception? innerException = null)
    {
        return new MaskingServiceException(
            EventCodeRegistry.GetCode(Constants.EventCodes.MaskingFailed),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.MaskingFailed),
            HttpStatusCode.InternalServerError,
            message ?? "Masking process failed.",
            innerException
        );
    }

    public static MaskingServiceException InvalidMaskingConfiguration(string? message = null, Exception? innerException = null)
    {
        return new MaskingServiceException(
            EventCodeRegistry.GetCode(Constants.EventCodes.InvalidConfiguration),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.InvalidConfiguration),
            HttpStatusCode.BadRequest,
            message ?? "Invalid masking configuration detected.",
            innerException
        );
    }

    public static MaskingServiceException MissingMaskingConfiguration(string? message = null, Exception? innerException = null)
    {
        return new MaskingServiceException(
            EventCodeRegistry.GetCode(Constants.EventCodes.MissingConfiguration),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.MissingConfiguration),
            HttpStatusCode.BadRequest,
            message ?? "Masking configuration is missing.",
            innerException
        );
    }

    public static MaskingServiceException MaskingPatternNotFound(string? message = null, Exception? innerException = null)
    {
        return new MaskingServiceException(
            EventCodeRegistry.GetCode(Constants.EventCodes.InvalidParameter),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.InvalidParameter),
            HttpStatusCode.BadRequest,
            message ?? "Masking pattern or rule not found.",
            innerException
        );
    }

    public static MaskingServiceException MaskingRuleViolation(string? message = null, Exception? innerException = null)
    {
        return new MaskingServiceException(
            EventCodeRegistry.GetCode(Constants.EventCodes.IntegrationContractViolation),
            ErrorCodeRegistry.GetCode(Constants.ErrorCodes.IntegrationContractViolation),
            HttpStatusCode.Conflict,
            message ?? "Masking rule violation occurred.",
            innerException
        );
    }
}