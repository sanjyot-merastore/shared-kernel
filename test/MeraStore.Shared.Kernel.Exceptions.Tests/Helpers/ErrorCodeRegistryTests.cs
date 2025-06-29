using FluentAssertions;
using MeraStore.Shared.Kernel.Exceptions.Codes.Http;

namespace MeraStore.Shared.Kernel.Exceptions.Tests.Helpers
{
    public class ErrorCodeRegistryTests
    {
        [Fact]
        public void GetCode_ShouldReturnCode_WhenErrorNameExists()
        {
            var result = ErrorCodeRegistry.GetCode("InvalidOperation");
            result.Should().NotBe("00");
        }

        [Fact]
        public void GetKey_ShouldReturnErrorName_WhenCodeExists()
        {
            var code = ErrorCodeRegistry.GetCode("InvalidOperation");
            var key = ErrorCodeRegistry.GetKey(code);
            key.Should().Be("InvalidOperation");
        }
    }
}