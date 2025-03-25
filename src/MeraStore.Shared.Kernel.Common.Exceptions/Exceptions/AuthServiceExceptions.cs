using System.Net;

namespace MeraStore.Shared.Kernel.Common.Exceptions.Exceptions;

public partial class AuthServiceExceptions
{
    public class AuthenticationException(string message) : BaseAppException(
      ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.AuthService),
      EventCodeProvider.GetEventCode(Constants.EventCodes.UnauthorizedAccess),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.UnauthorizedError),
      HttpStatusCode.Unauthorized, message);

    public class AuthorizationException(string message) : BaseAppException(
      ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.AuthService),
      EventCodeProvider.GetEventCode(Constants.EventCodes.Forbidden),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.ForbiddenError),
      HttpStatusCode.Forbidden, message);
}