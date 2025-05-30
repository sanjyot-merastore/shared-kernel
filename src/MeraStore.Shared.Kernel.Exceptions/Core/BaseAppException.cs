namespace MeraStore.Shared.Kernel.Exceptions.Core;

[ExcludeFromCodeCoverage]
public abstract class BaseAppException : Exception
{
    public string ServiceIdentifier { get; }
    public string EventCode { get; }
    public string ErrorCode { get; }
    public HttpStatusCode StatusCode { get; }
    public ExceptionCategory Category { get; }
    public ExceptionSeverity Severity { get; }


    /// <summary>
    /// Fully qualified error code: ServiceId-EventCode-ErrorCode
    /// </summary>
    public string FullErrorCode => $"{ServiceIdentifier}-{EventCode}-{ErrorCode}";

    protected BaseAppException(string serviceIdentifier, string eventCode, string errorCode, HttpStatusCode statusCode, string message,
        ExceptionCategory category = ExceptionCategory.General, ExceptionSeverity severity = ExceptionSeverity.Major) 
        : base(message)
    {
        ServiceIdentifier = serviceIdentifier ?? throw new ArgumentNullException(nameof(serviceIdentifier));
        EventCode = eventCode ?? throw new ArgumentNullException(nameof(eventCode));
        ErrorCode = errorCode ?? throw new ArgumentNullException(nameof(errorCode));
        StatusCode = statusCode;
        Category = category;
        Severity = severity;
    }

    protected BaseAppException(string serviceIdentifier, string eventCode, string errorCode, HttpStatusCode statusCode, string message, Exception? innerException, ExceptionCategory category = ExceptionCategory.General, ExceptionSeverity severity = ExceptionSeverity.Major) 
        : base(message, innerException)
    {
        ServiceIdentifier = serviceIdentifier ?? throw new ArgumentNullException(nameof(serviceIdentifier));
        EventCode = eventCode ?? throw new ArgumentNullException(nameof(eventCode));
        ErrorCode = errorCode ?? throw new ArgumentNullException(nameof(errorCode));
        StatusCode = statusCode;
        Category = category;
        Severity = severity;
    }

    public override string ToString()
    {
        return $"{GetType().Name}: {Message} [FullErrorCode={FullErrorCode}, StatusCode={(int)StatusCode}]";
    }
}