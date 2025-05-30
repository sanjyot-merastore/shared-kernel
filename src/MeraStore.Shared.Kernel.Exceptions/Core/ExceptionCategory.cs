namespace MeraStore.Shared.Kernel.Exceptions.Core;

/// <summary>
/// Defines standardized categories for exception classification, aiding observability, diagnostics, and handling strategies.
/// </summary>
public enum ExceptionCategory
{
    General = 0,                // Fallback/default
    Validation = 1,             // Input or model validation errors
    Configuration = 2,          // Missing or invalid configuration
    Security = 3,               // General security violations
    Authentication = 4,         // Identity-related failures
    Authorization = 5,          // Permission-related failures
    Database = 6,               // DB-level issues (queries, connection, constraints)
    Network = 7,                // Network communication failures (timeouts, DNS, etc.)
    ApiCommunication = 8,       // External/internal API request/response failures
    ExternalDependency = 9,     // Third-party or service integration failures
    Operational = 10,           // System-level runtime errors (disk, memory, IO, etc.)
    CrossCutting = 11                // Logging, Masking
}