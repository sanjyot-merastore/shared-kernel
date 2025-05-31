using MeraStore.Shared.Kernel.Exceptions.Codes.Events;
using MeraStore.Shared.Kernel.Exceptions.Codes.Http;

namespace MeraStore.Shared.Kernel.Http.Codes;

[ExcludeFromCodeCoverage]
public static class CodesHelpers
{
    static CodesHelpers()
    {
        EventCodeRegistry.Register("UrlMissing", "1001");
        ErrorCodeRegistry.Register("UrlMissing", "1002");
        
        EventCodeRegistry.Register("HttpMethodMissing", "1001");
        ErrorCodeRegistry.Register("HttpMethodMissing", "1002");
    }
}
