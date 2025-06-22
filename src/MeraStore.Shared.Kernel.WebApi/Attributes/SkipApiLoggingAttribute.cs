namespace MeraStore.Shared.Kernel.WebApi.Attributes;

/// <summary>
/// Skips logging for the decorated controller or action method.
/// Can be used on MVC, Minimal API, or endpoint-based routes.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
public class SkipApiLoggingAttribute : Attribute
{
}