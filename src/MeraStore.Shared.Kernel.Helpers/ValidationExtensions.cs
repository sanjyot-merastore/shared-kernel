namespace MeraStore.Shared.Kernel.Helpers;

/// <summary>
/// Extension methods for validation on nullable types and fluent usage.
/// </summary>
public static class ValidationExtensions
{
    /// <summary>
    /// Checks if the string is null or whitespace.
    /// </summary>
    public static bool IsNullOrWhiteSpace(this string? value) =>
        ValidationHelper.IsNullOrWhiteSpace(value);

    /// <summary>
    /// Checks if the collection is null or empty.
    /// </summary>
    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? collection) =>
        ValidationHelper.IsNullOrEmpty(collection);

    /// <summary>
    /// Checks if the string is a valid email.
    /// </summary>
    public static bool IsValidEmail(this string? email) =>
        ValidationHelper.IsValidEmail(email);

    /// <summary>
    /// Checks if the string is a valid URL.
    /// </summary>
    public static bool IsValidUrl(this string? url) =>
        ValidationHelper.IsValidUrl(url);

    /// <summary>
    /// Checks if the string is a valid IP address.
    /// </summary>
    public static bool IsValidIpAddress(this string? ip) =>
        ValidationHelper.IsValidIpAddress(ip);

    /// <summary>
    /// Checks if the string represents a valid integer.
    /// </summary>
    public static bool IsValidInteger(this string? value) =>
        ValidationHelper.IsValidInteger(value);

    /// <summary>
    /// Checks if the string represents a valid decimal.
    /// </summary>
    public static bool IsValidDecimal(this string? value) =>
        ValidationHelper.IsValidDecimal(value);

    /// <summary>
    /// Checks if the string contains only alphanumeric characters.
    /// </summary>
    public static bool IsAlphanumeric(this string? value) =>
        ValidationHelper.IsAlphanumeric(value);

    /// <summary>
    /// Checks if the string contains only alphabetic characters.
    /// </summary>
    public static bool IsAlphabetic(this string? value) =>
        ValidationHelper.IsAlphabetic(value);

    /// <summary>
    /// Fluent validation helper that throws if the string is null or whitespace.
    /// </summary>
    public static string ThrowIfNullOrWhiteSpace(this string? value, string? paramName = null)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"String cannot be null or whitespace.{(paramName != null ? $" Parameter name: {paramName}" : string.Empty)}");

        return value;
    }

    /// <summary>
    /// Fluent validation helper that throws if the object is null.
    /// </summary>
    public static T ThrowIfNull<T>(this T? obj, string? paramName = null) where T : class
    {
        if (obj is null)
            throw new ArgumentNullException(paramName ?? nameof(obj));

        return obj;
    }

    /// <summary>
    /// Fluent validation helper that throws if the collection is null or empty.
    /// </summary>
    public static IEnumerable<T> ThrowIfNullOrEmpty<T>(this IEnumerable<T>? collection, string? paramName = null)
    {
        if (collection == null || !collection.Any())
            throw new ArgumentException($"Collection cannot be null or empty.{(paramName != null ? $" Parameter name: {paramName}" : string.Empty)}");

        return collection;
    }
}