using MeraStore.Shared.Kernel.Logging.Interfaces;

namespace MeraStore.Shared.Kernel.Logging.Masking;

public class CreditCardMasker : IMask
{
  public string Mask(string input)
  {
    if (string.IsNullOrEmpty(input)) return input;

    var digitsOnly = new string(input.Where(char.IsDigit).ToArray());
    if (digitsOnly.Length < 4) return input;

    var maskedDigits = new string('*', digitsOnly.Length - 4) + digitsOnly[^4..];

    return ReinsertOriginalCharacters(input, maskedDigits);
  }

  private string ReinsertOriginalCharacters(string original, string maskedDigits)
  {
    int digitIndex = 0;
    return new string(original.Select(c => char.IsDigit(c) ? maskedDigits[digitIndex++] : c).ToArray());
  }
}