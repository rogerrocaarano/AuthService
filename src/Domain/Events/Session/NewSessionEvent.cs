using DDDSharp.Abstractions.Domain;

namespace Domain.Events.Session;

public class NewSessionEvent : DomainEvent
{
    public Guid OwnerId { get; }
    public DateTime ExpiresAt { get; }

    public NewSessionEvent(Guid aggregateId, Guid ownerId, DateTime expiresAt, string action) 
        : base(aggregateId, action)
    {
        OwnerId = ownerId;
        ExpiresAt = expiresAt;
    }
}