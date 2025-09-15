using DDDSharp.Abstractions.Domain;

namespace Domain.Identities;

public class Session : AggregateRoot
{
    private readonly Guid _ownerId;
    private readonly DateTime _expiresAt;
    private DateTime? _revokedAt;

    public Guid OwnerId() => _ownerId;
    public DateTime ExpiresAt() => _expiresAt;
    public DateTime? RevokedAt() => _revokedAt;

    public Session(Guid ownerId, DateTime expiresAt)
    {
        _ownerId = ownerId;
        _expiresAt = expiresAt;
    }

    public bool IsValid()
    {
        return _revokedAt == null && DateTime.UtcNow < _expiresAt;
    }

    public void Revoke()
    {
        _revokedAt = DateTime.UtcNow;
    }
}