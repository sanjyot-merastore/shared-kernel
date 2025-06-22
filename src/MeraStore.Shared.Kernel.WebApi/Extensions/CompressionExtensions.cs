using System.IO.Compression;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;

namespace MeraStore.Shared.Kernel.WebApi.Extensions;

public static class CompressionExtensions
{
    /// <summary>
    /// Registers Gzip/Brotli response compression for supported clients.
    /// </summary>
    public static IServiceCollection AddMeraStoreCompression(this IServiceCollection services)
    {
        services.Configure<BrotliCompressionProviderOptions>(options => { options.Level = CompressionLevel.Fastest; });

        services.Configure<GzipCompressionProviderOptions>(options => { options.Level = CompressionLevel.Fastest; });

        services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
            options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
            [
                "application/json",
                "application/xml",
                "text/plain",
                "text/css",
                "application/javascript"
            ]);
        });

        return services;
    }
}