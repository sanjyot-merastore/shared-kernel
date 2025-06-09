namespace MeraStore.Shared.Kernel.Helpers;

/// <summary>
/// Provides helper methods and extensions for working with <see cref="Stream"/> objects,
/// including reading, writing, and converting streams efficiently.
/// </summary>
public static class StreamHelper
{
    /// <summary>
    /// Reads the entire stream asynchronously and returns its content as a byte array.
    /// </summary>
    /// <param name="stream">The input stream to read.</param>
    /// <returns>A <see cref="byte"/> array containing the stream content.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is null.</exception>
    public static async Task<byte[]> ToByteArrayAsync(this Stream stream)
    {
        if (stream == null) throw new ArgumentNullException(nameof(stream));

        if (stream is MemoryStream ms && ms.TryGetBuffer(out var buffer))
        {
            // If it's a MemoryStream, return the internal buffer directly for performance
            return buffer.Array ?? await ReadStreamFullyAsync(stream);
        }

        return await ReadStreamFullyAsync(stream);

        static async Task<byte[]> ReadStreamFullyAsync(Stream s)
        {
            using var memoryStream = new MemoryStream();
            await s.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }

    /// <summary>
    /// Reads the entire stream synchronously and returns its content as a byte array.
    /// </summary>
    /// <param name="stream">The input stream to read.</param>
    /// <returns>A <see cref="byte"/> array containing the stream content.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is null.</exception>
    public static byte[] ToByteArray(this Stream stream)
    {
        if (stream == null) throw new ArgumentNullException(nameof(stream));

        if (stream is MemoryStream ms && ms.TryGetBuffer(out var buffer))
        {
            return buffer.Array ?? ReadStreamFully(stream);
        }

        return ReadStreamFully(stream);

        static byte[] ReadStreamFully(Stream s)
        {
            using var memoryStream = new MemoryStream();
            s.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }

    /// <summary>
    /// Reads the entire stream asynchronously and returns its content as a UTF8 string.
    /// </summary>
    /// <param name="stream">The input stream to read.</param>
    /// <returns>A string representing the UTF8-decoded content of the stream.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is null.</exception>
    public static async Task<string> ToUtf8StringAsync(this Stream stream)
    {
        var bytes = await ToByteArrayAsync(stream);
        return Encoding.UTF8.GetString(bytes);
    }

    /// <summary>
    /// Reads the entire stream synchronously and returns its content as a UTF8 string.
    /// </summary>
    /// <param name="stream">The input stream to read.</param>
    /// <returns>A string representing the UTF8-decoded content of the stream.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is null.</exception>
    public static string ToUtf8String(this Stream stream)
    {
        var bytes = ToByteArray(stream);
        return Encoding.UTF8.GetString(bytes);
    }

    /// <summary>
    /// Writes the specified byte array asynchronously to the stream.
    /// </summary>
    /// <param name="stream">The output stream to write to.</param>
    /// <param name="bytes">The byte array to write.</param>
    /// <returns>A task representing the asynchronous write operation.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> or <paramref name="bytes"/> is null.</exception>
    public static async Task WriteBytesAsync(this Stream stream, byte[] bytes)
    {
        if (stream == null) throw new ArgumentNullException(nameof(stream));
        if (bytes == null) throw new ArgumentNullException(nameof(bytes));

        await stream.WriteAsync(bytes, 0, bytes.Length);
    }

    /// <summary>
    /// Writes the specified string asynchronously to the stream using UTF8 encoding.
    /// </summary>
    /// <param name="stream">The output stream to write to.</param>
    /// <param name="content">The string content to write.</param>
    /// <returns>A task representing the asynchronous write operation.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> or <paramref name="content"/> is null.</exception>
    public static async Task WriteStringAsync(this Stream stream, string content)
    {
        if (stream == null) throw new ArgumentNullException(nameof(stream));
        if (content == null) throw new ArgumentNullException(nameof(content));

        var bytes = Encoding.UTF8.GetBytes(content);
        await stream.WriteBytesAsync(bytes);
    }

    /// <summary>
    /// Copies the source stream asynchronously into a new memory stream and resets its position to 0.
    /// </summary>
    /// <param name="source">The source stream to copy from.</param>
    /// <returns>A new <see cref="MemoryStream"/> containing a copy of the source stream's data.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="source"/> is null.</exception>
    public static async Task<MemoryStream> ToMemoryStreamAsync(this Stream source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));

        var memoryStream = new MemoryStream();
        await source.CopyToAsync(memoryStream);
        memoryStream.Position = 0;
        return memoryStream;
    }

    /// <summary>
    /// Copies the source stream synchronously into a new memory stream and resets its position to 0.
    /// </summary>
    /// <param name="source">The source stream to copy from.</param>
    /// <returns>A new <see cref="MemoryStream"/> containing a copy of the source stream's data.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="source"/> is null.</exception>
    public static MemoryStream ToMemoryStream(this Stream source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));

        var memoryStream = new MemoryStream();
        source.CopyTo(memoryStream);
        memoryStream.Position = 0;
        return memoryStream;
    }
}