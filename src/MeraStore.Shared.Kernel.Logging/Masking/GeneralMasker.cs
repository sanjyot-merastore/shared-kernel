using MeraStore.Shared.Kernel.Logging.Interfaces;

namespace MeraStore.Shared.Kernel.Logging.Masking;

public class GeneralMasker : IMask
{
  public string Mask(string input)
  {
    if (string.IsNullOrEmpty(input)) return input;

    var words = input.Split(' ');
    for (int i = 0; i < words.Length; i++)
    {
      words[i] = MaskWord(words[i]);
    }

    return string.Join(' ', words);
  }

  private static string MaskWord(string word)
  {
    return word.Length <= 2 ? word : word[0] + new string('*', word.Length - 2) + word[^1];

  }
}