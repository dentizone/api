using Dentizone.Domain.Entity;

namespace Dentizone.Domain.Interfaces.Repositories;

public interface IUserRepository : IBaseRepo<AppUser>
{
    Task<AppUser> Update(AppUser entity);
    Task<IEnumerable<AppUser>> GetAllAsync(int page = 1);

    Task<AppUser?> DeleteAsync(string id);
}