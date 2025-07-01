using System.Linq;
using System.Linq.Expressions;
using Dentizone.Domain.Constants;
using Dentizone.Domain.Interfaces;

namespace Dentizone.Infrastructure.Repositories
{
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

        protected IQueryable<T> BuildPagedQuery<T>(int page, Expression<Func<T, bool>>? filter,
            IQueryable<T> query) where T : IBaseEntity
        {
            if (page < 1)
            {
                page = 1;
            }

            // Always order before pagination
            query = query.OrderByDescending(o => o.CreatedAt);

            // Apply filter if present
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Always apply pagination
            query = query.Skip(CalculatePagination(page)).Take(DefaultPageSize);

            return query;
        }
    }
}