using DDDSharp.Abstractions.Domain;

namespace Domain.ValueObjects;

public sealed class Role : IValueObject
{
    public string Name { get; }

    public Role(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
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