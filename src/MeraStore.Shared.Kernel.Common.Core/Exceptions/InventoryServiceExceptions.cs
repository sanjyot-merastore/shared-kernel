using System.Net;

namespace MeraStore.Shared.Kernel.Common.Core.Exceptions;

public class InventoryServiceExceptions
{
    public class OutOfStockException(string message) : BaseAppException(
      ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.InventoryService),
      EventCodeProvider.GetEventCode(Constants.EventCodes.ResourceNotFound),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.NotFoundError),
      HttpStatusCode.NotFound, message);

    public class StockAdjustmentException(string message) : BaseAppException(
      ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.InventoryService),
      EventCodeProvider.GetEventCode(Constants.EventCodes.ServiceError),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.UnprocessableEntityError),
      HttpStatusCode.UnprocessableEntity, message);
}