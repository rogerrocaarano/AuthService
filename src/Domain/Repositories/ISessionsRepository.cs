using DDDSharp.Abstractions.Application;
using Domain.Identities;

namespace Domain.Repositories;

public interface ISessionsRepository : IRepository<Session>
{
    Task<Session[]> GetAllSessionsForIdentityAsync(Guid identityId, CancellationToken cancellationToken);
}