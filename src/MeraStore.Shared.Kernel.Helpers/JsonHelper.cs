using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace MeraStore.Shared.Kernel.Helpers;

/// <summary>
/// Provides JSON serialization and deserialization helpers using Newtonsoft.Json.
/// Includes default and indented settings with camelCase and enum string handling.
/// </summary>
public static class JsonHelper
{
    /// <summary>
    /// Default JSON serializer settings (non-indented).
    /// Null values are ignored, enums are serialized as camelCase strings, and missing members are ignored during deserialization.
    /// </summary>
    public static readonly JsonSerializerSettings DefaultSettings = new()
    {
        NullValueHandling = NullValueHandling.Ignore,
        Formatting = Formatting.None,
        MissingMemberHandling = MissingMemberHandling.Ignore,
        Converters = { new StringEnumConverter(new CamelCaseNamingStrategy(), allowIntegerValues: true) },
        ContractResolver = new CamelCasePropertyNamesContractResolver()
    };

    /// <summary>
    /// Indented JSON serializer settings (pretty-print).
    /// Same as default but with formatting set to indented.
    /// </summary>
    public static readonly JsonSerializerSettings IndentedSettings = new()
    {
        NullValueHandling = NullValueHandling.Ignore,
        Formatting = Formatting.Indented,
        MissingMemberHandling = MissingMemberHandling.Ignore,
        Converters = { new StringEnumConverter(new CamelCaseNamingStrategy(), allowIntegerValues: true) },
        ContractResolver = new CamelCasePropertyNamesContractResolver()
    };

    /// <summary>
    /// Serializes an object to JSON string using default or indented formatting.
    /// </summary>
    /// <typeparam name="T">Type of object to serialize.</typeparam>
    /// <param name="obj">Object instance to serialize.</param>
    /// <param name="indented">If true, output JSON is indented (pretty-printed).</param>
    /// <returns>Serialized JSON string or empty string if <paramref name="obj"/> is null.</returns>
    public static string Serialize<T>(T obj, bool indented = false)
    {
        if (obj == null) return string.Empty;
        var settings = indented ? IndentedSettings : DefaultSettings;
        return JsonConvert.SerializeObject(obj, settings);
    }

    /// <summary>
    /// Deserializes a JSON string into an object of type T.
    /// Returns default value if input is null/empty or deserialization fails.
    /// </summary>
    /// <typeparam name="T">Type to deserialize to.</typeparam>
    /// <param name="json">JSON string.</param>
    /// <returns>Deserialized object or default if unsuccessful.</returns>
    public static T? Deserialize<T>(string? json)
    {
        if (string.IsNullOrWhiteSpace(json)) return default;
        try
        {
            return JsonConvert.DeserializeObject<T>(json, DefaultSettings);
        }
        catch (JsonException)
        {
            return default;
        }
    }

    /// <summary>
    /// Tries to deserialize a JSON string into an object of type T.
    /// Returns a boolean indicating success or failure.
    /// </summary>
    /// <typeparam name="T">Type to deserialize to.</typeparam>
    /// <param name="json">JSON string.</param>
    /// <param name="result">Output deserialized object if successful; otherwise, default.</param>
    /// <returns>True if deserialization was successful; otherwise, false.</returns>
    public static bool TryDeserialize<T>(string? json, out T? result)
    {
        result = default;
        if (string.IsNullOrWhiteSpace(json)) return false;

        try
        {
            result = JsonConvert.DeserializeObject<T>(json, DefaultSettings);
            return result != null;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Extension method to serialize an object to JSON string with optional indentation.
    /// </summary>
    /// <typeparam name="T">Type of object.</typeparam>
    /// <param name="obj">Object instance.</param>
    /// <param name="indented">If true, pretty-print JSON.</param>
    /// <returns>Serialized JSON string.</returns>
    public static string ToJson<T>(this T obj, bool indented = false) => Serialize(obj, indented);

    /// <summary>
    /// Extension method to deserialize a JSON string into an object of type T.
    /// </summary>
    /// <typeparam name="T">Target type.</typeparam>
    /// <param name="json">JSON string.</param>
    /// <returns>Deserialized object or default if failed.</returns>
    public static T? FromJson<T>(this string? json) => Deserialize<T>(json);

    /// <summary>
    /// Extension method to attempt deserialization of JSON string into an object of type T.
    /// </summary>
    /// <typeparam name="T">Target type.</typeparam>
    /// <param name="json">JSON string.</param>
    /// <param name="result">Output deserialized object.</param>
    /// <returns>True if successful; otherwise false.</returns>
    public static bool TryFromJson<T>(this string? json, out T? result) => TryDeserialize(json, out result);
}
