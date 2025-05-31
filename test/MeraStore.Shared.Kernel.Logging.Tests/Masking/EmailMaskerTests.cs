using MeraStore.Shared.Kernel.Logging.Masking;

namespace MeraStore.Shared.Kernel.Logging.Tests.Masking;

public class EmailMaskerTests
{
    private readonly EmailMasker _masker = new();

    [Theory]
    [InlineData(null, null)]
    [InlineData("", "")]
    [InlineData("plainaddress", "plainaddress")] // No '@' character
    [InlineData("multiple@@ats.com", "multiple@@ats.com")] // Invalid split
    public void Mask_ShouldReturnInput_WhenInvalidEmail(string input, string expected)
    {
        var result = _masker.Mask(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("a@domain.com", "*@domain.com")]
    [InlineData("ab@domain.com", "**@domain.com")]
    [InlineData("abc@domain.com", "a*c@domain.com")]
    [InlineData("abcd@domain.com", "a**d@domain.com")]
    [InlineData("abcdefg@domain.com", "a*****g@domain.com")]
    public void Mask_ShouldMaskUsernameCorrectly(string input, string expected)
    {
        var result = _masker.Mask(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Mask_ShouldPreserveDomain()
    {
        var email = "user@example.com";
        var result = _masker.Mask(email);

        Assert.EndsWith("@example.com", result);
    }
}