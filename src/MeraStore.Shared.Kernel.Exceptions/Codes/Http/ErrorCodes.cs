namespace MeraStore.Shared.Kernel.Exceptions.Codes.Http;

public static class ErrorCodes
{
    public static readonly Dictionary<string, string> Codes = new()
    {
        // Client Errors
        { Constants.ErrorCodes.BadRequest, "400" },
        { Constants.ErrorCodes.DataValidationError, "402" },
        { Constants.ErrorCodes.Forbidden, "403" },
        { Constants.ErrorCodes.UnauthorizedAccess, "401" },
        { Constants.ErrorCodes.ResourceNotFound, "404" },
        { Constants.ErrorCodes.UnsupportedMediaType, "415" },
        { Constants.ErrorCodes.RateLimitExceeded, "429" },
        { Constants.ErrorCodes.RequestTimeout, "408" },
        { Constants.ErrorCodes.Timeout, "408" },
        { Constants.ErrorCodes.ResourceConflict, "409" },
        { Constants.ErrorCodes.ConcurrencyConflict, "409" },
        { Constants.ErrorCodes.FeatureNotSupported, "422" }, // Assuming feature not supported fits here
        { Constants.ErrorCodes.RetryableError, "499" }, // Client closed request or similar

        // Network & HTTP Communication Errors
        { Constants.ErrorCodes.NetworkError, "498" }, // Custom network error code
        { Constants.ErrorCodes.HttpRequestError, "499" },
        { Constants.ErrorCodes.CorrelationIdMissing, "611" },
        { Constants.ErrorCodes.ExternalServiceTimeoutError, "612" },

        // Server Errors
        { Constants.ErrorCodes.ServiceError, "500" },
        { Constants.ErrorCodes.ServiceUnavailable, "503" },
        { Constants.ErrorCodes.InternalServerError, "500" },
        { Constants.ErrorCodes.BadGatewayError, "502" },
        { Constants.ErrorCodes.GatewayTimeoutError, "504" },

        // Authentication & Authorization
        { Constants.ErrorCodes.TokenExpired, "850" },
        { Constants.ErrorCodes.AuthenticationError, "600" },
        { Constants.ErrorCodes.AuthorizationError, "601" },
        { Constants.ErrorCodes.InvalidAuthenticationToken, "850" },

        // Configuration Errors
        { Constants.ErrorCodes.MissingConfiguration, "900" },
        { Constants.ErrorCodes.InvalidConfiguration, "901" },

        // Data & Validation
        { Constants.ErrorCodes.DataConversionError, "902" },

        // Domain-Specific & Business Logic Errors
        { Constants.ErrorCodes.ApiRateLimitExceededError, "438" },
        { Constants.ErrorCodes.ValidationError, "433" },
        { Constants.ErrorCodes.InputError, "434" },
        { Constants.ErrorCodes.RequestError, "435" },
        { Constants.ErrorCodes.ValidationFailedError, "436" },
        { Constants.ErrorCodes.ResourceNotAvailableError, "437" },

        // Caching
        { Constants.ErrorCodes.CachingError, "602" },
        { Constants.ErrorCodes.CacheWriteError, "603" },
        { Constants.ErrorCodes.CacheSyncError, "740" },
        { Constants.ErrorCodes.CacheConnectionError, "741" },
        { Constants.ErrorCodes.CacheMiss, "742" },
        { Constants.ErrorCodes.CacheConnectionLost, "874" },
        { Constants.ErrorCodes.CacheDesyncDetected, "875" },

        // File & Media Services
        { Constants.ErrorCodes.FileUploadError, "604" },
        { Constants.ErrorCodes.FileUploadFailed, "831" },
        { Constants.ErrorCodes.FileDownloadError, "605" },
        { Constants.ErrorCodes.FileServiceError, "700" },
        { Constants.ErrorCodes.FileReadError, "701" },
        { Constants.ErrorCodes.FileWriteError, "702" },
        { Constants.ErrorCodes.FileFormatError, "703" },
        { Constants.ErrorCodes.FileOperationError, "839" },
        { Constants.ErrorCodes.FileReadFailed, "840" },
        { Constants.ErrorCodes.FileWriteFailed, "841" },
        { Constants.ErrorCodes.MediaServiceError, "680" },
        { Constants.ErrorCodes.MediaUploadFailed, "865" },
        { Constants.ErrorCodes.MediaProcessingError, "866" },

        // Messaging & Queues
        { Constants.ErrorCodes.MessageQueueError, "607" },
        { Constants.ErrorCodes.QueueTimeoutError, "606" },
        { Constants.ErrorCodes.QueueProcessingError, "845" },
        { Constants.ErrorCodes.MessageParsingError, "847" },
        { Constants.ErrorCodes.EventPublishingFailed, "844" },

        // Webhooks & Event Processing
        { Constants.ErrorCodes.WebhookDispatchError, "608" },
        { Constants.ErrorCodes.WebhookDeliveryFailed, "846" },
        { Constants.ErrorCodes.EventProcessingError, "610" },

        // Search & Indexing
        { Constants.ErrorCodes.SearchServiceError, "613" },
        { Constants.ErrorCodes.IndexingError, "614" },
        { Constants.ErrorCodes.SearchIndexError, "615" },
        { Constants.ErrorCodes.SearchIndexingFailed, "851" },
        { Constants.ErrorCodes.SearchQueryError, "852" },

        // Reviews & Ratings
        { Constants.ErrorCodes.ReviewSubmissionError, "620" },
        { Constants.ErrorCodes.ReviewValidationError, "853" },
        { Constants.ErrorCodes.ReviewSubmissionFailed, "854" },

        // Warehouse & Inventory
        { Constants.ErrorCodes.WarehouseSyncError, "630" },
        { Constants.ErrorCodes.InventoryMismatchError, "631" },
        { Constants.ErrorCodes.StockMovementFailed, "856" },

        // Analytics
        { Constants.ErrorCodes.AnalyticsError, "640" },
        { Constants.ErrorCodes.AnalyticsProcessingError, "857" },
        { Constants.ErrorCodes.MetricAggregationFailed, "858" },

        // Discounts & Promotions
        { Constants.ErrorCodes.DiscountError, "650" },
        { Constants.ErrorCodes.InvalidPromoCodeError, "651" },
        { Constants.ErrorCodes.DiscountApplicationFailed, "859" },
        { Constants.ErrorCodes.CouponInvalid, "860" },

        // Fraud Detection
        { Constants.ErrorCodes.FraudDetectionError, "660" },
        { Constants.ErrorCodes.FraudCheckFailed, "861" },
        { Constants.ErrorCodes.FraudAlertTriggered, "862" },

        // Machine Learning & AI
        { Constants.ErrorCodes.MachineLearningError, "670" },
        { Constants.ErrorCodes.ModelInferenceFailed, "863" },
        { Constants.ErrorCodes.ModelTrainingError, "864" },

        // Secrets & Security
        { Constants.ErrorCodes.SecretsAccessError, "710" },
        { Constants.ErrorCodes.SecretNotFoundError, "711" },
        { Constants.ErrorCodes.SecretDecryptionError, "712" },
        { Constants.ErrorCodes.SecretAccessFailed, "867" },
        { Constants.ErrorCodes.SecretRotationError, "868" },
        { Constants.ErrorCodes.SecretFetchFailed, "869" },

        // Multi-Tenancy
        { Constants.ErrorCodes.TenantResolutionError, "720" },
        { Constants.ErrorCodes.TenantContextMissingError, "721" },
        { Constants.ErrorCodes.TenantContextMissing, "870" },
        { Constants.ErrorCodes.MultiTenantBoundaryError, "722" },
        { Constants.ErrorCodes.TenantIsolationBreach, "871" },

        // Masking
        { Constants.ErrorCodes.DataMaskingError, "730" },
        { Constants.ErrorCodes.MaskingError, "731" },
        { Constants.ErrorCodes.DataMaskingFailed, "872" },
        { Constants.ErrorCodes.MaskingFailed, "873" },

        // Scheduler & Jobs
        { Constants.ErrorCodes.SchedulerError, "750" },
        { Constants.ErrorCodes.SchedulerJobMissed, "751" },
        { Constants.ErrorCodes.JobSchedulingFailed, "876" },
        { Constants.ErrorCodes.JobExecutionError, "877" },
        { Constants.ErrorCodes.SchedulerJobError, "878" },

        // OpenSearch
        { Constants.ErrorCodes.OpenSearchError, "760" },
        { Constants.ErrorCodes.OpenSearchIndexError, "761" },
        { Constants.ErrorCodes.OpenSearchUnavailable, "879" },
        { Constants.ErrorCodes.OpenSearchQueryFailed, "880" },

        // Kubernetes & Cloud Infrastructure
        { Constants.ErrorCodes.KubernetesError, "770" },
        { Constants.ErrorCodes.PodCommunicationError, "771" },
        { Constants.ErrorCodes.KubeDeploymentFailed, "881" },

        // Terraform & Provisioning
        { Constants.ErrorCodes.TerraformProvisioningError, "780" },
        { Constants.ErrorCodes.TerraformStateMismatch, "781" },
        { Constants.ErrorCodes.InfraProvisioningError, "883" },
        { Constants.ErrorCodes.TerraformApplyFailed, "884" },

        // Vault & Secrets Management
        { Constants.ErrorCodes.VaultAccessError, "790" },
        { Constants.ErrorCodes.VaultConnectionError, "791" },
        { Constants.ErrorCodes.VaultTokenExpired, "792" },
        { Constants.ErrorCodes.VaultAccessDenied, "885" },

        // Service Mesh & Istio
        { Constants.ErrorCodes.IstioRoutingError, "800" },
        { Constants.ErrorCodes.IstioConfigError, "801" },
        { Constants.ErrorCodes.ServiceMeshRoutingError, "802" },

        // CI/CD Pipeline
        { Constants.ErrorCodes.CiCdPipelineError, "810" },
        { Constants.ErrorCodes.BuildFailed, "811" },
        { Constants.ErrorCodes.PipelineTimeout, "812" },
        { Constants.ErrorCodes.CiCdDeploymentError, "882" },

        // General Errors & Misc
        { Constants.ErrorCodes.DatabaseConnectionError, "820" },
        { Constants.ErrorCodes.DataIntegrityError, "821" },
        { Constants.ErrorCodes.MessagingError, "822" },
        { Constants.ErrorCodes.PaymentProcessingError, "823" },
        { Constants.ErrorCodes.OrderProcessingError, "824" },
        { Constants.ErrorCodes.ShippingError, "825" },
        { Constants.ErrorCodes.CustomerServiceError, "826" },
        { Constants.ErrorCodes.ApiKeyMissing, "830" },
        { Constants.ErrorCodes.DeserializationError, "832" },
        { Constants.ErrorCodes.SerializationError, "833" },
        { Constants.ErrorCodes.InvalidOperation, "834" },
        { Constants.ErrorCodes.OperationFailed, "835" },
        { Constants.ErrorCodes.EnvironmentMismatch, "836" },
        { Constants.ErrorCodes.HttpError, "837" },
        { Constants.ErrorCodes.UnsupportedOperation, "838" },
        { Constants.ErrorCodes.HttpClientRetryExhausted, "842" },
        { Constants.ErrorCodes.ThirdPartyServiceFailure, "843" },
        { Constants.ErrorCodes.ThirdPartyTimeout, "848" },
        { Constants.ErrorCodes.IntegrationContractViolation, "849" },
        { Constants.ErrorCodes.TracingSpanError, "886" },
        { Constants.ErrorCodes.MonitoringAlertMissed, "887" },
        {
            Constants.ErrorCodes.MethodNotAllowedError, "405"
        },
        {
            Constants.ErrorCodes.RequestTimeoutError, "408"
        },
        {
            Constants.ErrorCodes.ConflictError, "409"
        },
        {
            Constants.ErrorCodes.UnsupportedMediaTypeError, "415"
        },
        {
            Constants.ErrorCodes.TooManyRequestsError, "429"
        },
        {
            Constants.ErrorCodes.JsonParsingError, "609"
        },
        {
            Constants.ErrorCodes.IntegrationError, "690"
        },
        {
            Constants.ErrorCodes.ContractViolationError, "691"
        }
    };

}