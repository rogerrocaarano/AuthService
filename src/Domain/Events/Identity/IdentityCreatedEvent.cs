using DDDSharp.Abstractions.Domain;

namespace Domain.Events.Identity;

public class IdentityCreatedEvent : DomainEvent
{
    public string CredentialType { get; }
    public string CredentialIdentifier { get; }

    public IdentityCreatedEvent(Guid aggregateId, string credentialType, string credentialIdentifier, string action) 
        : base(aggregateId, action)
    {
        if (string.IsNullOrWhiteSpace(credentialType))
            throw new ArgumentException("CredentialType cannot be null or empty.", nameof(credentialType));
        if (string.IsNullOrWhiteSpace(credentialIdentifier))
            throw new ArgumentException("CredentialIdentifier cannot be null or empty.", nameof(credentialIdentifier));

        CredentialType = credentialType;
        CredentialIdentifier = credentialIdentifier;
    }
}