using MeraStore.Shared.Kernel.WebApi.Attributes;
using Microsoft.AspNetCore.Builder;

namespace MeraStore.Shared.Kernel.WebApi.Extensions;

public static class MinimalApiHeaderExtensions
{
    public static TBuilder WithHeader<TBuilder>(
        this TBuilder builder,
        string name,
        bool required = false,
        string? description = null)
        where TBuilder : IEndpointConventionBuilder
    {
        builder.WithMetadata(new HeaderAttribute(name, required, description));
        return builder;
    }

    public static TBuilder WithHeaders<TBuilder>(
        this TBuilder builder,
        params HeaderAttribute[] headers)
        where TBuilder : IEndpointConventionBuilder
    {
        foreach (var header in headers)
        {
            builder.WithMetadata(header);
        }

        return builder;
    }
}