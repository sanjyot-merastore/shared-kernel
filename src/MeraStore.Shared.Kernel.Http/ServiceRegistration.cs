using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeraStore.Shared.Kernel.Http;

public static class ServiceRegistration
{
  public static IServiceCollection AddHttpServices(this IServiceCollection services, IConfiguration configuration)
  {

    services.AddHttpClient();
    return services;
  }
}