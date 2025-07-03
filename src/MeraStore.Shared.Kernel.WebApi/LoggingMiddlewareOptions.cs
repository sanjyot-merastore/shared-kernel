namespace MeraStore.Shared.Kernel.WebApi;

public sealed class LoggingMiddlewareOptions
{
    public List<string> SkipPaths { get; init; } = [];
    public List<string> SkipPathStartsWith { get; init; } = [];
    public List<string> SkipMethods { get; init; } = [];
}