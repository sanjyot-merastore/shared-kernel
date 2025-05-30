namespace MeraStore.Shared.Kernel.Exceptions;

[ExcludeFromCodeCoverage]
public partial class ProductServiceException : BaseAppException
{
    public ProductServiceException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.ProductService, eventCode, errorCode, statusCode, message, category,
            severity)
    {
    }

    public ProductServiceException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        Exception? innerException,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.ProductService, eventCode, errorCode, statusCode, message,
            innerException, category, severity)
    {
    }
}
[ExcludeFromCodeCoverage]
public partial class UserServiceException : BaseAppException
{
    public UserServiceException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.UserService, eventCode, errorCode, statusCode, message, category,
            severity)
    {
    }

    public UserServiceException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        Exception? innerException,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.UserService, eventCode, errorCode, statusCode, message, innerException,
            category, severity)
    {
    }
}

[ExcludeFromCodeCoverage]
public partial class InventoryServiceException : BaseAppException
{
    public InventoryServiceException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.InventoryService, eventCode, errorCode, statusCode, message, category,
            severity)
    {
    }

    public InventoryServiceException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        Exception? innerException,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.InventoryService, eventCode, errorCode, statusCode, message,
            innerException, category, severity)
    {
    }
}

[ExcludeFromCodeCoverage]
public partial class CartServiceException : BaseAppException
{
    public CartServiceException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.CartService, eventCode, errorCode, statusCode, message, category,
            severity)
    {
    }

    public CartServiceException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        Exception? innerException,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.CartService, eventCode, errorCode, statusCode, message, innerException,
            category, severity)
    {
    }
}

[ExcludeFromCodeCoverage]
public partial class OrderServiceException : BaseAppException
{
    public OrderServiceException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.OrderService, eventCode, errorCode, statusCode, message, category,
            severity)
    {
    }

    public OrderServiceException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        Exception? innerException,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.OrderService, eventCode, errorCode, statusCode, message, innerException,
            category, severity)
    {
    }
}

[ExcludeFromCodeCoverage]
public partial class AuthServiceException : BaseAppException
{
    public AuthServiceException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.AuthService, eventCode, errorCode, statusCode, message, category,
            severity)
    {
    }

    public AuthServiceException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        Exception? innerException,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.AuthService, eventCode, errorCode, statusCode, message, innerException,
            category, severity)
    {
    }
}

[ExcludeFromCodeCoverage]
public partial class EventBusException : BaseAppException
{
    public EventBusException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.EventBus, eventCode, errorCode, statusCode, message, category, severity)
    {
    }

    public EventBusException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        Exception? innerException,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.EventBus, eventCode, errorCode, statusCode, message, innerException,
            category, severity)
    {
    }
}

[ExcludeFromCodeCoverage]
public partial class ConfigurationException : BaseAppException
{
    public ConfigurationException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.Configuration, eventCode, errorCode, statusCode, message, category,
            severity)
    {
    }

    public ConfigurationException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        Exception? innerException,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.Configuration, eventCode, errorCode, statusCode, message,
            innerException, category, severity)
    {
    }
}
[ExcludeFromCodeCoverage]

public partial class DatabaseException : BaseAppException
{
    public DatabaseException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.Database, eventCode, errorCode, statusCode, message, category, severity)
    {
    }

    public DatabaseException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        Exception? innerException,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.Database, eventCode, errorCode, statusCode, message, innerException,
            category, severity)
    {
    }
}
[ExcludeFromCodeCoverage]
public partial class SecurityException : BaseAppException
{
    public SecurityException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.Security, eventCode, errorCode, statusCode, message, category, severity)
    {
    }

    public SecurityException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        Exception? innerException,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.Security, eventCode, errorCode, statusCode, message, innerException,
            category, severity)
    {
    }
}
[ExcludeFromCodeCoverage]
public partial class GeneralException : BaseAppException
{
    public GeneralException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.General, eventCode, errorCode, statusCode, message, category, severity)
    {
    }

    public GeneralException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        Exception? innerException,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.General, eventCode, errorCode, statusCode, message, innerException,
            category, severity)
    {
    }
}
[ExcludeFromCodeCoverage]
public partial class PaymentServiceException : BaseAppException
{
    public PaymentServiceException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.PaymentService, eventCode, errorCode, statusCode, message, category,
            severity)
    {
    }

    public PaymentServiceException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        Exception? innerException,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.PaymentService, eventCode, errorCode, statusCode, message,
            innerException, category, severity)
    {
    }
}

