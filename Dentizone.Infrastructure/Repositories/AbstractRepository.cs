using Dentizone.Domain.Constants;

namespace Dentizone.Infrastructure.Repositories
{
    public abstract class AbstractRepository(AppDbContext dbContext)
    {
        protected readonly int DefaultPageSize = Pagination.DefaultPageSize;
        protected readonly AppDbContext dbContext = dbContext;

        protected int CalculatePagination(int page)
        {
            if (page < 1)
            {
                page = 1;
            }

            return (page - 1) * DefaultPageSize;
        }
    }
}