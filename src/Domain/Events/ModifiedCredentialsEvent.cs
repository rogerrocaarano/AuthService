using DDDSharp.Abstractions.Domain;

namespace Domain.Events;

public class ModifiedCredentialsEvent : DomainEvent
{
    public string CredentialType { get; }
    public string CredentialIdentifier { get; }

    public ModifiedCredentialsEvent(Guid aggregateId, string credentialType, string credentialIdentifier, string action) 
        : base(aggregateId, action)
    {
        CredentialType = credentialType ?? throw new ArgumentNullException(nameof(credentialType));
        CredentialIdentifier = credentialIdentifier ?? throw new ArgumentNullException(nameof(credentialIdentifier));
    }
}