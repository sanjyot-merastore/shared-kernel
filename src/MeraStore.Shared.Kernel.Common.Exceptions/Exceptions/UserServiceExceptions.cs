using System.Net;

namespace MeraStore.Shared.Kernel.Common.Exceptions.Exceptions;

public partial class UserServiceExceptions
{
    public class UserNotFoundException(string message) : BaseAppException(
      ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.UserService),
      EventCodeProvider.GetEventCode(Constants.EventCodes.ResourceNotFound),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.NotFoundError),
      HttpStatusCode.NotFound, message);

    public class UserCreationException(string message) : BaseAppException(
      ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.UserService),
      EventCodeProvider.GetEventCode(Constants.EventCodes.InvalidRequest),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.InvalidFieldError),
      HttpStatusCode.BadRequest, message);

    public class UserUpdateException(string message) : BaseAppException(
      ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.UserService),
      EventCodeProvider.GetEventCode(Constants.EventCodes.InvalidRequest),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.InvalidFieldError),
      HttpStatusCode.BadRequest, message);

    public class UserAlreadyExistsException(string message) : BaseAppException(
      ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.UserService),
      EventCodeProvider.GetEventCode(Constants.EventCodes.ResourceConflict),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.ConflictError),
      HttpStatusCode.BadRequest, message);
}