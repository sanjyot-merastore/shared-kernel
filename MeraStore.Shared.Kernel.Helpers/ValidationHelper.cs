using System.Text.RegularExpressions;

namespace MeraStore.Shared.Kernel.Helpers;

/// <summary>
/// Provides common validation utility methods.
/// </summary>
public static class ValidationHelper
{
    /// <summary>
    /// Validates if the given string is null, empty, or consists only of white-space characters.
    /// </summary>
    public static bool IsNullOrWhiteSpace(string? value) =>
        string.IsNullOrWhiteSpace(value);

    /// <summary>
    /// Validates if the given collection is null or empty.
    /// </summary>
    public static bool IsNullOrEmpty<T>(IEnumerable<T>? collection) =>
        collection == null || !collection.Any();

    /// <summary>
    /// Validates if the given object is null.
    /// </summary>
    public static bool IsNull(object? obj) => obj is null;

    /// <summary>
    /// Validates if the given string matches the specified regular expression pattern.
    /// </summary>
    /// <param name="value">Input string to validate.</param>
    /// <param name="pattern">Regex pattern to match.</param>
    /// <param name="options">Optional Regex options.</param>
    /// <returns>True if the string matches the pattern; otherwise false.</returns>
    public static bool IsMatch(string? value, string pattern, RegexOptions options = RegexOptions.None)
    {
        if (string.IsNullOrEmpty(value)) return false;
        return Regex.IsMatch(value, pattern, options);
    }

    /// <summary>
    /// Validates if the string is a valid email address.
    /// </summary>
    public static bool IsValidEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;

        const string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, emailPattern, RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// Validates if the string is a valid URL.
    /// </summary>
    public static bool IsValidUrl(string? url)
    {
        if (string.IsNullOrWhiteSpace(url)) return false;

        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) &&
               (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }

    /// <summary>
    /// Validates if the string is a valid IPv4 or IPv6 address.
    /// </summary>
    public static bool IsValidIpAddress(string? ip)
    {
        if (string.IsNullOrWhiteSpace(ip)) return false;
        return System.Net.IPAddress.TryParse(ip, out _);
    }

    /// <summary>
    /// Validates if the string represents a valid integer number.
    /// </summary>
    public static bool IsValidInteger(string? value) =>
        int.TryParse(value, out _);

    /// <summary>
    /// Validates if the string represents a valid decimal number.
    /// </summary>
    public static bool IsValidDecimal(string? value) =>
        decimal.TryParse(value, out _);

    /// <summary>
    /// Validates if the string contains only alphanumeric characters (letters and digits).
    /// </summary>
    public static bool IsAlphanumeric(string? value)
    {
        if (string.IsNullOrEmpty(value)) return false;
        return Regex.IsMatch(value, @"^[a-zA-Z0-9]*$");
    }

    /// <summary>
    /// Validates if the string contains only alphabetic characters (letters).
    /// </summary>
    public static bool IsAlphabetic(string? value)
    {
        if (string.IsNullOrEmpty(value)) return false;
        return Regex.IsMatch(value, @"^[a-zA-Z]*$");
    }

    /// <summary>
    /// Validates if the object is of a specified type.
    /// </summary>
    public static bool IsOfType<T>(object? obj) =>
        obj is T;
}

