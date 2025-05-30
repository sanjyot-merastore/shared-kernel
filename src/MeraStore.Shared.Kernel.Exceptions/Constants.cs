namespace MeraStore.Shared.Kernel.Exceptions;

[ExcludeFromCodeCoverage]
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

        // 🆕 Additions below this line
        public static readonly string FileService = "file-service";
        public static readonly string MediaService = "media-service";
        public static readonly string MlService = "ml-service";
        public static readonly string ExternalIntegration = "external-integration";
    }


    public static class EventCodes
    {
        // 🌐 Common
        public const string ApiError = "ApiErrors";
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
        public const string FileUploadFailed = "FileUploadFailed";
        public const string Timeout = "Timeout";
        public const string TokenExpired = "TokenExpired";
        public const string UnauthorizedAccess = "UnauthorizedAccess";
        public const string UnsupportedMediaType = "UnsupportedMediaType";
        public const string DeserializationError = "DeserializationError";
        public const string SerializationError = "SerializationError";
        public const string InvalidOperation = "InvalidOperation";
        public const string OperationFailed = "OperationFailed";

        // ⚙️ Configuration
        public const string MissingConfiguration = "MissingConfiguration";
        public const string InvalidConfiguration = "InvalidConfiguration";
        public const string EnvironmentMismatch = "EnvironmentMismatch";

        // 🔁 Retry / Concurrency
        public const string RetryableError = "RetryableError";
        public const string HttpError = "HttpError";
        public const string UnsupportedOperation = "UnsupportedOperation";
        public const string ConcurrencyConflict = "ConcurrencyConflict";

        // 💾 Data
        public const string DataConversionError = "DataConversionError";
        public const string FileOperationError = "FileOperationError";
        public const string FileReadFailed = "FileReadFailed";
        public const string FileWriteFailed = "FileWriteFailed";

        // 🌐 HTTP
        public const string HttpRequestError = "HttpRequestError";
        public const string HttpClientRetryExhausted = "HttpClientRetryExhausted";

        // 🚨 Third-Party & Queue
        public const string ThirdPartyServiceFailure = "ThirdPartyServiceFailure";
        public const string EventPublishingFailed = "EventPublishingFailed";
        public const string QueueProcessingError = "QueueProcessingError";
        public const string WebhookDeliveryFailed = "WebhookDeliveryFailed";
        public const string MessageParsingError = "MessageParsingError";
        public const string ThirdPartyTimeout = "ThirdPartyTimeout";
        public const string IntegrationContractViolation = "IntegrationContractViolation";

        // 🔐 Auth
        public const string InvalidAuthenticationToken = "InvalidAuthenticationToken";

        // 🔍 Search
        public const string SearchIndexingFailed = "SearchIndexingFailed";
        public const string SearchQueryError = "SearchQueryError";

        // 🗣️ Reviews
        public const string ReviewValidationError = "ReviewValidationError";
        public const string ReviewSubmissionFailed = "ReviewSubmissionFailed";

        // 🏬 Warehouse
        public const string WarehouseSyncError = "WarehouseSyncError";
        public const string StockMovementFailed = "StockMovementFailed";

        // 📈 Analytics
        public const string AnalyticsProcessingError = "AnalyticsProcessingError";
        public const string MetricAggregationFailed = "MetricAggregationFailed";

        // 🎁 Discounts
        public const string DiscountApplicationFailed = "DiscountApplicationFailed";
        public const string CouponInvalid = "CouponInvalid";

        // 🕵️ Fraud Detection
        public const string FraudCheckFailed = "FraudCheckFailed";
        public const string FraudAlertTriggered = "FraudAlertTriggered";

        // 🧠 ML
        public const string ModelInferenceFailed = "ModelInferenceFailed";
        public const string ModelTrainingError = "ModelTrainingError";

        // 📸 Media
        public const string MediaUploadFailed = "MediaUploadFailed";
        public const string MediaProcessingError = "MediaProcessingError";

        // 🛡️ Secrets
        public const string SecretAccessFailed = "SecretAccessFailed";
        public const string SecretRotationError = "SecretRotationError";
        public const string SecretFetchFailed = "SecretFetchFailed";

        // 🧵 Multi-Tenancy
        public const string TenantContextMissing = "TenantContextMissing";
        public const string TenantIsolationBreach = "TenantIsolationBreach";

        // 🫣 Masking
        public const string DataMaskingFailed = "DataMaskingFailed";
        public const string MaskingFailed = "MaskingFailed";

        // 🧠 Caching
        public const string CacheConnectionLost = "CacheConnectionLost";
        public const string CacheDesyncDetected = "CacheDesyncDetected";
        public const string CacheConnectionError = "CacheConnectionError";

        // 📅 Scheduler
        public const string JobSchedulingFailed = "JobSchedulingFailed";
        public const string JobExecutionError = "JobExecutionError";
        public const string SchedulerJobError = "SchedulerJobError";

        // 🔎 OpenSearch
        public const string OpenSearchUnavailable = "OpenSearchUnavailable";
        public const string OpenSearchQueryFailed = "OpenSearchQueryFailed";

        // ☸️ Kubernetes
        public const string PodCommunicationError = "PodCommunicationError";
        public const string KubeDeploymentFailed = "KubeDeploymentFailed";

        // ⚙️ CI/CD
        public const string BuildFailed = "BuildFailed";
        public const string PipelineTimeout = "PipelineTimeout";
        public const string CiCdDeploymentError = "CiCdDeploymentError";

        // 🌍 Terraform
        public const string InfraProvisioningError = "InfraProvisioningError";
        public const string TerraformApplyFailed = "TerraformApplyFailed";

        // 🏦 Vault
        public const string VaultTokenExpired = "VaultTokenExpired";
        public const string VaultAccessDenied = "VaultAccessDenied";

        // 📡 Istio / Mesh
        public const string IstioRoutingFailure = "IstioRoutingFailure";
        public const string ServiceMeshRoutingError = "ServiceMeshRoutingError";

        // 📉 Monitoring / Tracing
        public const string TracingSpanError = "TracingSpanError";
        public const string MonitoringAlertMissed = "MonitoringAlertMissed";
    }

    public static class ErrorCodes
    {

        // 🌐 Common
        public const string ApiKeyMissing = "ApiKeyMissing";
        public const string DataIntegrityViolation = "DataIntegrityViolation";
        public const string DataValidationError = "DataValidationError";
        public const string DeserializationError = "DeserializationError";
        public const string FeatureNotSupported = "FeatureNotSupported";
        public const string FileUploadFailed = "FileUploadFailed";
        public const string Forbidden = "Forbidden";
        public const string InternalServerError = "InternalServerError";
        public const string InvalidOperation = "InvalidOperation";
        public const string InvalidParameter = "InvalidParameter";
        public const string InvalidRequest = "InvalidRequest";
        public const string MissingParameter = "MissingParameter";
        public const string NetworkError = "NetworkError";
        public const string NotImplemented = "NotImplemented";
        public const string OperationFailed = "OperationFailed";
        public const string RateLimitExceeded = "RateLimitExceeded";
        public const string RequestTimeout = "RequestTimeout";
        public const string ResourceConflict = "ResourceConflict";
        public const string ResourceNotFound = "ResourceNotFound";
        public const string SerializationError = "SerializationError";
        public const string ServiceError = "ServiceError";
        public const string ServiceUnavailable = "ServiceUnavailable";
        public const string Timeout = "Timeout";
        public const string TokenExpired = "TokenExpired";
        public const string UnauthorizedAccess = "UnauthorizedAccess";
        public const string UnsupportedMediaType = "UnsupportedMediaType";
        public const string EnvironmentMismatch = "EnvironmentMismatch";
        public const string InvalidConfiguration = "InvalidConfiguration";
        public const string MissingConfiguration = "MissingConfiguration";
        public const string ConcurrencyConflict = "ConcurrencyConflict";
        public const string HttpError = "HttpError";
        public const string RetryableError = "RetryableError";
        public const string UnsupportedOperation = "UnsupportedOperation";
        public const string DataConversionError = "DataConversionError";
        public const string FileOperationError = "FileOperationError";
        public const string FileReadFailed = "FileReadFailed";
        public const string FileWriteFailed = "FileWriteFailed";
        public const string HttpClientRetryExhausted = "HttpClientRetryExhausted";
        public const string HttpRequestError = "HttpRequestError";
        public const string EventPublishingFailed = "EventPublishingFailed";
        public const string IntegrationContractViolation = "IntegrationContractViolation";
        public const string MessageParsingError = "MessageParsingError";
        public const string QueueProcessingError = "QueueProcessingError";
        public const string ThirdPartyServiceFailure = "ThirdPartyServiceFailure";
        public const string ThirdPartyTimeout = "ThirdPartyTimeout";
        public const string WebhookDeliveryFailed = "WebhookDeliveryFailed";
        public const string InvalidAuthenticationToken = "InvalidAuthenticationToken";
        public const string SearchIndexingFailed = "SearchIndexingFailed";
        public const string SearchQueryError = "SearchQueryError";
        public const string ReviewSubmissionFailed = "ReviewSubmissionFailed";
        public const string ReviewValidationError = "ReviewValidationError";

        // 🏬 Warehouse
        public const string StockMovementFailed = "StockMovementFailed";
        public const string WarehouseSyncError = "WarehouseSyncError";

        // 📈 Analytics
        public const string AnalyticsProcessingError = "AnalyticsProcessingError";
        public const string MetricAggregationFailed = "MetricAggregationFailed";

        // 🎁 Discounts
        public const string CouponInvalid = "CouponInvalid";
        public const string DiscountApplicationFailed = "DiscountApplicationFailed";

        // 🕵️ Fraud Detection
        public const string FraudAlertTriggered = "FraudAlertTriggered";
        public const string FraudCheckFailed = "FraudCheckFailed";

        // 🧠 ML
        public const string ModelInferenceFailed = "ModelInferenceFailed";
        public const string ModelTrainingError = "ModelTrainingError";

        // 📸 Media
        public const string MediaProcessingError = "MediaProcessingError";
        public const string MediaUploadFailed = "MediaUploadFailed";

        // 🛡️ Secrets
        public const string SecretAccessFailed = "SecretAccessFailed";
        public const string SecretFetchFailed = "SecretFetchFailed";
        public const string SecretRotationError = "SecretRotationError";

        // 🧵 Multi-Tenancy
        public const string TenantContextMissing = "TenantContextMissing";
        public const string TenantIsolationBreach = "TenantIsolationBreach";

        // 🫣 Masking
        public const string DataMaskingFailed = "DataMaskingFailed";
        public const string MaskingFailed = "MaskingFailed";

        // 🧠 Caching
        public const string CacheConnectionError = "CacheConnectionError";
        public const string CacheConnectionLost = "CacheConnectionLost";
        public const string CacheDesyncDetected = "CacheDesyncDetected";

        // 📅 Scheduler
        public const string JobExecutionError = "JobExecutionError";
        public const string JobSchedulingFailed = "JobSchedulingFailed";
        public const string SchedulerJobError = "SchedulerJobError";

        // 🔎 OpenSearch
        public const string OpenSearchQueryFailed = "OpenSearchQueryFailed";
        public const string OpenSearchUnavailable = "OpenSearchUnavailable";


        //------------------------------------------


        // ☸️ Kubernetes
        public const string PodCommunicationError = "PodCommunicationError";
        public const string KubeDeploymentFailed = "KubeDeploymentFailed";

        // ⚙️ CI/CD
        public const string BuildFailed = "BuildFailed";
        public const string PipelineTimeout = "PipelineTimeout";
        public const string CiCdDeploymentError = "CiCdDeploymentError";

        // 🌍 Terraform
        public const string InfraProvisioningError = "InfraProvisioningError";
        public const string TerraformApplyFailed = "TerraformApplyFailed";

        // 🏦 Vault
        public const string VaultTokenExpired = "VaultTokenExpired";
        public const string VaultAccessDenied = "VaultAccessDenied";

        // 📡 Istio / Mesh
        public const string ServiceMeshRoutingError = "ServiceMeshRoutingError";

        // 📉 Monitoring / Tracing
        public const string TracingSpanError = "TracingSpanError";
        public const string MonitoringAlertMissed = "MonitoringAlertMissed";

        // --- Standard HTTP Client Errors ---
        public const string BadRequest = "BadRequest";
        public const string UnauthorizedError = "UnauthorizedError";
        public const string ForbiddenError = "ForbiddenError";
        public const string NotFoundError = "NotFoundError";
        public const string MethodNotAllowedError = "MethodNotAllowedError";
        public const string RequestTimeoutError = "RequestTimeoutError";
        public const string ConflictError = "ConflictError";
        public const string UnsupportedMediaTypeError = "UnsupportedMediaTypeError";
        public const string TooManyRequestsError = "TooManyRequestsError";
        public const string ValidationError = "ValidationError";
        public const string InputError = "InputError";
        public const string RequestError = "RequestError";
        public const string ValidationFailedError = "ValidationFailedError";
        public const string ResourceNotAvailableError = "ResourceNotAvailableError";
        public const string ApiRateLimitExceededError = "ApiRateLimitExceededError";

        // --- Standard HTTP Server Errors ---
        public const string BadGatewayError = "BadGatewayError";
        public const string ServiceUnavailableError = "ServiceUnavailableError";
        public const string GatewayTimeoutError = "GatewayTimeoutError";

        // --- Domain-Specific Errors ---
        public const string AuthenticationError = "AuthenticationError";
        public const string AuthorizationError = "AuthorizationError";
        public const string CachingError = "CachingError";
        public const string CacheWriteError = "CacheWriteError";
        public const string FileUploadError = "FileUploadError";
        public const string FileDownloadError = "FileDownloadError";
        public const string QueueTimeoutError = "QueueTimeoutError";
        public const string MessageQueueError = "MessageQueueError";
        public const string WebhookDispatchError = "WebhookDispatchError";
        public const string JsonParsingError = "JsonParsingError";
        public const string EventProcessingError = "EventProcessingError";
        public const string CorrelationIdMissing = "CorrelationIdMissing";
        public const string ExternalServiceTimeoutError = "ExternalServiceTimeoutError";

        // --- Search & Indexing ---
        public const string SearchServiceError = "SearchServiceError";
        public const string IndexingError = "IndexingError";
        public const string SearchIndexError = "SearchIndexError";

        // --- Reviews & Ratings ---
        public const string ReviewSubmissionError = "ReviewSubmissionError";

        // --- Warehouse Operations ---
        public const string InventoryMismatchError = "InventoryMismatchError";

        // --- Analytics ---
        public const string AnalyticsError = "AnalyticsError";

        // --- Discounts & Offers ---
        public const string DiscountError = "DiscountError";
        public const string InvalidPromoCodeError = "InvalidPromoCodeError";

        // --- Fraud Detection ---
        public const string FraudDetectionError = "FraudDetectionError";

        // --- ML & Intelligence ---
        public const string MachineLearningError = "MachineLearningError";

        // --- Media Service ---
        public const string MediaServiceError = "MediaServiceError";

        // --- External Integrations ---
        public const string IntegrationError = "IntegrationError";
        public const string ContractViolationError = "ContractViolationError";

        // --- File Service ---
        public const string FileServiceError = "FileServiceError";
        public const string FileReadError = "FileReadError";
        public const string FileWriteError = "FileWriteError";
        public const string FileFormatError = "FileFormatError";

        // --- Secrets & Security ---
        public const string SecretsAccessError = "SecretsAccessError";
        public const string SecretNotFoundError = "SecretNotFoundError";
        public const string SecretDecryptionError = "SecretDecryptionError";

        // --- Multi-Tenancy ---
        public const string TenantResolutionError = "TenantResolutionError";
        public const string TenantContextMissingError = "TenantContextMissingError";
        public const string MultiTenantBoundaryError = "MultiTenantBoundaryError";

        // --- Masking ---
        public const string DataMaskingError = "DataMaskingError";
        public const string MaskingError = "MaskingError";

        // --- Caching ---
        public const string CacheSyncError = "CacheSyncError";
        public const string CacheMiss = "CacheMiss";

        // --- Scheduler ---
        public const string SchedulerError = "SchedulerError";
        public const string SchedulerJobMissed = "SchedulerJobMissed";

        // --- OpenSearch ---
        public const string OpenSearchError = "OpenSearchError";
        public const string OpenSearchIndexError = "OpenSearchIndexError";

        // --- Kubernetes ---
        public const string KubernetesError = "KubernetesError";

        // --- Terraform ---
        public const string TerraformProvisioningError = "TerraformProvisioningError";
        public const string TerraformStateMismatch = "TerraformStateMismatch";

        // --- Vault ---
        public const string VaultAccessError = "VaultAccessError";
        public const string VaultConnectionError = "VaultConnectionError";

        // --- Istio / Service Mesh ---
        public const string IstioRoutingError = "IstioRoutingError";
        public const string IstioConfigError = "IstioConfigError";

        // --- CI/CD ---
        public const string CiCdPipelineError = "CiCdPipelineError";
        // --- New Domain-Specific Errors ---
        public const string DatabaseConnectionError = "DatabaseConnectionError";
        public const string DataIntegrityError = "DataIntegrityError";
        public const string MessagingError = "MessagingError";
        public const string PaymentProcessingError = "PaymentProcessingError";
        public const string OrderProcessingError = "OrderProcessingError";
        public const string ShippingError = "ShippingError";
        public const string CustomerServiceError = "CustomerServiceError";
    }
}
