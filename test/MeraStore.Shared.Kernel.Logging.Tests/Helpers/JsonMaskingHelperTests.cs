using MeraStore.Shared.Kernel.Logging.Helpers;
using MeraStore.Shared.Kernel.Logging.Interfaces;
using MeraStore.Shared.Kernel.Logging.Masking;
using Moq;
using Newtonsoft.Json.Linq;

namespace MeraStore.Shared.Kernel.Logging.Tests.Helpers;

public class JsonMaskingHelperTests
{
    private readonly IMask _maskerMock = new GeneralMasker();

    [Fact]
    public void MaskJson_WithNullOrWhitespace_ReturnsInput()
    {
        Assert.Null(JsonMaskingHelper.MaskJson(null, "password", _maskerMock));
        Assert.Equal("", JsonMaskingHelper.MaskJson("", "", _maskerMock));
    }

    
    [Fact]
    public void MaskJson_FieldNotFound_ReturnsUnchanged()
    {
        string json = @"{ ""username"": ""user1"", ""email"": ""user@example.com"" }";

        var maskedJson = JsonMaskingHelper.MaskJson(json, "password", _maskerMock);

        // No password field, JSON remains unchanged except formatting
        var original = JObject.Parse(json);
        var masked = JObject.Parse(maskedJson);
        Assert.True(JToken.DeepEquals(original, masked));
    }

    [Fact]
    public void MaskJson_InvalidJson_ReturnsOriginal()
    {
        string invalidJson = "{ this is not valid json }";

        var result = JsonMaskingHelper.MaskJson(invalidJson, "password", _maskerMock);

        Assert.Equal(invalidJson, result);
    }
}