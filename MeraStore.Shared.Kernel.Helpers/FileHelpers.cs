namespace MeraStore.Shared.Kernel.Helpers;

public static class FileHelpers
{
    /// <summary>
    /// Reads all text from a file asynchronously with retries to handle file locks or access issues.
    /// </summary>
    /// <param name="path">The file path to read from.</param>
    /// <param name="maxRetries">Maximum number of retry attempts if the file is locked or inaccessible. Default is 5.</param>
    /// <param name="delayMilliseconds">Initial delay between retries in milliseconds. Exponential backoff is applied. Default is 200ms.</param>
    /// <returns>The file content as a string.</returns>
    /// <exception cref="IOException">Throws if all retry attempts fail due to file locks or access issues.</exception>
    public static async Task<string> ReadAllTextAsync(string path, int maxRetries = 5, int delayMilliseconds = 200)
    {
        var tries = 0;
        Exception? lastException = null;

        while (tries < maxRetries)
        {
            try
            {
                using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                using var reader = new StreamReader(stream);
                return await reader.ReadToEndAsync();
            }
            catch (IOException ex)
            {
                lastException = ex;
            }
            catch (UnauthorizedAccessException ex)
            {
                lastException = ex;
            }

            tries++;
            await Task.Delay(delayMilliseconds * tries); // Exponential backoff
        }

        throw new IOException($"Failed to read file '{path}' after {maxRetries} retries due to file lock or access issues.", lastException);
    }

    /// <summary>
    /// Writes all text to a file asynchronously, overwriting if the file exists.
    /// </summary>
    /// <param name="path">The file path to write to.</param>
    /// <param name="content">The content to write.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task WriteAllTextAsync(string path, string content)
    {
        using var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
        using var writer = new StreamWriter(stream);
        await writer.WriteAsync(content);
    }

    /// <summary>
    /// Gets the file extension including the leading dot, or empty string if none.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <returns>The file extension with leading dot, or empty string.</returns>
    public static string GetFileExtension(string filePath)
    {
        return Path.GetExtension(filePath) ?? string.Empty;
    }
    
}