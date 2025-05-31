using System.Net;
using MeraStore.Shared.Kernel.Exceptions;
using MeraStore.Shared.Kernel.Exceptions.Codes.Events;
using MeraStore.Shared.Kernel.Exceptions.Codes.Http;

namespace MeraStore.Shared.Kernel.Http.Exceptions;

public partial class ApiExceptions
{
    public static ApiException HttpMethodMissing(string? message = null, Exception? innerException = null)
    {
        return new ApiException(EventCodeRegistry.GetCode("HttpMethodMissing"), ErrorCodeRegistry.GetCode("HttpMethodMissing"), HttpStatusCode.BadRequest,
            message ?? "The HTTP method used is not specified or is invalid.", innerException);
    }

    public static ApiException UrlMissing(string? message = null, Exception? innerException = null)
    {
        return new ApiException(EventCodeRegistry.GetCode("UrlMissing"), ErrorCodeRegistry.GetCode("UrlMissing"), HttpStatusCode.BadRequest,
            message ?? "The request URL is missing or empty.", innerException);
    }
}