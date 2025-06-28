using FluentAssertions;
using MeraStore.Shared.Kernel.Exceptions.Codes.Events;

namespace MeraStore.Shared.Kernel.Exceptions.Tests.Helpers
{
    public class EventCodeRegistryTests
    {
        [Fact]
        public void GetCode_ShouldReturnCode_WhenEventNameExists()
        {
            var result = EventCodeRegistry.GetCode("InvalidOperation");
            result.Should().NotBe("00"); // Assumes it's registered
        }

        [Fact]
        public void GetKey_ShouldReturnEventName_WhenCodeExists()
        {
            var code = EventCodeRegistry.GetCode("InvalidOperation");
            var key = EventCodeRegistry.GetKey(code);
            key.Should().Be("InvalidOperation");
        }
    }
}