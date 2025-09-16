using DDDSharp.Abstractions.Domain;

namespace Domain.ValueObjects;

public sealed class Credential : IValueObject
{
    public string Type { get; }
    public string Identifier { get; }
    public string SecretHash { get; }
    public string Salt { get; }

    public Credential(string type, string identifier, string secretHash, string salt)
    {
        Type = string.IsNullOrWhiteSpace(type) ? throw new ArgumentNullException(nameof(type)) : type;
        Identifier = string.IsNullOrWhiteSpace(identifier) ? throw new ArgumentNullException(nameof(identifier)) : identifier;
        SecretHash = secretHash ?? throw new ArgumentNullException(nameof(secretHash));
        Salt = salt ?? throw new ArgumentNullException(nameof(salt));
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as IValueObject);
    }

    public bool Equals(IValueObject? other)
    {
        if (other is not Credential credential) return false;
        return Type == credential.Type && 
               Identifier == credential.Identifier && 
               SecretHash == credential.SecretHash && 
               Salt == credential.Salt;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Type, Identifier, SecretHash, Salt);
    }
}