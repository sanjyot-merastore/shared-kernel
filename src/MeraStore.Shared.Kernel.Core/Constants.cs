namespace MeraStore.Shared.Kernel.Core;

public static class Constants
{
  public static readonly string ApplicationName = "ApplicationName";

  public static class Services
  {
    public static readonly string User = "merastore-user-service";
    public static readonly string Product = "merastore-product-service";
    public static readonly string Inventory = "merastore-inventory-service";
    public static readonly string Order = "merastore-order-service";
    public static readonly string Cart = "merastore-cart-service";
    public static readonly string Logging = "merastore-logging-service";
    public static readonly string Payment = "merastore-payment-service";
    public static readonly string Shipping = "merastore-shipping-service";
    public static readonly string Auth = "merastore-auth-service";
    public static readonly string Notification = "merastore-notification-service";
    public static readonly string Search = "merastore-search-service";
    public static readonly string Review = "merastore-review-service";
    public static readonly string Warehouse = "merastore-warehouse-service";
    public static readonly string Analytics = "merastore-analytics-service";
    public static readonly string Discount = "merastore-discount-service";
    public static readonly string FraudDetection = "merastore-fraud-detection-service";
  }

  public static class SharedLibraries
  {
    public static readonly string SharedKernel = "library-shared-kernel";
    public static readonly string Crypto = "library-crypto";
    public static readonly string Serialization = "library-serialization";
    public static readonly string Logging = "library-logging";
    public static readonly string Caching = "library-caching";
    public static readonly string Configuration = "library-configuration";
    public static readonly string Validation = "library-validation";
    public static readonly string ErrorHandling = "library-error-handling";
    public static readonly string RateLimiting = "library-rate-limiting";
    public static readonly string HealthCheck = "library-health-check";
    public static readonly string Authorization = "library-authorization";
    public static readonly string Telemetry = "library-telemetry";
    public static readonly string I18n = "library-i18n";
  }

  public static class Headers
  {
    // Tracing & Observability
    public const string CorrelationId = "ms-correlationid";
    public const string RequestId = "ms-request-id";
    public const string TransactionId = "ms-txn-id";
    public const string TraceId = "ms-traceid";
    public const string SpanId = "ms-spanid";
    public const string ParentSpanId = "ms-parent-spanid";
    public const string RequestStart = "ms-requeststart";
    public const string ServiceName = "ms-servicename";

    // Security & Identity
    public const string UserId = "ms-userid";
    public const string ClientId = "ms-clientid";
    public const string SessionId = "ms-sessionid";
    public const string TenantId = "ms-tenantid";
    public const string Roles = "ms-roles";
    public const string Scope = "ms-scope";
    public const string AuthMethod = "ms-auth-method";
    public const string DeviceId = "ms-deviceid";
    public const string AppVersion = "ms-appversion";
    public const string ApiKey = "ms-api-key";
    public const string UserToken = "ms-user-token";

    // Performance & Debugging
    public const string Region = "ms-region";
    public const string Environment = "ms-environment";
    public const string ApiVersion = "ms-apiversion";
    public const string FeatureFlag = "ms-feature-flag";
    public const string DebugMode = "ms-debug-mode";

    // ---------------------- Standard Request Headers ----------------------

    public const string UserAgent = "user-agent";
    public const string Authorization = "authorization";
    public const string ContentType = "content-type";
    public const string Accept = "accept";
    public const string AcceptEncoding = "accept-encoding";
    public const string AcceptLanguage = "accept-language";
    public const string Host = "host";
    public const string Referer = "referer";
    public const string Origin = "origin";
    public const string Connection = "connection";
    public const string CacheControl = "cache-control";
    public const string Pragma = "pragma";
    public const string XForwardedFor = "x-forwarded-for";
    public const string XForwardedProto = "x-forwarded-proto";
    public const string XForwardedPort = "x-forwarded-port";
    public const string XRequestId = "x-request-id";
    public const string XRealIp = "x-real-ip";
    public const string XApiKey = "x-api-key";
    public const string IfModifiedSince = "if-modified-since";
    public const string IfNoneMatch = "if-none-match";
    public const string Range = "range";

    public const string SetCookie = "set-cookie";
    public const string Expires = "expires";
    public const string LastModified = "last-modified";
    public const string ETag = "etag";
    public const string XPoweredBy = "x-powered-by";
    public const string AccessControlAllowOrigin = "access-control-allow-origin";
    public const string AccessControlAllowCredentials = "access-control-allow-credentials";
    public const string AccessControlAllowMethods = "access-control-allow-methods";
    public const string AccessControlAllowHeaders = "access-control-allow-headers";
    public const string XFrameOptions = "x-frame-options";
    public const string XXSSProtection = "x-xss-protection";
    public const string XContentTypeOptions = "x-content-type-options";
    public const string StrictTransportSecurity = "strict-transport-security";
    public const string ReferrerPolicy = "referrer-policy";
    public const string FeaturePolicy = "feature-policy";
    public const string ContentSecurityPolicy = "content-security-policy";
  }



  public static class Infrastructure
  {
    public static readonly string Config = "merastore-infra-config";
    public static readonly string DockerImages = "merastore-docker-images";
    public static readonly string CiCdPipelines = "merastore-ci-cd-pipelines";
  }

  public static class Frontend
  {
    public static readonly string Web = "merastore-frontend";
    public static readonly string Mobile = "merastore-mobile-app";
  }

  public static class Additional
  {
    public static readonly string Docs = "merastore-docs";
    public static readonly string Monitoring = "merastore-monitoring";
    public static readonly string SecretsManagement = "merastore-secrets-management";
    public static readonly string Testing = "merastore-testing";
    public static readonly string Analytics = "merastore-analytics";
    public static readonly string FeatureFlags = "merastore-feature-flags";
  }

  public static partial class ServiceIdentifiers
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
  }



  public static partial class EventCodes
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
  }

}