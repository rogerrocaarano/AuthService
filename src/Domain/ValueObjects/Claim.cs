using DDDSharp.Abstractions.Domain;

namespace Domain.ValueObjects;

public sealed class Claim : IValueObject
{
    public string Type { get; }
    public string Value { get; }

    public Claim(string type, string value)
    {
        Type = type ?? throw new ArgumentNullException(nameof(type));
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as IValueObject);
    }

    public bool Equals(IValueObject? other)
    {
        if (other is not Claim claim) return false;
        return Type == claim.Type && Value == claim.Value;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Type, Value);
    }
}