using Dentizone.Domain.Entity;

namespace Dentizone.Application.Interfaces;

internal interface IUserRepository : IBaseRepo<AppUser>
{
    Task<AppUser> Update(AppUser entity);
}