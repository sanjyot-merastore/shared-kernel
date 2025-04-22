using System.Text;
using MeraStore.Shared.Kernel.Logging.Helpers;
using MeraStore.Shared.Kernel.Logging.Interfaces;
using MeraStore.Shared.Kernel.Logging.Masking;

namespace MeraStore.Shared.Kernel.Logging.Filters
{
  public class MaskingFilter(IEnumerable<IMaskingFieldFilter> requestFilters, IEnumerable<IMaskingFieldFilter> responseFilters)
    : IMaskingFilter
  {
    // Constructor accepts IEnumerable of filters, allowing multiple filter registrations

    // Mask request payload based on added filters
    public byte[] MaskRequestPayload(byte[] payload, string contentType = "application/json")
    {
      string jsonPayload = Encoding.UTF8.GetString(payload);

      // Apply each request filter to the payload
      jsonPayload = requestFilters.Aggregate(jsonPayload, (current1, filter) =>
        filter.GetMaskedFields().Aggregate(current1, (current, field) =>
          JsonMaskingHelper.MaskJson(current, field, new GeneralMasker())));

      return Encoding.UTF8.GetBytes(jsonPayload);
    }

    // Mask response payload based on added filters
    public byte[] MaskResponsePayload(byte[] payload)
    {
      string jsonPayload = Encoding.UTF8.GetString(payload);

      // Apply each response filter to the payload
      jsonPayload = responseFilters.SelectMany(filter => filter.GetMaskedFields())
        .Aggregate(jsonPayload, (current, field) =>
          JsonMaskingHelper.MaskJson(current, field, new GeneralMasker()));

      return Encoding.UTF8.GetBytes(jsonPayload);
    }
  }
}