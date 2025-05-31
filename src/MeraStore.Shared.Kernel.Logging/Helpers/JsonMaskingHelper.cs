using MeraStore.Shared.Kernel.Logging.Interfaces;
using Newtonsoft.Json.Linq;

namespace MeraStore.Shared.Kernel.Logging.Helpers;

public static class JsonMaskingHelper
{
    public static string MaskJson(string json, string fieldName, IMask masker)
    {
        if (string.IsNullOrWhiteSpace(json)) return json;

        try
        {
            var token = JToken.Parse(json);

            if (token is JObject obj)
            {
                ApplyMask(obj, fieldName, masker);
            }
            else if (token is JArray array)
            {
                foreach (var item in array)
                {
                    if (item is JObject childObj)
                    {
                        ApplyMask(childObj, fieldName, masker);
                    }
                }
            }

            return token.ToString();
        }
        catch (Exception ex)
        {
            // Optionally log the error
            return json; // Return unmodified if masking fails
        }
    }

    private static void ApplyMask(JObject obj, string fieldName, IMask masker)
    {
        var token = obj.SelectToken(fieldName);
        if (token is { Type: JTokenType.String })
        {
            token.Replace(masker.Mask(token.ToString()));
        }
    }
}