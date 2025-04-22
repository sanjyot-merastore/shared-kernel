using MeraStore.Shared.Kernel.Context;

namespace MeraStore.Services.Sample.Api;

public class AppContext(string serviceName) : AppContextBase(serviceName)
{
  public Guid SampleId { get; set; } = Guid.NewGuid();
  public new static AppContext Current => (AppContext)AppContextScope.Current;
}