namespace MeraStore.Shared.Kernel.Common.Exceptions;

public static class Constants
{
  public static class ServiceIdentifiers
  {
    public static readonly string General = "general";
    public static readonly string ApiGateway = "api-gateway";
    public static readonly string AuthService = "auth-service";
    public static readonly string CartService = "cart-service";
    public static readonly string Configuration = "configuration";
    public static readonly string Database = "database";
    public static readonly string DataValidation = "data-validation";
    public static readonly string EventBus = "event-bus";
    public static readonly string InventoryService = "inventory-service";
    public static readonly string Network = "network";
    public static readonly string Operational = "operational";
    public static readonly string OrderService = "order-service";
    public static readonly string ProductService = "product-service";
    public static readonly string PaymentService = "payment-service";
    public static readonly string ShippingService = "shipping-service";
    public static readonly string Security = "security";
    public static readonly string UserService = "user-service";
    public static readonly string NotificationService = "notification-service";
    public static readonly string SearchService = "search-service";
    public static readonly string ReviewService = "review-service";
    public static readonly string WarehouseService = "warehouse-service";
    public static readonly string AnalyticsService = "analytics-service";
    public static readonly string DiscountService = "discount-service";
    public static readonly string FraudDetectionService = "fraud-detection-service";
    public static readonly string LoggingService = "logging-service";
    public static readonly string Tracing = "tracing";
    public static readonly string Monitoring = "monitoring";
    public static readonly string Secrets = "secrets";
    public static readonly string Caching = "caching";
    public static readonly string MultiTenancy = "multi-tenancy";
    public static readonly string Masking = "masking";
    public static readonly string HttpClient = "http-client";
    public static readonly string Scheduler = "scheduler";
    public static readonly string WebhookProcessor = "webhook-processor";
    public static readonly string OpenSearch = "open-search";
    public static readonly string FileStorage = "file-storage";
    public static readonly string Kubernetes = "kubernetes";
    public static readonly string CI_CD = "ci-cd";
    public static readonly string Terraform = "terraform";
    public static readonly string Vault = "vault";
    public static readonly string Istio = "istio";


  }



  public static class EventCodes
  {
    public const string ApiKeyMissing = "ApiKeyMissing";
    public const string DataIntegrityViolation = "DataIntegrityViolation";
    public const string DataValidationError = "DataValidationError";
    public const string FeatureNotSupported = "FeatureNotSupported";
    public const string Forbidden = "Forbidden";
    public const string InternalServerError = "InternalServerError";
    public const string InvalidParameter = "InvalidParameter";
    public const string InvalidRequest = "InvalidRequest";
    public const string MissingParameter = "MissingParameter";
    public const string NetworkError = "NetworkError";
    public const string NotImplemented = "NotImplemented";
    public const string RateLimitExceeded = "RateLimitExceeded";
    public const string RequestTimeout = "RequestTimeout";
    public const string ResourceConflict = "ResourceConflict";
    public const string ResourceNotFound = "ResourceNotFound";
    public const string ServiceError = "ServiceError";
    public const string ServiceUnavailable = "ServiceUnavailable";
    public const string Timeout = "Timeout";
    public const string TokenExpired = "TokenExpired";
    public const string UnauthorizedAccess = "UnauthorizedAccess";
    public const string UnsupportedMediaType = "UnsupportedMediaType";
    public const string DeserializationError = "DeserializationError";
    public const string InvalidOperation = "InvalidOperation";
    public const string OperationFailed = "OperationFailed";
    public const string SerializationError = "SerializationError";
    public const string HttpRequestError = "HttpRequestError";

    // 🔽 Newly added
    public const string MissingConfiguration = "MissingConfiguration";
    public const string InvalidConfiguration = "InvalidConfiguration";
    public const string EnvironmentMismatch = "EnvironmentMismatch";
    public const string RetryableError = "RetryableError";
    public const string HttpError = "HttpError";
    public const string UnsupportedOperation = "UnsupportedOperation";
    public const string ConcurrencyConflict = "ConcurrencyConflict";
    public const string DataConversionError = "DataConversionError";
    public const string FileOperationError = "FileOperationError";
  }

  public static class ErrorCodes
  {
    public const string BadGatewayError = "BadGatewayError";
    public const string BadRequestError = "BadRequestError";
    public const string ConflictError = "ConflictError";
    public const string DataNotFoundError = "DataNotFoundError";
    public const string ForbiddenError = "ForbiddenError";
    public const string InputError = "InputError";
    public const string InternalServerError = "InternalServerError";
    public const string InvalidFieldError = "InvalidFieldError";
    public const string InvalidFormatError = "InvalidFormatError";
    public const string MissingFieldError = "MissingFieldError";
    public const string NotFoundError = "NotFoundError";
    public const string NotImplementedError = "NotImplementedError";
    public const string RequestError = "RequestError";
    public const string ServiceUnavailableError = "ServiceUnavailableError";
    public const string TooManyRequestsError = "TooManyRequestsError";
    public const string UnauthorizedError = "UnauthorizedError";
    public const string UnprocessableEntityError = "UnprocessableEntityError";
    public const string ValidationError = "ValidationError";
    public const string HttpRequestError = "HttpRequestError";
    public const string MissingUriError = "MissingUriError";
    public const string MissingHttpMethodError = "MissingHttpMethodError";

    // 🔽 Newly added
    public const string MissingConfigurationError = "MissingConfigurationError";
    public const string InvalidConfigurationError = "InvalidConfigurationError";
    public const string ConfigurationConflictError = "ConfigurationConflictError";
    public const string ExternalDependencyError = "ExternalDependencyError";
    public const string TransientError = "TransientError";
    public const string TimeoutError = "TimeoutError";
    public const string HttpRequestFailed = "HttpRequestFailed";
    public const string InvalidCriteriaError = "InvalidCriteriaError";
    public const string InvalidOperationError = "InvalidOperationError";
    public const string IOError = "IOError";
  }


}