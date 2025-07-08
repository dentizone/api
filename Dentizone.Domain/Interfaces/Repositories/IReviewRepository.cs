using Dentizone.Domain.Entity;
using System.Linq.Expressions;

namespace Dentizone.Domain.Interfaces.Repositories;

public interface IReviewRepository : IBaseRepo<Review>
{
    Task<Review> Update(Review entity);
    Task<Review?> DeleteAsync(string id);
    Task<PagedResult<Review>> FindAllBy(int page, Expression<Func<Review, bool>>? filter);
    Task<Dictionary<string, int>> GetReviewsStatsByAll();
}