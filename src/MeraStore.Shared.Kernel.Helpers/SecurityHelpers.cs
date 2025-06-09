using System.Security.Cryptography;

namespace MeraStore.Shared.Kernel.Helpers;

public static class SecurityHelpers
{
    /// <summary>
    /// Generates a cryptographically secure random string of specified length (default 32).
    /// </summary>
    public static string GenerateSecureRandomString(int length = 32)
    {
        if (length <= 0) throw new ArgumentOutOfRangeException(nameof(length));

        var bytes = new byte[length];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);

        // Base64 encode and trim to requested length safely
        var base64 = Convert.ToBase64String(bytes)
            .Replace("+", "")
            .Replace("/", "")
            .Replace("=", "");

        return base64.Length > length ? base64[..length] : base64;
    }

    /// <summary>
    /// Computes SHA256 hash of input string and returns hex representation.
    /// </summary>
    public static string ComputeSha256Hash(string input)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = sha256.ComputeHash(bytes);
        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
    }

    /// <summary>
    /// Compares two strings in constant time to prevent timing attacks.
    /// </summary>
    public static bool ConstantTimeEquals(string a, string b)
    {
        if (a == null || b == null || a.Length != b.Length)
            return false;

        var result = 0;
        for (var i = 0; i < a.Length; i++)
            result |= a[i] ^ b[i];

        return result == 0;
    }
}