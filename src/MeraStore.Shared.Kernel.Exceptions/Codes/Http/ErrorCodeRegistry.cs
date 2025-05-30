using System.Collections.ObjectModel;

namespace MeraStore.Shared.Kernel.Exceptions.Codes.Http;

public static class ErrorCodeRegistry
{
    private static readonly Dictionary<string, string> CoreCodes = new(ErrorCodes.Codes);
    private static readonly Dictionary<string, string> AdditionalCodes = new();

    public static IReadOnlyDictionary<string, string> AllCodes =>
        new ReadOnlyDictionary<string, string>(
            CoreCodes.Concat(AdditionalCodes).ToDictionary(kvp => kvp.Key, kvp => kvp.Value));

    public static void Register(string key, string code)
    {
        if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Invalid error code or key.");

        if (CoreCodes.ContainsKey(key) || AdditionalCodes.ContainsKey(key))
            throw new InvalidOperationException($"Error code key '{key}' already exists.");

        AdditionalCodes[key] = code;
    }

    public static string GetCode(string key)
    {
        if (AllCodes.TryGetValue(key, out var addCode))
            return addCode;

        throw new KeyNotFoundException($"Error code key '{key}' not found.");
    }
}