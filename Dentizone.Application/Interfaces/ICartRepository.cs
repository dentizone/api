using Dentizone.Domain.Entity;

namespace Dentizone.Application.Interfaces;

internal interface ICartRepository : IBaseRepo<Cart>
{
    Task<Cart?> DeleteAsync(string id);
}