using MeraStore.Shared.Kernel.Logging.Filters;

namespace MeraStore.Shared.Kernel.Logging.Interfaces;

public interface IMaskingFilterBuilder
{
  MaskingFilterBuilder AddRequestFilter(IMaskingFieldFilter filter);
  MaskingFilterBuilder AddResponseFilter(IMaskingFieldFilter filter);
  IMaskingFilter Build();
}