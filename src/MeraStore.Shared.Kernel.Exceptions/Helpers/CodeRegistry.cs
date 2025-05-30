using MeraStore.Shared.Kernel.Exceptions.Codes.Services;

namespace MeraStore.Shared.Kernel.Exceptions.Helpers;

public static class CodeRegistry
{
    private const string DefaultCode = "00";
    private const string DefaultKey = "unknown";

    public static string GetCode(string name, Dictionary<string, string> registry)
    {
        return registry.GetValueOrDefault(name, DefaultCode);
    }

    public static string GetKey(string code, Dictionary<string, string> registry)
    {
        foreach (var kvp in registry.Where(kvp => kvp.Value == code))
        {
            return kvp.Key;
        }

        return DefaultKey;
    }
}

public static class ServiceCodeRegistry
{
    public static string GetCode(string serviceName)
        => CodeRegistry.GetCode(serviceName, ServiceIdentifiers.ServiceCodes);

    public static string GetKey(string serviceCode)
        => CodeRegistry.GetKey(serviceCode, ServiceIdentifiers.ServiceCodes);
}

public static class EventCodeRegistry
{
    public static string GetCode(string eventName)
        => CodeRegistry.GetCode(eventName, EventCodes.Codes);

    public static string GetKey(string eventCode)
        => CodeRegistry.GetKey(eventCode, EventCodes.Codes);
}

public static class ErrorCodeRegistry
{
    public static string GetCode(string errorName)
        => CodeRegistry.GetCode(errorName, ErrorCodes.Codes);

    public static string GetKey(string errorCode)
        => CodeRegistry.GetKey(errorCode, ErrorCodes.Codes);
}
