using MeraStore.Shared.Kernel.Logging.Filters;
using MeraStore.Shared.Kernel.Logging.Interfaces;

namespace MeraStore.Shared.Kernel.Logging.Helpers;

public static class DefaultMaskingFilter
{
  /// <summary>
  /// Returns a default masking filter instance with standard sensitive fields masked.
  /// </summary>
  public static IMaskingFilter Get()
  {
    // Sensitive fields to mask in request payloads
    var requestFilter = new JsonPayloadRequestFilter();
    requestFilter.AddField("password");
    requestFilter.AddField("confirmPassword");
    requestFilter.AddField("accessToken");
    requestFilter.AddField("refreshToken");
    requestFilter.AddField("creditCardNumber");
    requestFilter.AddField("cvv");
    requestFilter.AddField("ssn");
    requestFilter.AddField("pin");
    requestFilter.AddField("securityCode");
    requestFilter.AddField("secretKey");
    requestFilter.AddField("apiKey");

    // Sensitive fields to mask in response payloads
    var responseFilter = new JsonPayloadResponseFilter();
    responseFilter.AddField("password");
    responseFilter.AddField("creditCardNumber");
    responseFilter.AddField("ssn");
    responseFilter.AddField("accessToken");
    responseFilter.AddField("refreshToken");
    responseFilter.AddField("secretKey");
    responseFilter.AddField("apiKey");
    responseFilter.AddField("internalNotes");
    responseFilter.AddField("summary"); // e.g. order or account summaries
    responseFilter.AddField("jwt");

    return new MaskingFilter([requestFilter], [responseFilter]);
  }
}
