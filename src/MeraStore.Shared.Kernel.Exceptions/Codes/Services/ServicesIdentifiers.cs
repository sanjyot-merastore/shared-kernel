namespace MeraStore.Shared.Kernel.Exceptions.Codes.Services;

public static class ServiceIdentifiers
{
    public static readonly Dictionary<string, string> ServiceCodes = new()
    {
        // Core Business Microservices
        { Constants.ServiceIdentifiers.ProductService, "01" },
        { Constants.ServiceIdentifiers.UserService, "02" },
        { Constants.ServiceIdentifiers.InventoryService, "03" },
        { Constants.ServiceIdentifiers.CartService, "04" },
        { Constants.ServiceIdentifiers.OrderService, "05" },
        { Constants.ServiceIdentifiers.AuthService, "06" },
        { Constants.ServiceIdentifiers.ApiGateway, "07" },
        { Constants.ServiceIdentifiers.EventBus, "08" },
        { Constants.ServiceIdentifiers.Database, "09" },
        { Constants.ServiceIdentifiers.Security, "10" },
        { Constants.ServiceIdentifiers.Configuration, "11" },
        { Constants.ServiceIdentifiers.DataValidation, "12" },
        { Constants.ServiceIdentifiers.Operational, "13" },
        { Constants.ServiceIdentifiers.Network, "14" },
        { Constants.ServiceIdentifiers.General, "15" },
        { Constants.ServiceIdentifiers.PaymentService, "16" },
        { Constants.ServiceIdentifiers.ShippingService, "17" },
        { Constants.ServiceIdentifiers.NotificationService, "18" },
        { Constants.ServiceIdentifiers.SearchService, "19" },
        { Constants.ServiceIdentifiers.ReviewService, "20" },
        { Constants.ServiceIdentifiers.WarehouseService, "21" },
        { Constants.ServiceIdentifiers.AnalyticsService, "22" },
        { Constants.ServiceIdentifiers.DiscountService, "23" },
        { Constants.ServiceIdentifiers.FraudDetectionService, "24" },
        { Constants.ServiceIdentifiers.LoggingService, "25" },

        // Supporting Domain Extensions
        { Constants.ServiceIdentifiers.MediaService, "26" },
        { Constants.ServiceIdentifiers.MlService, "27" },
        { Constants.ServiceIdentifiers.WebhookProcessor, "28" },
        { Constants.ServiceIdentifiers.ExternalIntegration, "29" },
        { Constants.ServiceIdentifiers.FileService, "30" },

        // Infrastructure, Observability & Platform
        { Constants.ServiceIdentifiers.Tracing, "31" },
        { Constants.ServiceIdentifiers.Monitoring, "32" },
        { Constants.ServiceIdentifiers.Secrets, "33" },
        { Constants.ServiceIdentifiers.Caching, "34" },
        { Constants.ServiceIdentifiers.MultiTenancy, "35" },
        { Constants.ServiceIdentifiers.Masking, "36" },
        { Constants.ServiceIdentifiers.HttpClient, "37" },
        { Constants.ServiceIdentifiers.Scheduler, "38" },
        { Constants.ServiceIdentifiers.OpenSearch, "39" },
        { Constants.ServiceIdentifiers.FileStorage, "40" },
        { Constants.ServiceIdentifiers.Kubernetes, "41" },
        { Constants.ServiceIdentifiers.CI_CD, "42" },
        { Constants.ServiceIdentifiers.Terraform, "43" },
        { Constants.ServiceIdentifiers.Vault, "44" },
        { Constants.ServiceIdentifiers.Istio, "45" }
    };
}
