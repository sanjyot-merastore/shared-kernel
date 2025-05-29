#nullable enable
using MeraStore.Shared.Kernel.Common.Core;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace MeraStore.Shared.Kernel.Http;

public static class ResponseHelper
{
  private static readonly JsonSerializerSettings JsonOptions = new()
  {
    NullValueHandling = NullValueHandling.Ignore,
    Formatting = Formatting.Indented,
    ContractResolver = new CamelCasePropertyNamesContractResolver(),
    Converters = { new StringEnumConverter() }
  };

  public static async Task<ApiResponse<T>> GetResponseOrFault<T>(this HttpResponseMessage response)
  {
    var content = await response.Content.ReadAsStringAsync();
    var statusCode = (int)response.StatusCode;

    return response.IsSuccessStatusCode
        ? HandleSuccessResponse<T>(content, statusCode)
        : HandleErrorResponse<T>(content, statusCode);
  }

  private static ApiResponse<T> HandleSuccessResponse<T>(string content, int statusCode)
  {
    if (string.IsNullOrWhiteSpace(content))
      return CreateNoContentResponse<T>(statusCode);

    try
    {
      var responseData = DeserializeContent<T>(content);
      return new ApiResponse<T>
      {
        Response = responseData,
        StatusCode = statusCode
      };
    }
    catch (JsonException ex)
    {
      return CreateErrorResponse<T>("Deserialization Error", ex.Message, statusCode);
    }
  }

  private static ApiResponse<T> HandleErrorResponse<T>(string content, int statusCode)
  {
    if (string.IsNullOrWhiteSpace(content))
      return CreateNoContentResponse<T>(statusCode);

    try
    {
      var errorDetails = JsonConvert.DeserializeObject<ProblemDetails>(content, JsonOptions);
      return new ApiResponse<T>
      {
        StatusCode = statusCode,
        ErrorInfo = errorDetails
      };
    }
    catch (JsonException ex)
    {
      return CreateErrorResponse<T>("Error Response Parsing Failed", ex.Message, statusCode);
    }
  }

  private static T? DeserializeContent<T>(string content)
  {
    return typeof(T) switch
    {
      var t when t == typeof(string) => (T)(object)content,
      var t when t.IsPrimitive || t == typeof(decimal) => (T)Convert.ChangeType(content, typeof(T))!,
      _ => JsonConvert.DeserializeObject<T>(content, JsonOptions)
    };
  }

  private static ApiResponse<T> CreateErrorResponse<T>(string title, string detail, int statusCode) =>
      new()
      {
        StatusCode = statusCode,
        ErrorInfo = new ProblemDetails
        {
          Title = title,
          Detail = detail,
          Status = statusCode
        }
      };

  private static ApiResponse<T> CreateNoContentResponse<T>(int statusCode) =>
      new()
      {
        StatusCode = statusCode,
        ErrorInfo = new ProblemDetails
        {
          Title = "No Content",
          Detail = "The API response did not contain any content.",
          Status = statusCode
        }
      };
}
