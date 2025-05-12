namespace MeraStore.Shared.Kernel.Logging.Interfaces;

public interface IMaskingFieldFilter
{
  IEnumerable<string> GetMaskedFields();
  void AddField(string fieldName, IMask masker = null);
}