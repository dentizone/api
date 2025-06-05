using Dentizone.Domain.Entity;

namespace Dentizone.Domain.Interfaces.Repositories;

public interface IReviewRepository : IBaseRepo<Review>
{
    Task<Review> Update(Review entity);
    Task<Review?> DeleteAsync(string id);
}