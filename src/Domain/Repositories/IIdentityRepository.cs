using DDDSharp.Abstractions.Application;
using Domain.Identities;

namespace Domain.Repositories;

public interface IIdentityRepository : IRepository<Identity>
{
    Task<Identity> GetEntityWithRolesAsync(Guid identityId, CancellationToken cancellationToken);
}