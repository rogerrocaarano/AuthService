using DDDSharp.Abstractions.Domain;

namespace Domain.Events.Identity;

public class ModifiedClaimsEvent : DomainEvent
{
    public string ClaimType { get; }
    public string ClaimValue { get; }

    public ModifiedClaimsEvent(Guid aggregateId, string claimType, string claimValue, string action) 
        : base(aggregateId, action)
    {
        ClaimType = claimType ?? throw new ArgumentNullException(nameof(claimType));
        ClaimValue = claimValue ?? throw new ArgumentNullException(nameof(claimValue));
    }
}