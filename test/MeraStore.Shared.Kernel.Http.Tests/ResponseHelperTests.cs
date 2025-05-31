using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

namespace MeraStore.Shared.Kernel.Http.Tests;

public class ResponseHelperTests
{
  [Fact]
  public async Task GetResponseOrFault_Should_Return_ApiResponse_When_ValidJsonObject()
  {
    // Arrange
    var testObject = new { Name = "Boss", Age = 99 };
    var json = JsonConvert.SerializeObject(testObject);
    var response = new HttpResponseMessage(HttpStatusCode.OK)
    {
      Content = new StringContent(json, Encoding.UTF8, "application/json")
    };

    // Act
    var result = await response.GetResponseOrFault<dynamic>();

    // Assert
    Assert.Equal(200, result.StatusCode);
    Assert.NotNull(result.Response);
    Assert.Null(result.ErrorInfo);
  }

  [Fact]
  public async Task GetResponseOrFault_Should_Return_ApiResponse_When_String()
  {
    var content = "Simple text response.";
    var response = new HttpResponseMessage(HttpStatusCode.OK)
    {
      Content = new StringContent(content)
    };

    var result = await response.GetResponseOrFault<string>();

    Assert.Equal(200, result.StatusCode);
    Assert.Equal(content, result.Response);
  }

  [Fact]
  public async Task GetResponseOrFault_Should_Return_ApiResponse_When_PrimitiveType()
  {
    var number = "42";
    var response = new HttpResponseMessage(HttpStatusCode.OK)
    {
      Content = new StringContent(number)
    };

    var result = await response.GetResponseOrFault<int>();

    Assert.Equal(200, result.StatusCode);
    Assert.Equal(42, result.Response);
  }

  [Fact]
  public async Task GetResponseOrFault_Should_Return_ProblemDetails_When_Error_With_Valid_Json()
  {
    var error = new ProblemDetails
    {
      Title = "Not Found",
      Detail = "Item doesn't exist.",
      Status = 404
    };

    var json = JsonConvert.SerializeObject(error);
    var response = new HttpResponseMessage(HttpStatusCode.NotFound)
    {
      Content = new StringContent(json, Encoding.UTF8, "application/json")
    };

    var result = await response.GetResponseOrFault<object>();

    Assert.Equal(404, result.StatusCode);
    Assert.NotNull(result.ErrorInfo);
    Assert.Equal("Not Found", result.ErrorInfo?.Title);
  }

  [Fact]
  public async Task GetResponseOrFault_Should_Return_DeserializationError_When_BadJson()
  {
    var badJson = "{ invalid_json ";
    var response = new HttpResponseMessage(HttpStatusCode.OK)
    {
      Content = new StringContent(badJson, Encoding.UTF8, "application/json")
    };

    var result = await response.GetResponseOrFault<object>();

    Assert.Equal(200, result.StatusCode);
    Assert.Null(result.Response);
    Assert.NotNull(result.ErrorInfo);
    Assert.Equal("Deserialization Error", result.ErrorInfo?.Title);
  }

  [Fact]
  public async Task GetResponseOrFault_Should_Return_NoContent_When_Empty()
  {
    var response = new HttpResponseMessage(HttpStatusCode.NoContent)
    {
      Content = new StringContent("")
    };

    var result = await response.GetResponseOrFault<object>();

    Assert.Equal(204, result.StatusCode);
    Assert.Null(result.Response);
    Assert.Equal("No Content", result.ErrorInfo?.Title);
  }
}
