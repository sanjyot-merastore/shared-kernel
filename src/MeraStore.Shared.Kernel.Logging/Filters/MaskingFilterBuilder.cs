using MeraStore.Shared.Kernel.Logging.Interfaces;

namespace MeraStore.Shared.Kernel.Logging.Filters;

public class MaskingFilterBuilder : IMaskingFilterBuilder
{
  private readonly List<IMaskingFieldFilter> _requestFilters = [];
  private readonly List<IMaskingFieldFilter> _responseFilters = [];

  public static MaskingFilterBuilder Create() => new MaskingFilterBuilder();

  public MaskingFilterBuilder AddRequestFilter(IMaskingFieldFilter filter)
  {
    _requestFilters.Add(filter);
    return this;
  }

  public MaskingFilterBuilder AddResponseFilter(IMaskingFieldFilter filter)
  {
    _responseFilters.Add(filter);
    return this;
  }

  public IMaskingFilter Build()
  {
    return new MaskingFilter(_requestFilters, _responseFilters);
  }
}