using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MeraStore.Services.Sample.Api;

public class ServiceHealthCheck : IHealthCheck
{
  public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
  {
    return Task.FromResult(HealthCheckResult.Healthy("Sample service is running."));
  }
}