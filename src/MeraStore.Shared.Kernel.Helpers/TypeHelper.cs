using System.Collections.Concurrent;

namespace MeraStore.Shared.Kernel.Helpers;

/// <summary>
/// Provides helper methods for type inspection, conversions, and default value handling.
/// </summary>
public static class TypeHelper
{
    private static readonly ConcurrentDictionary<Type, object?> DefaultValuesCache = new();

    /// <summary>
    /// Gets the default value for a given <see cref="Type"/>.
    /// For reference types, returns null; for value types, returns default(T).
    /// </summary>
    /// <param name="type">The type to get the default value for.</param>
    /// <returns>The default value of the specified type.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="type"/> is null.</exception>
    public static object? GetDefaultValue(Type type)
    {
        if (type == null) throw new ArgumentNullException(nameof(type));

        return DefaultValuesCache.GetOrAdd(type, t => t.IsValueType ? Activator.CreateInstance(t) : null);
    }

    /// <summary>
    /// Gets the default value for the generic type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type to get the default value for.</typeparam>
    /// <returns>The default value of type <typeparamref name="T"/>.</returns>
    public static T GetDefaultValue<T>()
    {
        return default!;
    }

    /// <summary>
    /// Checks if the specified type is nullable (Nullable&lt;T&gt;) or a reference type.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns><c>true</c> if the type is nullable or a reference type; otherwise, <c>false</c>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="type"/> is null.</exception>
    public static bool IsNullable(Type type)
    {
        if (type == null) throw new ArgumentNullException(nameof(type));

        return !type.IsValueType || Nullable.GetUnderlyingType(type) != null;
    }

    /// <summary>
    /// Attempts to convert the specified value to the target type, supporting nullable types and
    /// type converters.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="targetType">The type to convert to.</param>
    /// <param name="result">The converted value, or null if conversion failed.</param>
    /// <returns><c>true</c> if the conversion succeeded; otherwise, <c>false</c>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="targetType"/> is null.</exception>
    public static bool TryChangeType(object? value, Type targetType, out object? result)
    {
        if (targetType == null) throw new ArgumentNullException(nameof(targetType));

        result = null;

        if (value == null)
        {
            // Null can be assigned only if targetType is nullable or reference type
            if (IsNullable(targetType))
            {
                result = null;
                return true;
            }
            return false;
        }

        var underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;

        try
        {
            // If value already matches the target type, return directly
            if (underlyingType.IsInstanceOfType(value))
            {
                result = value;
                return true;
            }

            // Handle enums (from string or underlying type)
            if (underlyingType.IsEnum)
            {
                if (value is string s)
                {
                    if (Enum.TryParse(underlyingType, s, ignoreCase: true, out var enumValue))
                    {
                        result = enumValue;
                        return true;
                    }
                    return false;
                }
                else
                {
                    try
                    {
                        var converted = Convert.ChangeType(value, Enum.GetUnderlyingType(underlyingType));
                        result = Enum.ToObject(underlyingType, converted);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }

            // Use TypeConverter as fallback for conversion
            var converter = TypeDescriptor.GetConverter(underlyingType);
            if (converter.CanConvertFrom(value.GetType()))
            {
                result = converter.ConvertFrom(value);
                return true;
            }

            // Try direct ChangeType (for primitive types)
            result = Convert.ChangeType(value, underlyingType);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Converts the specified value to the target type, throwing an exception if conversion fails.
    /// </summary>
    /// <typeparam name="T">Target type.</typeparam>
    /// <param name="value">Value to convert.</param>
    /// <returns>Converted value of type <typeparamref name="T"/>.</returns>
    /// <exception cref="InvalidCastException">If conversion fails.</exception>
    public static T ChangeType<T>(object? value)
    {
        if (TryChangeType(value, typeof(T), out var result) && result is T typedResult)
            return typedResult;

        throw new InvalidCastException($"Failed to convert value '{value}' to type '{typeof(T).FullName}'.");
    }

    /// <summary>
    /// Checks if the specified type is numeric (int, double, float, decimal, long, short, byte, etc.).
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns><c>true</c> if the type is numeric; otherwise, <c>false</c>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="type"/> is null.</exception>
    public static bool IsNumeric(Type type)
    {
        if (type == null) throw new ArgumentNullException(nameof(type));

        var underlyingType = Nullable.GetUnderlyingType(type) ?? type;

        return underlyingType == typeof(byte) || underlyingType == typeof(sbyte) ||
               underlyingType == typeof(short) || underlyingType == typeof(ushort) ||
               underlyingType == typeof(int) || underlyingType == typeof(uint) ||
               underlyingType == typeof(long) || underlyingType == typeof(ulong) ||
               underlyingType == typeof(float) || underlyingType == typeof(double) ||
               underlyingType == typeof(decimal);
    }

    /// <summary>
    /// Gets the type name, optionally including the namespace.
    /// </summary>
    /// <param name="type">The type to get the name of.</param>
    /// <param name="includeNamespace">If true, returns the full name including namespace; otherwise, just the type name.</param>
    /// <returns>The type name, optionally including namespace.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="type"/> is null.</exception>
    public static string? GetTypeName(Type type, bool includeNamespace = false)
    {
        return type == null ? throw new ArgumentNullException(nameof(type)) :
            includeNamespace ? type.FullName : type.Name;
    }

}