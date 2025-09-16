using DDDSharp.Abstractions.Domain;

namespace Domain.ValueObjects;

public sealed class Role : IValueObject
{
    public string Name { get; }

    public Role(string name)
    {
        Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentException("Name cannot be null or whitespace.", nameof(name));
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as IValueObject);
    }

    public bool Equals(IValueObject? other)
    {
        if (other is not Role role) return false;
        return Name == role.Name;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}