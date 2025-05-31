using MeraStore.Shared.Kernel.Logging.Masking;

namespace MeraStore.Shared.Kernel.Logging.Tests.Masking;

public class PhoneNumberMaskerTests
{
    private readonly PhoneNumberMasker _masker = new();

    [Theory]
    [InlineData(null, null)]
    [InlineData("", "")]
    [InlineData("123", "123")] // less than 4 digits, no mask
    public void Mask_ShouldReturnInput_WhenNullOrTooShort(string input, string expected)
    {
        var result = _masker.Mask(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Mask_ShouldMaskAllButLast4Digits()
    {
        var input = "1234567890";
        var expected = "******7890";

        var result = _masker.Mask(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Mask_ShouldPreserveFormatting()
    {
        var input = "+1 (234) 567-8901";
        var expected = "+* (***) ***-8901";

        var result = _masker.Mask(input);

        Assert.Equal(expected, result);
    }
}