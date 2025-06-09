using System.Reflection;

namespace MeraStore.Shared.Kernel.Helpers;

/// <summary>
/// Extension methods for working with enumeration types.
/// Includes helpers for parsing, getting names, descriptions, and checking definitions.
/// </summary>
public static class EnumHelperExtensions
{
    /// <summary>
    /// Retrieves the <see cref="DescriptionAttribute"/> value of the enum, or falls back to its name.
    /// </summary>
    /// <typeparam name="TEnum">Enum type.</typeparam>
    /// <param name="value">The enum value.</param>
    /// <returns>The description if found; otherwise, the enum name.</returns>
    public static string GetDescription<TEnum>(this TEnum value) where TEnum : struct, Enum
    {
        var type = typeof(TEnum);
        var name = Enum.GetName(type, value);
        if (name == null)
            return string.Empty;

        var field = type.GetField(name);
        if (field == null)
            return name;

        var attr = field.GetCustomAttribute<DescriptionAttribute>();
        return attr?.Description ?? name;
    }

    /// <summary>
    /// Parses a string to the specified enum type. Case-insensitive. Returns a fallback value if parsing fails.
    /// </summary>
    /// <typeparam name="TEnum">Enum type.</typeparam>
    /// <param name="value">The string to parse.</param>
    /// <param name="defaultValue">The fallback value to return if parsing fails.</param>
    /// <returns>The parsed enum value or the default value.</returns>
    public static TEnum ParseOrDefault<TEnum>(this string? value, TEnum defaultValue) where TEnum : struct, Enum
    {
        if (string.IsNullOrWhiteSpace(value))
            return defaultValue;

        return Enum.TryParse(value, true, out TEnum result) ? result : defaultValue;
    }

    /// <summary>
    /// Converts an integer to the specified enum type. Returns a fallback value if not defined in enum.
    /// </summary>
    /// <typeparam name="TEnum">Enum type.</typeparam>
    /// <param name="value">Integer value to convert.</param>
    /// <param name="defaultValue">Fallback value if the integer is not a defined enum value.</param>
    /// <returns>The enum value or fallback.</returns>
    public static TEnum ParseOrDefault<TEnum>(this int value, TEnum defaultValue) where TEnum : struct, Enum
    {
        return Enum.IsDefined(typeof(TEnum), value) ? (TEnum)Enum.ToObject(typeof(TEnum), value) : defaultValue;
    }

    /// <summary>
    /// Retrieves all values of the specified enum type.
    /// </summary>
    /// <typeparam name="TEnum">Enum type.</typeparam>
    /// <param name="_">Unused enum parameter (for extension call pattern).</param>
    /// <returns>List of enum values.</returns>
    public static IEnumerable<TEnum> GetValues<TEnum>(this TEnum _) where TEnum : struct, Enum
    {
        return Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
    }

    /// <summary>
    /// Retrieves all names of the specified enum type.
    /// </summary>
    /// <typeparam name="TEnum">Enum type.</typeparam>
    /// <param name="_">Unused enum parameter (for extension call pattern).</param>
    /// <returns>List of enum names as strings.</returns>
    public static IEnumerable<string> GetNames<TEnum>(this TEnum _) where TEnum : struct, Enum
    {
        return Enum.GetNames(typeof(TEnum));
    }

    /// <summary>
    /// Retrieves all descriptions of the enum values.
    /// </summary>
    /// <typeparam name="TEnum">Enum type.</typeparam>
    /// <param name="_">Unused enum parameter (for extension call pattern).</param>
    /// <returns>List of enum value descriptions.</returns>
    public static IEnumerable<string> GetDescriptions<TEnum>(this TEnum _) where TEnum : struct, Enum
    {
        return GetValues<TEnum>(default).Select(v => v.GetDescription());
    }

    /// <summary>
    /// Checks whether the given integer value is defined in the specified enum type.
    /// </summary>
    /// <typeparam name="TEnum">Enum type.</typeparam>
    /// <param name="value">Integer value to check.</param>
    /// <returns>True if defined; otherwise, false.</returns>
    public static bool IsDefined<TEnum>(this int value) where TEnum : struct, Enum
    {
        return Enum.IsDefined(typeof(TEnum), value);
    }

    /// <summary>
    /// Checks whether the given string represents a valid enum name (case-insensitive).
    /// </summary>
    /// <typeparam name="TEnum">Enum type.</typeparam>
    /// <param name="value">String to check.</param>
    /// <returns>True if valid; otherwise, false.</returns>
    public static bool IsDefined<TEnum>(this string value) where TEnum : struct, Enum
    {
        return Enum.TryParse(value, true, out TEnum _);
    }
}
