namespace MeraStore.Shared.Kernel.Core.Domain.Entities;

public abstract class ValueObject
{
  protected abstract IEnumerable<object> GetEqualityComponents();

  public override bool Equals(object obj)
  {
    return obj is ValueObject other && GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
  }

  public override int GetHashCode()
  {
    return GetEqualityComponents()
      .Select(x => x?.GetHashCode() ?? 0)
      .Aggregate((x, y) => x ^ y);
  }
}