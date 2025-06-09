using System.Text.RegularExpressions;

namespace MeraStore.Shared.Kernel.Helpers;

/// <summary>
/// Provides string extension methods for common transformations and utilities.
/// </summary>
public static class StringHelperExtensions
{
    /// <summary>
    /// Returns the first n characters of a string safely without throwing exceptions.
    /// </summary>
    /// <param name="value">Input string.</param>
    /// <param name="length">Number of characters to return.</param>
    /// <returns>Substring of up to n characters, or empty if null/empty.</returns>
    public static string SafeSubstring(this string? value, int length)
    {
        if (string.IsNullOrEmpty(value) || length <= 0)
            return string.Empty;

        return value.Length <= length ? value : value[..length];
    }
    /// <summary>
    /// Converts PascalCase or camelCase string to snake_case.
    /// </summary>
    public static string ToSnakeCase(this string str)
    {
        if (string.IsNullOrEmpty(str)) return str;

        var startUnderscores = Regex.Match(str, @"^_+", RegexOptions.None, TimeSpan.FromSeconds(1));
        var snake = Regex.Replace(str, @"([a-z0-9])([A-Z])", "$1_$2", RegexOptions.None, TimeSpan.FromSeconds(1)).ToLower();
        return startUnderscores + snake;
    }

    /// <summary>
    /// Converts the first character of the string to uppercase.
    /// </summary>
    public static string? UppercaseFirst(this string? value)
    {
        if (string.IsNullOrEmpty(value) || char.IsUpper(value[0]))
            return value;

        return char.ToUpper(value[0]) + value[1..];
    }

    /// <summary>
    /// Converts the first character of the string to lowercase.
    /// </summary>
    public static string? LowercaseFirst(this string? value)
    {
        if (string.IsNullOrEmpty(value) || char.IsLower(value[0]))
            return value;

        return char.ToLower(value[0]) + value[1..];
    }

    /// <summary>
    /// Reverses the characters in the string.
    /// </summary>
    public static string Reverse(this string? value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        var arr = value.ToCharArray();
        Array.Reverse(arr);
        return new string(arr);
    }

    /// <summary>
    /// Trims all whitespace characters from the string (start and end).
    /// </summary>
    public static string? TrimSafe(this string? value) =>
        value?.Trim();

    /// <summary>
    /// Checks if the string is null or empty, returns default value if true.
    /// </summary>
    public static string DefaultIfNullOrEmpty(this string? value, string defaultValue) =>
        string.IsNullOrEmpty(value) ? defaultValue : value;

    /// <summary>
    /// Converts a string to Title Case (first letter of each word uppercase).
    /// Note: Basic implementation; for culture-specific, consider TextInfo.ToTitleCase.
    /// </summary>
    public static string ToTitleCase(this string? value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        var words = value.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        for (var i = 0; i < words.Length; i++)
        {
            words[i] = words[i].UppercaseFirst();
        }
        return string.Join(' ', words);
    }

    /// <summary>
    /// Returns true if the string contains only whitespace characters or is null.
    /// </summary>
    public static bool IsNullOrWhiteSpace(this string? value) =>
        string.IsNullOrWhiteSpace(value);

    /// <summary>
    /// Returns true if the string contains only digits.
    /// </summary>
    public static bool IsDigitsOnly(this string? value)
    {
        return !string.IsNullOrEmpty(value) && value.All(char.IsDigit);
    }
}