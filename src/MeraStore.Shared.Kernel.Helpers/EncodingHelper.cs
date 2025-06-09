using System.Net;

namespace MeraStore.Shared.Kernel.Helpers.MeraStore.Shared.Kernel.Helpers;

/// <summary>
/// Provides helper methods for encoding and decoding operations, including Base64 and URL encoding.
/// </summary>
public static class EncodingHelper
{
    /// <summary>
    /// Encodes a string to Base64 using the specified encoding (defaults to UTF-8).
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <param name="encoding">The encoding to use. Defaults to UTF-8.</param>
    /// <returns>Base64-encoded string.</returns>
    public static string ToBase64(this string input, Encoding? encoding = null)
    {
        if (string.IsNullOrEmpty(input)) return string.Empty;
        var enc = encoding ?? Encoding.UTF8;
        var bytes = enc.GetBytes(input);
        return Convert.ToBase64String(bytes);
    }

    /// <summary>
    /// Decodes a Base64 string to plain text using the specified encoding (defaults to UTF-8).
    /// </summary>
    /// <param name="base64Encoded">The Base64 encoded string.</param>
    /// <param name="encoding">The encoding to use. Defaults to UTF-8.</param>
    /// <returns>The decoded string, or empty string if decoding fails.</returns>
    public static string FromBase64(this string base64Encoded, Encoding? encoding = null)
    {
        if (string.IsNullOrWhiteSpace(base64Encoded)) return string.Empty;

        try
        {
            var enc = encoding ?? Encoding.UTF8;
            var bytes = Convert.FromBase64String(base64Encoded);
            return enc.GetString(bytes);
        }
        catch
        {
            return string.Empty;
        }
    }

    /// <summary>
    /// Encodes a string to a URL-safe format.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <returns>URL-encoded string.</returns>
    public static string UrlEncode(this string input)
    {
        return string.IsNullOrEmpty(input) ? string.Empty : WebUtility.UrlEncode(input);
    }

    /// <summary>
    /// Decodes a URL-encoded string to its original format.
    /// </summary>
    /// <param name="input">The URL-encoded string.</param>
    /// <returns>Decoded string.</returns>
    public static string UrlDecode(this string input)
    {
        return string.IsNullOrEmpty(input) ? string.Empty : WebUtility.UrlDecode(input);
    }

    /// <summary>
    /// Encodes a byte array to Base64 string.
    /// </summary>
    /// <param name="bytes">The byte array.</param>
    /// <returns>Base64-encoded string.</returns>
    public static string ToBase64(this byte[] bytes)
    {
        return bytes == null || bytes.Length == 0 ? string.Empty : Convert.ToBase64String(bytes);
    }

    /// <summary>
    /// Decodes a Base64 string to byte array.
    /// </summary>
    /// <param name="base64Encoded">The Base64 encoded string.</param>
    /// <returns>Decoded byte array, or empty array if decoding fails.</returns>
    public static byte[] FromBase64ToBytes(this string base64Encoded)
    {
        if (string.IsNullOrWhiteSpace(base64Encoded)) return [];

        try
        {
            return Convert.FromBase64String(base64Encoded);
        }
        catch
        {
            return [];
        }
    }

    /// <summary>
    /// Converts a string to a byte array using the specified encoding (defaults to UTF-8).
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <param name="encoding">The encoding to use. Defaults to UTF-8.</param>
    /// <returns>Byte array representation.</returns>
    public static byte[] ToBytes(this string input, Encoding? encoding = null)
    {
        var enc = encoding ?? Encoding.UTF8;
        return enc.GetBytes(input);
    }

    /// <summary>
    /// Converts a byte array to string using the specified encoding (defaults to UTF-8).
    /// </summary>
    /// <param name="bytes">The byte array.</param>
    /// <param name="encoding">The encoding to use. Defaults to UTF-8.</param>
    /// <returns>Decoded string.</returns>
    public static string ToStringFromBytes(this byte[] bytes, Encoding? encoding = null)
    {
        if (bytes == null || bytes.Length == 0) return string.Empty;
        var enc = encoding ?? Encoding.UTF8;
        return enc.GetString(bytes);
    }
}