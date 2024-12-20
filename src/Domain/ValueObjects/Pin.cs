using Domain.Exceptions;

namespace Domain.ValueObjects;

public sealed class Pin
{
    public string Value { get; }

    public Pin(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("PIN cannot be empty or whitespace.");
        }

        if (value.Length < 4)
        {
            throw new DomainException("PIN must be at least 4 characters long.");
        }

        Value = value;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Pin other)
            return Value == other.Value;
        return false;
    }

    public override int GetHashCode()
    {
        return StringComparer.Ordinal.GetHashCode(Value);
    }
}