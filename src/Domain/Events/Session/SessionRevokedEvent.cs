using DDDSharp.Abstractions.Domain;

namespace Domain.Events.Session;

public class SessionRevokedEvent : DomainEvent
{
    public Guid OwnerId { get; }
    public DateTime RevokedAt { get; }

    public SessionRevokedEvent(Guid aggregateId, Guid ownerId, DateTime revokedAt, string action) 
        : base(aggregateId, action)
    {
        OwnerId = ownerId;
        RevokedAt = revokedAt;
    }
}