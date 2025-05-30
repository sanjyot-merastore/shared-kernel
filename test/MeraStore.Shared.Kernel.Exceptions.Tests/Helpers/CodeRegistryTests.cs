using FluentAssertions;
using MeraStore.Shared.Kernel.Exceptions.Helpers;

namespace MeraStore.Shared.Kernel.Exceptions.Tests.Helpers;

public class CodeRegistryTests
{
    private readonly Dictionary<string, string> _testRegistry = new()
    {
        { "TestService", "SVC-001" },
        { "TestEvent", "EVT-001" },
        { "TestError", "ERR-001" }
    };

    [Fact]
    public void GetCode_ShouldReturnCorrectCode_WhenKeyExists()
    {
        var result = CodeRegistry.GetCode("TestService", _testRegistry);
        result.Should().Be("SVC-001");
    }

    [Fact]
    public void GetCode_ShouldReturnDefault_WhenKeyDoesNotExist()
    {
        var result = CodeRegistry.GetCode("NonExistentKey", _testRegistry);
        result.Should().Be("00");
    }

    [Fact]
    public void GetKey_ShouldReturnCorrectKey_WhenCodeExists()
    {
        var result = CodeRegistry.GetKey("EVT-001", _testRegistry);
        result.Should().Be("TestEvent");
    }

    [Fact]
    public void GetKey_ShouldReturnDefault_WhenCodeDoesNotExist()
    {
        var result = CodeRegistry.GetKey("NON-999", _testRegistry);
        result.Should().Be("unknown");
    }
}

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
