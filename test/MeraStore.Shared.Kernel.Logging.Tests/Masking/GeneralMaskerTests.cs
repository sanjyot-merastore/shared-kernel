using MeraStore.Shared.Kernel.Logging.Masking;

namespace MeraStore.Shared.Kernel.Logging.Tests.Masking;

public class GeneralMaskerTests
{
    private readonly GeneralMasker _masker = new();

    [Theory]
    [InlineData(null, null)]
    [InlineData("", "")]
    [InlineData("a", "a")]
    [InlineData("to", "to")]
    public void Mask_ShouldReturnInput_WhenNullOrShortWords(string input, string expected)
    {
        var result = _masker.Mask(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("hello", "h***o")]
    [InlineData("mask", "m**k")]
    [InlineData("general", "g*****l")]
    public void Mask_ShouldMaskLongWords(string input, string expected)
    {
        var result = _masker.Mask(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Mask_ShouldMaskEachWordIndependently()
    {
        var input = "hello world to you";
        var expected = "h***o w***d to y*u";

        var result = _masker.Mask(input);

        Assert.Equal(expected, result);
    }
}