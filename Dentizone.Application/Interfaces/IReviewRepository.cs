using Dentizone.Domain.Entity;

namespace Dentizone.Application.Interfaces;

internal interface IReviewRepository : IBaseRepo<Review>
{
    Task<Review> Update(Review entity);
}