namespace MeraStore.Shared.Kernel.Common.Core;

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
    // **MS-Specific Headers for Tracing & Observability**
    public static readonly string CorrelationId = "ms-correlationid";
    public static readonly string TraceId = "ms-traceid";
    public static readonly string SpanId = "ms-spanid";
    public static readonly string ParentSpanId = "ms-parent-spanid";
    public static readonly string RequestStart = "ms-requeststart";
    public static readonly string ServiceName = "ms-servicename";

    // **MS-Specific Headers for Security & Identity**
    public static readonly string UserId = "ms-userid";
    public static readonly string ClientId = "ms-clientid";
    public static readonly string SessionId = "ms-sessionid";
    public static readonly string TenantId = "ms-tenantid";
    public static readonly string Roles = "ms-roles";
    public static readonly string Scope = "ms-scope";
    public static readonly string AuthMethod = "ms-auth-method";
    public static readonly string DeviceId = "ms-deviceid";
    public static readonly string AppVersion = "ms-appversion";

    // **MS-Specific Headers for Performance & Debugging**
    public static readonly string Region = "ms-region";
    public static readonly string Environment = "ms-environment";
    public static readonly string ApiVersion = "ms-apiversion";
    public static readonly string FeatureFlag = "ms-feature-flag";
    public static readonly string DebugMode = "ms-debug-mode";

    // **Standard Request Headers**
    public static readonly string UserAgent = "user-agent";
    public static readonly string Authorization = "authorization";
    public static readonly string ContentType = "content-type";
    public static readonly string Accept = "accept";
    public static readonly string AcceptEncoding = "accept-encoding";
    public static readonly string AcceptLanguage = "accept-language";
    public static readonly string Host = "host";
    public static readonly string Referer = "referer";
    public static readonly string Origin = "origin";
    public static readonly string Connection = "connection";
    public static readonly string CacheControl = "cache-control";
    public static readonly string Pragma = "pragma";
    public static readonly string XForwardedFor = "x-forwarded-for";
    public static readonly string XForwardedProto = "x-forwarded-proto";
    public static readonly string XForwardedPort = "x-forwarded-port";
    public static readonly string XRequestId = "x-request-id";
    public static readonly string XRealIp = "x-real-ip";
    public static readonly string XApiKey = "x-api-key";
    public static readonly string IfModifiedSince = "if-modified-since";
    public static readonly string IfNoneMatch = "if-none-match";
    public static readonly string Range = "range";

    // **Standard Response Headers**
    public static readonly string ResponseCorrelationId = "ms-correlationid";
    public static readonly string ResponseRequestId = "ms-requestid";
    public static readonly string ResponseContentType = "content-type";
    public static readonly string ResponseContentLength = "content-length";
    public static readonly string ResponseServer = "server";
    public static readonly string ResponseConnection = "connection";
    public static readonly string ResponseCacheControl = "cache-control";
    public static readonly string ResponsePragma = "pragma";
    public static readonly string ResponseSetCookie = "set-cookie";
    public static readonly string ResponseExpires = "expires";
    public static readonly string ResponseLastModified = "last-modified";
    public static readonly string ResponseETag = "etag";
    public static readonly string ResponseXPoweredBy = "x-powered-by";
    public static readonly string ResponseAccessControlAllowOrigin = "access-control-allow-origin";
    public static readonly string ResponseAccessControlAllowCredentials = "access-control-allow-credentials";
    public static readonly string ResponseAccessControlAllowMethods = "access-control-allow-methods";
    public static readonly string ResponseAccessControlAllowHeaders = "access-control-allow-headers";
    public static readonly string ResponseXFrameOptions = "x-frame-options";
    public static readonly string ResponseXXSSProtection = "x-xss-protection";
    public static readonly string ResponseXContentTypeOptions = "x-content-type-options";
    public static readonly string ResponseStrictTransportSecurity = "strict-transport-security";
    public static readonly string ResponseReferrerPolicy = "referrer-policy";
    public static readonly string ResponseFeaturePolicy = "feature-policy";
    public static readonly string ResponseContentSecurityPolicy = "content-security-policy";
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