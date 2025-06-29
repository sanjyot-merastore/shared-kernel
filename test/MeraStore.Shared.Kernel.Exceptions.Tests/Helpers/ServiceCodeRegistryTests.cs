using FluentAssertions;
using MeraStore.Shared.Kernel.Exceptions.Codes.Services;

namespace MeraStore.Shared.Kernel.Exceptions.Tests.Helpers;



public class ServiceCodeRegistryTests
{
    [Fact]
    public void GetCode_ShouldReturnCode_WhenServiceNameExists()
    {
        var result = ServiceCodeRegistry.GetCode("logging-service");
        result.Should().Be("25");
    }

    [Fact]
    public void GetKey_ShouldReturnServiceName_WhenCodeExists()
    {
        var result = ServiceCodeRegistry.GetKey("36");
        result.Should().Be("Masking".ToLowerInvariant());
    }

    [Fact]
    public void GetCode_ShouldReturnDefault_WhenServiceNameInvalid()
    {
        var result = ServiceCodeRegistry.GetCode("AlienService");
        result.Should().Be("00");
    }

    [Fact]
    public void GetKey_ShouldReturnDefault_WhenServiceCodeInvalid()
    {
        var result = ServiceCodeRegistry.GetKey("SVC-INVALID");
        result.Should().Be("unknown");
    }
}