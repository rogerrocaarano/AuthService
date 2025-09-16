using DDDSharp.Abstractions.Domain;
using Domain.Events.Session;

namespace Domain.Aggregates;

public class Session : AggregateRoot
{
    private readonly Guid _ownerId;
    private readonly DateTime _expiresAt;
    private DateTime? _revokedAt;

    public Guid OwnerId() => _ownerId;
    public DateTime ExpiresAt() => _expiresAt;
    public DateTime? RevokedAt() => _revokedAt;


    /// <summary>
    /// Constructor used when rehydrating from persistence
    /// </summary>
    public Session(
        // for base constructor
        Guid id,
        DateTime createdAt,
        DateTime modifiedAt,
        // for class attributes
        Guid ownerId,
        DateTime expiresAt,
        DateTime? revokedAt = null)
        : base(id, createdAt, modifiedAt)
    {
        _ownerId = ownerId;
        _expiresAt = expiresAt;
        _revokedAt = revokedAt;
    }

    /// <summary>
    /// Constructor used when creating a new session
    /// </summary>
    /// <param name="ownerId">The ID of the identity that owns this session</param>
    /// <param name="expiresAt">The expiration time of the session</param>
    /// <exception cref="ArgumentException">Thrown if expiresAt is in the past</exception
    public Session(Guid ownerId, DateTime expiresAt)
        : base()
    {
        if (expiresAt <= DateTime.UtcNow)
            throw new ArgumentException("Expiration time must be in the future.", nameof(expiresAt));

        _ownerId = ownerId;
        _expiresAt = expiresAt;
        _revokedAt = null;

        AddDomainEvent(new NewSessionEvent(Id, ownerId, expiresAt, "SessionCreated"));
    }

    public bool IsValid()
    {
        return _revokedAt == null && DateTime.UtcNow < _expiresAt;
    }

    public void Revoke()
    {
        if (_revokedAt == null)
        {
            _revokedAt = DateTime.UtcNow;
            AddDomainEvent(new SessionRevokedEvent(Id, _ownerId, _revokedAt.Value, "SessionRevoked"));
        }
    }
}