[ExcludeFromCodeCoverage]
public partial class ShippingServiceException : BaseAppException
{
    public ShippingServiceException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.ShippingService, eventCode, errorCode, statusCode, message, category,
            severity)
    {
    }

    public ShippingServiceException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        Exception? innerException,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.ShippingService, eventCode, errorCode, statusCode, message,
            innerException, category, severity)
    {
    }
}

[ExcludeFromCodeCoverage]
public partial class NotificationServiceException : BaseAppException
{
    public NotificationServiceException(string eventCode, string errorCode, HttpStatusCode statusCode,
        string message,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.NotificationService, eventCode, errorCode, statusCode, message,
            category, severity)
    {
    }

    public NotificationServiceException(string eventCode, string errorCode, HttpStatusCode statusCode,
        string message, Exception? innerException,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.NotificationService, eventCode, errorCode, statusCode, message,
            innerException, category, severity)
    {
    }
}

[ExcludeFromCodeCoverage]
public partial class SearchServiceException : BaseAppException
{
    public SearchServiceException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.SearchService, eventCode, errorCode, statusCode, message, category,
            severity)
    {
    }

    public SearchServiceException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        Exception? innerException,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.SearchService, eventCode, errorCode, statusCode, message,
            innerException, category, severity)
    {
    }
}

[ExcludeFromCodeCoverage]
public partial class ReviewServiceException : BaseAppException
{
    public ReviewServiceException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.ReviewService, eventCode, errorCode, statusCode, message, category,
            severity)
    {
    }

    public ReviewServiceException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        Exception? innerException,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.ReviewService, eventCode, errorCode, statusCode, message,
            innerException, category, severity)
    {
    }
}

[ExcludeFromCodeCoverage]
public partial class WarehouseServiceException : BaseAppException
{
    public WarehouseServiceException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.WarehouseService, eventCode, errorCode, statusCode, message, category,
            severity)
    {
    }

    public WarehouseServiceException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        Exception? innerException,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.WarehouseService, eventCode, errorCode, statusCode, message,
            innerException, category, severity)
    {
    }
}

[ExcludeFromCodeCoverage]
public partial class AnalyticsServiceException : BaseAppException
{
    public AnalyticsServiceException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.AnalyticsService, eventCode, errorCode, statusCode, message, category,
            severity)
    {
    }

    public AnalyticsServiceException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        Exception? innerException,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.AnalyticsService, eventCode, errorCode, statusCode, message,
            innerException, category, severity)
    {
    }
}
[ExcludeFromCodeCoverage]
public partial class DiscountServiceException : BaseAppException
{
    public DiscountServiceException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.DiscountService, eventCode, errorCode, statusCode, message, category,
            severity)
    {
    }

    public DiscountServiceException(string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        Exception? innerException,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.DiscountService, eventCode, errorCode, statusCode, message,
            innerException, category, severity)
    {
    }
}

[ExcludeFromCodeCoverage]
public partial class FraudDetectionServiceException : BaseAppException
{
    public FraudDetectionServiceException(string eventCode, string errorCode, HttpStatusCode statusCode,
        string message,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.FraudDetectionService, eventCode, errorCode, statusCode, message,
            category, severity)
    {
    }

    public FraudDetectionServiceException(string eventCode, string errorCode, HttpStatusCode statusCode,
        string message, Exception? innerException,
        ExceptionCategory category = ExceptionCategory.General,
        ExceptionSeverity severity = ExceptionSeverity.Major)
        : base(Constants.ServiceIdentifiers.FraudDetectionService, eventCode, errorCode, statusCode, message,
            innerException, category, severity)
    {
    }
}