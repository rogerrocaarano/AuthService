using DDDSharp.Abstractions.Domain;
using Domain.Events.Identity;
using Domain.Events.Session;
using Domain.ValueObjects;

namespace Domain.Aggregates;

public class Identity : AggregateRoot
{
    private readonly List<Credential> _credentials = [];
    private readonly List<Role> _roles = [];
    private readonly List<Claim> _claims = [];
    private bool _isActive;

    public IReadOnlyCollection<Credential> Credentials() => _credentials.AsReadOnly();
    public IReadOnlyCollection<Role> Roles() => _roles.AsReadOnly();
    public IReadOnlyCollection<Claim> Claims() => _claims.AsReadOnly();
    public bool IsActive() => _isActive;
    
    public Identity(
        // for base constructor
        Guid id,
        DateTime createdAt, 
        DateTime modifiedAt,
        // for class attributes
        IEnumerable<Credential> credentials, 
        IEnumerable<Role>? roles = null, 
        IEnumerable<Claim>? claims = null,
        bool isActive = true) 
        : base(id, createdAt, modifiedAt)
    {
        ArgumentNullException.ThrowIfNull(credentials);
        var enumerable = credentials.ToList();
        if (enumerable.Count == 0)
            throw new ArgumentException("At least one credential is required.", nameof(credentials));
        
        _credentials.AddRange(enumerable);
        if (roles != null) _roles.AddRange(roles);
        if (claims != null) _claims.AddRange(claims);
        _isActive = isActive;
    }

    public Identity(
        Credential credentials, 
        IEnumerable<Role>? roles = null, 
        IEnumerable<Claim>? claims = null,
        bool isActive = true)
    : base()
    {
        ArgumentNullException.ThrowIfNull(credentials);
        _credentials.Add(credentials);
        if (roles != null) _roles.AddRange(roles);
        if (claims != null) _claims.AddRange(claims);
        _isActive = isActive;

        AddDomainEvent(new IdentityCreatedEvent(Id, credentials.Type, credentials.Identifier, "IdentityCreated"));
    }

    public void AddCredential(Credential credential)
    {
        ArgumentNullException.ThrowIfNull(credential);
        if (_credentials.Contains(credential)) return;
        _credentials.Add(credential);
        AddDomainEvent(new ModifiedCredentialsEvent(Id, credential.Type, credential.Identifier, "CredentialAdded"));
    }

    public void RemoveCredential(Credential credential)
    {
        ArgumentNullException.ThrowIfNull(credential);
        if (!_credentials.Contains(credential)) return;

        if (_credentials.Count == 1)
            throw new InvalidOperationException("The identity must have at least one credential.");

        if (_credentials.Remove(credential))
        {
            AddDomainEvent(new ModifiedCredentialsEvent(Id, credential.Type, credential.Identifier, "CredentialRemoved"));
        }
    }

    public bool ValidateCredential(Credential credential)
    {
        ArgumentNullException.ThrowIfNull(credential);
        return _credentials.Contains(credential);
    }

    public void AddRole(Role role)
    {
        ArgumentNullException.ThrowIfNull(role);
        if (_roles.Contains(role)) return;
        _roles.Add(role);
        AddDomainEvent(new ModifiedRolesEvent(Id, role.Name, "RoleAdded"));
    }

    public void RemoveRole(Role role)
    {
        ArgumentNullException.ThrowIfNull(role);
        if (!_roles.Contains(role)) return;
        _roles.Remove(role);
        AddDomainEvent(new ModifiedRolesEvent(Id, role.Name, "RoleRemoved"));
    }

    public void AddClaim(Claim claim)
    {
        ArgumentNullException.ThrowIfNull(claim);
        if (_claims.Contains(claim)) return;
        _claims.Add(claim);
        AddDomainEvent(new ModifiedClaimsEvent(Id, claim.Type, claim.Value, "ClaimAdded"));
    }

    public void RemoveClaim(Claim claim)
    {
        ArgumentNullException.ThrowIfNull(claim);
        if (!_claims.Contains(claim)) return;
        _claims.Remove(claim);
        AddDomainEvent(new ModifiedClaimsEvent(Id, claim.Type, claim.Value, "ClaimRemoved"));
    }

    public void Activate()
    {
        if (_isActive)
            throw new InvalidOperationException("The identity is already active.");

        _isActive = true;
        AddDomainEvent(new ToggledIdentityActivationEvent(Id, _isActive, "IdentityActivated"));
    }

    public void Deactivate()
    {
        if (!_isActive)
            throw new InvalidOperationException("The identity is already inactive.");

        _isActive = false;
        AddDomainEvent(new ToggledIdentityActivationEvent(Id, _isActive, "IdentityDeactivated"));
    }
}