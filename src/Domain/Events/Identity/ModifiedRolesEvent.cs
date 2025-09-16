using DDDSharp.Abstractions.Domain;

namespace Domain.Events.Identity;

public class ModifiedRolesEvent : DomainEvent
{
    public string RoleName { get; }

    public ModifiedRolesEvent(Guid aggregateId, string roleName, string action) 
        : base(aggregateId, action)
    {
        RoleName = roleName ?? throw new ArgumentNullException(nameof(roleName));
    }
}