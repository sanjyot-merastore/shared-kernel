using MeraStore.Shared.Kernel.Logging.Interfaces;
using MeraStore.Shared.Kernel.Logging.Masking;

namespace MeraStore.Shared.Kernel.Logging.Filters;

public class JsonPayloadResponseFilter : IMaskingFieldFilter
{
  private readonly List<KeyValuePair<string, IMask>> _fields = [];

  public void AddField(string fieldName, IMask masker = null)
  {
    _fields.Add(new KeyValuePair<string, IMask>(fieldName, masker ?? new GeneralMasker()));
   
  }

  public IEnumerable<string> GetMaskedFields()
  {
    return _fields.Select(f => f.Key);
  }
}