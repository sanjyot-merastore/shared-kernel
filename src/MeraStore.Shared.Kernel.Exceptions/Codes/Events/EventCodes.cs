namespace MeraStore.Shared.Kernel.Exceptions.Codes.Events;

public static class EventCodes
{
    public static readonly Dictionary<string, string> Codes = new()
    {
        { Constants.EventCodes.ApiError, "151" },
        { Constants.EventCodes.ApiKeyMissing, "111" },
        { Constants.EventCodes.DataIntegrityViolation, "116" },
        { Constants.EventCodes.DataValidationError, "107" },
        { Constants.EventCodes.FeatureNotSupported, "113" },
        { Constants.EventCodes.Forbidden, "104" },
        { Constants.EventCodes.InternalServerError, "106" },
        { Constants.EventCodes.InvalidParameter, "118" },
        { Constants.EventCodes.InvalidRequest, "102" },
        { Constants.EventCodes.MissingParameter, "117" },
        { Constants.EventCodes.NetworkError, "115" },
        { Constants.EventCodes.NotImplemented, "110" },
        { Constants.EventCodes.RateLimitExceeded, "114" },
        { Constants.EventCodes.RequestTimeout, "119" },
        { Constants.EventCodes.ResourceConflict, "105" },
        { Constants.EventCodes.ResourceNotFound, "101" },
        { Constants.EventCodes.ServiceError, "109" },
        { Constants.EventCodes.ServiceUnavailable, "100" },
        { Constants.EventCodes.Timeout, "108" },
        { Constants.EventCodes.TokenExpired, "112" },
        { Constants.EventCodes.HttpClientRetryExhausted, "180" },
        { Constants.EventCodes.UnauthorizedAccess, "103" },
        { Constants.EventCodes.UnsupportedMediaType, "120" },
        { Constants.EventCodes.DeserializationError, "121" },
        { Constants.EventCodes.SerializationError, "122" },
        { Constants.EventCodes.InvalidOperation, "123" },
        { Constants.EventCodes.OperationFailed, "124" },

        // Configuration
        { Constants.EventCodes.MissingConfiguration, "125" },
        { Constants.EventCodes.InvalidConfiguration, "126" },
        { Constants.EventCodes.EnvironmentMismatch, "127" },

        // Retry/Concurrency
        { Constants.EventCodes.RetryableError, "128" },
        { Constants.EventCodes.HttpError, "129" },
        { Constants.EventCodes.UnsupportedOperation, "130" },
        { Constants.EventCodes.ConcurrencyConflict, "131" },

        // Data & Files
        { Constants.EventCodes.DataConversionError, "132" },
        { Constants.EventCodes.FileOperationError, "133" },
        { Constants.EventCodes.HttpRequestError, "134" },
        { Constants.EventCodes.FileReadFailed, "159" },
        { Constants.EventCodes.FileWriteFailed, "160" },
        { Constants.EventCodes.FileUploadFailed, "146" },

        // Third-Party & Messaging
        { Constants.EventCodes.ThirdPartyServiceFailure, "135" },
        { Constants.EventCodes.EventPublishingFailed, "136" },
        { Constants.EventCodes.QueueProcessingError, "137" },
        { Constants.EventCodes.WebhookDeliveryFailed, "138" },
        { Constants.EventCodes.MessageParsingError, "139" },
        { Constants.EventCodes.InvalidAuthenticationToken, "140" },
        { Constants.EventCodes.ThirdPartyTimeout, "157" },
        { Constants.EventCodes.IntegrationContractViolation, "158" },

        // Search & Discovery
        { Constants.EventCodes.SearchIndexingFailed, "141" },
        { Constants.EventCodes.SearchQueryError, "142" },

        // Reviews & Ratings
        { Constants.EventCodes.ReviewValidationError, "143" },
        { Constants.EventCodes.ReviewSubmissionFailed, "144" },

        // Warehouse Ops
        { Constants.EventCodes.WarehouseSyncError, "145" },
        { Constants.EventCodes.StockMovementFailed, "146" },

        // Analytics
        { Constants.EventCodes.AnalyticsProcessingError, "147" },
        { Constants.EventCodes.MetricAggregationFailed, "148" },

        // Discounts & Offers
        { Constants.EventCodes.DiscountApplicationFailed, "149" },
        { Constants.EventCodes.CouponInvalid, "150" },

        // Fraud Detection
        { Constants.EventCodes.FraudCheckFailed, "151" },
        { Constants.EventCodes.FraudAlertTriggered, "152" },

        // ML & Intelligence
        { Constants.EventCodes.ModelInferenceFailed, "153" },
        { Constants.EventCodes.ModelTrainingError, "154" },

        // Media
        { Constants.EventCodes.MediaUploadFailed, "155" },
        { Constants.EventCodes.MediaProcessingError, "156" },

        // Secrets & Security
        { Constants.EventCodes.SecretAccessFailed, "161" },
        { Constants.EventCodes.SecretRotationError, "162" },
        { Constants.EventCodes.SecretFetchFailed, "148" },

        // Multi-Tenancy
        { Constants.EventCodes.TenantContextMissing, "163" },
        { Constants.EventCodes.TenantIsolationBreach, "164" },

        // Masking
        { Constants.EventCodes.DataMaskingFailed, "165" },
        { Constants.EventCodes.MaskingFailed, "144" },

        // Caching
        { Constants.EventCodes.CacheConnectionLost, "166" },
        { Constants.EventCodes.CacheDesyncDetected, "167" },
        { Constants.EventCodes.CacheConnectionError, "143" },

        // Scheduler
        { Constants.EventCodes.JobSchedulingFailed, "168" },
        { Constants.EventCodes.JobExecutionError, "169" },
        { Constants.EventCodes.SchedulerJobError, "147" },

        // OpenSearch
        { Constants.EventCodes.OpenSearchUnavailable, "170" },
        { Constants.EventCodes.OpenSearchQueryFailed, "155" },

        // Kubernetes
        { Constants.EventCodes.PodCommunicationError, "171" },
        { Constants.EventCodes.KubeDeploymentFailed, "172" },

        // CI/CD
        { Constants.EventCodes.BuildFailed, "173" },
        { Constants.EventCodes.PipelineTimeout, "174" },
        { Constants.EventCodes.CiCdDeploymentError, "151" },

        // Terraform
        { Constants.EventCodes.InfraProvisioningError, "175" },
        { Constants.EventCodes.TerraformApplyFailed, "153" },

        // Vault
        { Constants.EventCodes.VaultTokenExpired, "176" },
        { Constants.EventCodes.VaultAccessDenied, "152" },

        // Istio / Mesh
        { Constants.EventCodes.IstioRoutingFailure, "154" },
        { Constants.EventCodes.ServiceMeshRoutingError, "177" },

        // Monitoring & Tracing
        { Constants.EventCodes.TracingSpanError, "149" },
        { Constants.EventCodes.MonitoringAlertMissed, "150" }
    };
}
