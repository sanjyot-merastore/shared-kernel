using MeraStore.Shared.Kernel.Logging.Filters;
using MeraStore.Shared.Kernel.Logging.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MeraStore.Shared.Kernel.Logging;

public static class ServiceRegistrations
{
  public static IServiceCollection AddLoggingServicesCollection(this IServiceCollection services)
  {
    services.TryAddScoped<IMaskingFilter, MaskingFilter>();
    services.TryAddScoped<IMaskingFilterBuilder, MaskingFilterBuilder>();
    services.TryAddScoped<IMaskingFieldFilter, JsonPayloadRequestFilter>();
    services.TryAddScoped<IMaskingFieldFilter, JsonPayloadResponseFilter>();
    return services;
  }
}