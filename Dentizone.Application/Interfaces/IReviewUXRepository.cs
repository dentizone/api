using Dentizone.Domain.Entity;

namespace Dentizone.Application.Interfaces;

interface IReviewUXRepository : IBaseRepo<ReviewUx>
{
    Task<ReviewUx> Update(ReviewUx entity);
}