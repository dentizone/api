using System.Linq.Expressions;
using Dentizone.Domain.Constants;
using Dentizone.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Infrastructure.Repositories
{
    public interface IPagedResult<T>
    {
        IQueryable<T> Query { get; }
        int TotalCount { get; }
    }

    public class PaginatedResult<T> : IPagedResult<T>
    {
        public IQueryable<T> Query { get; set; } = Enumerable.Empty<T>().AsQueryable();
        public int TotalCount { get; set; }
    }

    public abstract class AbstractRepository(AppDbContext dbContext)
    {
        protected readonly int DefaultPageSize = Pagination.DefaultPageSize;
        protected readonly AppDbContext DbContext = dbContext;


        protected int CalculatePagination(int page)
        {
            if (page < 1)
            {
                page = 1;
            }

            return (page - 1) * DefaultPageSize;
        }

        protected async Task<PaginatedResult<T>> BuildPagedQuery<T>(
            int page,
            Expression<Func<T, bool>>? filter,
            IQueryable<T> query) where T : IBaseEntity
        {
            if (page < 1)
                page = 1;

            query = query.OrderByDescending(o => o.CreatedAt);

            int count = 0;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            count = await query.CountAsync();

            var pagedQuery = query.Skip(CalculatePagination(page)).Take(DefaultPageSize);

            return new PaginatedResult<T>
            {
                Query = pagedQuery,
                TotalCount = count
            };
        }
    }
}