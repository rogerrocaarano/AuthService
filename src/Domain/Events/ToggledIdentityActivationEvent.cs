using DDDSharp.Abstractions.Domain;

namespace Domain.Events;

public class ToggledIdentityActivationEvent : DomainEvent
{
    public bool NewStatus { get; }

    public ToggledIdentityActivationEvent(Guid aggregateId, bool newStatus, string action) 
        : base(aggregateId, action)
    {
        NewStatus = newStatus;
    }
}