using MeraStore.Shared.Kernel.Logging.Masking;

namespace MeraStore.Shared.Kernel.Logging.Tests.Masking;

public class CreditCardMaskerTests
{
    private readonly CreditCardMasker _masker = new();

    [Theory]
    [InlineData(null, null)]
    [InlineData("", "")]
    [InlineData("123", "123")] // less than 4 digits
    public void Mask_ShouldReturnInput_WhenNullOrTooShort(string input, string expected)
    {
        var result = _masker.Mask(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Mask_ShouldMaskAllButLast4Digits()
    {
        var input = "1234567812345678";
        var expected = "************5678";

        var result = _masker.Mask(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Mask_ShouldPreserveNonDigitCharacters()
    {
        var input = "Card: 1234-5678-9876-5432";
        var expected = "Card: ****-****-****-5432";

        var result = _masker.Mask(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Mask_ShouldHandleWhitespaceAndSymbols()
    {
        var input = "CC  4111 1111 1111 1111";
        var expected = "CC  **** **** **** 1111";

        var result = _masker.Mask(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Mask_ShouldHandleOnlyDigitsWithSpaces()
    {
        var input = "4111 2222 3333 4444";
        var expected = "**** **** **** 4444";

        var result = _masker.Mask(input);

        Assert.Equal(expected, result);
    }
}