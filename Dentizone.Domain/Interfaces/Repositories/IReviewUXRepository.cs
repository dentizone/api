using Dentizone.Domain.Entity;

namespace Dentizone.Domain.Interfaces.Repositories;

public interface IReviewUXRepository : IBaseRepo<ReviewUx>
{
    Task<ReviewUx> Update(ReviewUx entity);
    Task<ReviewUx?> DeleteAsync(string id);
}