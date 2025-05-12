using System.Text;

namespace MeraStore.Shared.Kernel.Logging;

public class Payload
{
  // Raw byte[] for payload storage
  private byte[] _data;

  // Func to lazily generate byte array (if provided)
  private readonly Func<byte[]> _dataGenerator;

  // Property to get byte[] value either directly or from Func
  public byte[] Data
  {
    get
    {
      if (_data == null && _dataGenerator != null)
      {
        // Generate the byte array using the provided function
        _data = _dataGenerator();
      }
      return _data;
    }
  }

  // Constructor to initialize from byte array
  public Payload(byte[] data)
  {
    _data = data ?? throw new ArgumentNullException(nameof(data), "Data cannot be null.");
  }

  // Constructor to initialize using Func<byte[]> for lazy loading of data
  public Payload(Func<byte[]> dataGenerator)
  {
    _dataGenerator = dataGenerator ?? throw new ArgumentNullException(nameof(dataGenerator), "Data generator function cannot be null.");
  }

  // Constructor that takes byte[] and Encoding to convert data to the desired byte array
  public Payload(string data, Encoding encoding = null)
  {
    encoding ??= Encoding.UTF8; // Default to UTF-8 if encoding is not provided
    _data = encoding.GetBytes(data);
  }

  // Method to convert byte array to string using provided encoding
  public string GetString(Encoding encoding = null)
  {
    encoding ??= Encoding.UTF8; // Default to UTF-8 if encoding is not provided
    return encoding.GetString(Data);
  }

  // Method to get the size of the payload in bytes
  public int GetSizeInBytes()
  {
    return Data.Length;
  }

  // Method to check if payload is empty
  public bool IsEmpty()
  {
    return Data.Length == 0;
  }

  // Override ToString for easier logging and debugging (using UTF-8 encoding by default)
  public override string ToString()
  {
    return GetString(Encoding.UTF8);
  }
}