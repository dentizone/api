using Dentizone.Application.DTOs.Review;
using Dentizone.Domain.Entity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Hosting;
using Pipelines.Sockets.Unofficial;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Dentizone.Application.Interfaces.Review
{
    public interface IReviewService
    {
       Task CreateOrderReviewAsync(string userId,CreateReviewDTO  createReviewDto);
       Task UpdateReviewAsync(string reviewId, UpdateReviewDTO updateReviewDto);
       Task DeleteReviewAsync(string reviewId);
       Task <IEnumerable<ReviewDTO>> GetUserReviewsTaken(string userId);
    }
}
