using MeraStore.Shared.Kernel.WebApi.Attributes;
using Microsoft.AspNetCore.Builder;

namespace MeraStore.Shared.Kernel.WebApi.Extensions;

public static class EndpointConventionBuilderExtensions
{
    public static TBuilder SkipApiLogging<TBuilder>(this TBuilder builder)
        where TBuilder : IEndpointConventionBuilder
    {
        builder.WithMetadata(new SkipApiLoggingAttribute());
        return builder;
    }
}