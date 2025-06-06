﻿using MeraStore.Shared.Kernel.Core.Enums;

namespace MeraStore.Shared.Kernel.Core.Domain.ValueObjects;

public class Money : IEquatable<Money>
{
  public decimal Amount { get; }
  public Currency Currency { get; }

  public Money(decimal amount, Currency currency = Currency.INR)
  {
    if (amount < 0) throw new FormatException("Amount cannot be negative");

    Amount = amount;
    Currency = currency;
  }

  public override bool Equals(object? obj) => Equals(obj as Money);
  public bool Equals(Money? other) => other != null &&
                                      Amount == other.Amount &&
                                      Currency == other.Currency;

  public override int GetHashCode() => HashCode.Combine(Amount, Currency);
}