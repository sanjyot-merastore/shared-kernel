using FluentAssertions;
using MeraStore.Shared.Kernel.Exceptions.Codes.Events;
using MeraStore.Shared.Kernel.Exceptions.Codes.Http;
using MeraStore.Shared.Kernel.Exceptions.Codes.Services;

namespace MeraStore.Shared.Kernel.Exceptions.Tests.Helpers
{
    public class RegistryWrappersTests
    {
        [Fact]
        public void ServiceCodeRegistry_ShouldReturnCode_IfServiceNameExists()
        {
            // Replace with actual service name in your constants
            var code = ServiceCodeRegistry.GetCode("logging-service");
            code.Should().NotBe("00", "because LoggingService should be a valid registered service");
        }

        [Fact]
        public void ServiceCodeRegistry_ShouldReturnUnknown_IfCodeNotFound()
        {
            var key = ServiceCodeRegistry.GetKey("SVC-INVALID");
            key.Should().Be("unknown");
        }

        [Fact]
        public void EventCodeRegistry_ShouldReturnCode_IfEventExists()
        {
            // Replace with a real key in EventCodes
            var code = EventCodeRegistry.GetCode("InvalidOperation");
            code.Should().NotBe("00", "because InvalidOperation should be registered");
        }

        [Fact]
        public void EventCodeRegistry_ShouldReturnUnknown_IfCodeNotFound()
        {
            var key = EventCodeRegistry.GetKey("EVT-NOTFOUND");
            key.Should().Be("unknown");
        }

        [Fact]
        public void ErrorCodeRegistry_ShouldReturnCode_IfErrorExists()
        {
            // Replace with a valid error key
            var code = ErrorCodeRegistry.GetCode("InvalidOperation");
            code.Should().NotBe("00", "because InvalidOperation should be registered");
        }

        [Fact]
        public void ErrorCodeRegistry_ShouldReturnUnknown_IfCodeNotFound()
        {
            var key = ErrorCodeRegistry.GetKey("ERR-NOTFOUND");
            key.Should().Be("unknown");
        }
    }
}