using DDDSharp.Abstractions.Domain;
using Domain.ValueObjects;

namespace Domain.Identities;

public class Identity : AggregateRoot
{
    private readonly List<Credential> _credentials = new();
    private readonly List<Role> _roles = new();
    private readonly List<Claim> _claims = new();
    private bool _isActive;

    public IReadOnlyCollection<Credential> Credentials() => _credentials.AsReadOnly();
    public IReadOnlyCollection<Role> Roles() => _roles.AsReadOnly();
    public IReadOnlyCollection<Claim> Claims() => _claims.AsReadOnly();
    public bool IsActive() => _isActive;

    public Identity(IEnumerable<Credential> credentials, IEnumerable<Role>? roles = null, IEnumerable<Claim>? claims = null, bool isActive = true)
    {
        if (credentials == null || !credentials.Any()) 
            throw new ArgumentException("At least one credential is required.", nameof(credentials));
        
        _credentials.AddRange(credentials);
        if (roles != null) _roles.AddRange(roles);
        if (claims != null) _claims.AddRange(claims);
        _isActive = isActive;
    }

    public void AddCredential(Credential credential)
    {
        if (credential == null) throw new ArgumentNullException(nameof(credential));
        if (!_credentials.Contains(credential))
            _credentials.Add(credential);
    }

    public void RemoveCredential(Credential credential)
    {
        if (credential == null) throw new ArgumentNullException(nameof(credential));
        _credentials.Remove(credential);
    }

    public bool ValidateCredential(Credential credential)
    {
        if (credential == null) throw new ArgumentNullException(nameof(credential));
        return _credentials.Contains(credential);
    }

    public void AddRole(Role role)
    {
        if (role == null) throw new ArgumentNullException(nameof(role));
        if (!_roles.Contains(role))
            _roles.Add(role);
    }

    public void RemoveRole(Role role)
    {
        if (role == null) throw new ArgumentNullException(nameof(role));
        _roles.Remove(role);
    }

    public void AddClaim(Claim claim)
    {
        if (claim == null) throw new ArgumentNullException(nameof(claim));
        if (!_claims.Contains(claim))
            _claims.Add(claim);
    }

    public void RemoveClaim(Claim claim)
    {
        if (claim == null) throw new ArgumentNullException(nameof(claim));
        _claims.Remove(claim);
    }

    public void Activate() => _isActive = true;

    public void Deactivate() => _isActive = false;
}