﻿using System.Net;

namespace MeraStore.Shared.Kernel.Common.Exceptions.Exceptions;

public partial class OrderServiceExceptions
{
    public class OrderNotFoundException(string message) : BaseAppException(
      ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.OrderService),
      EventCodeProvider.GetEventCode(Constants.EventCodes.ResourceNotFound),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.NotFoundError),
      HttpStatusCode.NotFound, message);

    //public class PaymentProcessingException(string message) : BaseAppException(
    //  ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.PaymentService),
    //  EventCodeProvider.GetEventCode(Constants.EventCodes.PaymentFailed),
    //  ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.PaymentError),
    //  HttpStatusCode.PaymentRequired, message);

    public class OrderValidationException(string message) : BaseAppException(
      ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.OrderService),
      EventCodeProvider.GetEventCode(Constants.EventCodes.DataValidationError),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.ValidationError),
      HttpStatusCode.UnprocessableEntity, message);
}