using MeraStore.Shared.Kernel.Logging.Interfaces;

namespace MeraStore.Shared.Kernel.Logging.Masking;

public class EmailMasker : IMask
{
  public string Mask(string input)
  {
    if (string.IsNullOrEmpty(input) || !input.Contains("@")) return input;

    var parts = input.Split('@');
    if (parts.Length != 2) return input;

    var username = parts[0];
    var domain = parts[1];

    var maskedUsername = username.Length > 2
      ? username[0] + new string('*', username.Length - 2) + username[^1]
      : new string('*', username.Length);

    return maskedUsername + "@" + domain;
  }
}