using System.Net;

namespace MeraStore.Shared.Kernel.Common.Exceptions.Exceptions;

public class CommonExceptions
{
  public partial class ApiCommunicationException : BaseAppException
  {
    public ApiCommunicationException(string message) : base(
        ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.ApiGateway),
        EventCodeProvider.GetEventCode(Constants.EventCodes.ServiceError),
        ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.BadGatewayError),
        HttpStatusCode.BadGateway, message)
    {
    }

    public ApiCommunicationException(string message, Exception innerException) : base(
      ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.ApiGateway),
      EventCodeProvider.GetEventCode(Constants.EventCodes.ServiceError),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.BadGatewayError),
      HttpStatusCode.BadGateway, message)
    {
    }
  }

  public class InvalidDataFormatException(string message) : BaseAppException(
      ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.DataValidation),
      EventCodeProvider.GetEventCode(Constants.EventCodes.InvalidParameter),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.InvalidFormatError),
      HttpStatusCode.BadRequest, message);

  public class EventBusCommunicationException(string message) : BaseAppException(
      ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.EventBus),
      EventCodeProvider.GetEventCode(Constants.EventCodes.ServiceError),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.ServiceUnavailableError),
      HttpStatusCode.ServiceUnavailable, message);

  public class ValidationException(string message) : BaseAppException(
      ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.DataValidation),
      EventCodeProvider.GetEventCode(Constants.EventCodes.DataValidationError),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.ValidationError),
      HttpStatusCode.UnprocessableEntity, message);

  public class BusinessRuleException(string message) : BaseAppException(
      ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.DataValidation),
      EventCodeProvider.GetEventCode(Constants.EventCodes.ResourceConflict),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.UnprocessableEntityError),
      HttpStatusCode.UnprocessableEntity, message);

  public class ConfigurationException(string message) : BaseAppException(
      ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.Configuration),
      EventCodeProvider.GetEventCode(Constants.EventCodes.ServiceError),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.InternalServerError),
      HttpStatusCode.InternalServerError, message);

  public class MissingConfigurationException(string message) : BaseAppException(
      ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.Configuration),
      EventCodeProvider.GetEventCode(Constants.EventCodes.MissingConfiguration),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.MissingConfigurationError),
      HttpStatusCode.InternalServerError, message);

  public class InvalidConfigurationException(string message) : BaseAppException(
      ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.Configuration),
      EventCodeProvider.GetEventCode(Constants.EventCodes.InvalidConfiguration),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.InvalidConfigurationError),
      HttpStatusCode.InternalServerError, message);

  public class EnvironmentMismatchException(string message) : BaseAppException(
      ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.Configuration),
      EventCodeProvider.GetEventCode(Constants.EventCodes.EnvironmentMismatch),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.ConfigurationConflictError),
      HttpStatusCode.InternalServerError, message);

  public class DatabaseConnectionException(string message) : BaseAppException(
      ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.Database),
      EventCodeProvider.GetEventCode(Constants.EventCodes.ServiceError),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.InternalServerError),
      HttpStatusCode.InternalServerError, message);

  public class DataNotFoundException(string message) : BaseAppException(
      ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.Database),
      EventCodeProvider.GetEventCode(Constants.EventCodes.ResourceNotFound),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.DataNotFoundError),
      HttpStatusCode.NotFound, message);

  public class DataIntegrityException(string message) : BaseAppException(
      ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.Database),
      EventCodeProvider.GetEventCode(Constants.EventCodes.DataIntegrityViolation),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.ConflictError),
      HttpStatusCode.Conflict, message);

  public class DuplicateDataException(string message) : BaseAppException(
      ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.Database),
      EventCodeProvider.GetEventCode(Constants.EventCodes.ResourceConflict),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.ConflictError),
      HttpStatusCode.Conflict, message);

  public class ExternalDependencyException(string message) : BaseAppException(
      ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.General),
      EventCodeProvider.GetEventCode(Constants.EventCodes.ServiceError),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.ExternalDependencyError),
      HttpStatusCode.ServiceUnavailable, message);

  public class RetryableServiceException(string message) : BaseAppException(
      ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.General),
      EventCodeProvider.GetEventCode(Constants.EventCodes.RetryableError),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.TransientError),
      HttpStatusCode.ServiceUnavailable, message);

  public class TimeoutWrapperException(string message) : BaseAppException(
      ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.General),
      EventCodeProvider.GetEventCode(Constants.EventCodes.Timeout),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.TimeoutError),
      HttpStatusCode.RequestTimeout, message);

  public class HttpRequestFailureException(string message) : BaseAppException(
      ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.General),
      EventCodeProvider.GetEventCode(Constants.EventCodes.HttpError),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.HttpRequestFailed),
      HttpStatusCode.BadGateway, message);

  public class InvalidCriteriaException(string message) : BaseAppException(
      ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.DataValidation),
      EventCodeProvider.GetEventCode(Constants.EventCodes.InvalidParameter),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.InvalidCriteriaError),
      HttpStatusCode.BadRequest, message);

  public class InvalidOperationStateException(string message) : BaseAppException(
      ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.General),
      EventCodeProvider.GetEventCode(Constants.EventCodes.InvalidOperation),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.InvalidOperationError),
      HttpStatusCode.Conflict, message);

  public class UnsupportedOperationException(string message) : BaseAppException(
      ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.General),
      EventCodeProvider.GetEventCode(Constants.EventCodes.UnsupportedOperation),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.NotImplementedError),
      HttpStatusCode.NotImplemented, message);

  public class ConcurrencyConflictException(string message) : BaseAppException(
      ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.Database),
      EventCodeProvider.GetEventCode(Constants.EventCodes.ConcurrencyConflict),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.ConflictError),
      HttpStatusCode.Conflict, message);

  public class SerializationException(string message) : BaseAppException(
      ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.General),
      EventCodeProvider.GetEventCode(Constants.EventCodes.SerializationError),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.InternalServerError),
      HttpStatusCode.BadRequest, message);

  public class DataConversionException(string message) : BaseAppException(
      ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.General),
      EventCodeProvider.GetEventCode(Constants.EventCodes.DataConversionError),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.InvalidFormatError),
      HttpStatusCode.BadRequest, message);

  public class FileOperationException(string message) : BaseAppException(
      ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.General),
      EventCodeProvider.GetEventCode(Constants.EventCodes.FileOperationError),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.IOError),
      HttpStatusCode.InternalServerError, message);
}
