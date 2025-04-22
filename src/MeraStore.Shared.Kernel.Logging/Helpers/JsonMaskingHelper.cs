using MeraStore.Shared.Kernel.Logging.Interfaces;
using Newtonsoft.Json.Linq;

namespace MeraStore.Shared.Kernel.Logging.Helpers;
public static class JsonMaskingHelper
{
  public static string MaskJson(string json, string fieldName, IMask masker)
  {
    var jsonObject = JObject.Parse(json);
    var token = jsonObject.SelectToken(fieldName);
    token?.Replace(masker.Mask(token.ToString()));  // Mask the value using the provided masker

    return jsonObject.ToString();  // Return the modified JSON
  }
}