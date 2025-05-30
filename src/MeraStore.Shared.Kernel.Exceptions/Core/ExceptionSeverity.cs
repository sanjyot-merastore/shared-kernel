namespace MeraStore.Shared.Kernel.Exceptions.Core;

/// <summary>
/// Indicates how severe the exception is — for alerting, logging, and triaging.
/// </summary>
public enum ExceptionSeverity
{
    Info = 0,
    Warning = 1,
    Minor = 2,
    Major = 3,
    Critical = 4,
    Fatal = 5
}