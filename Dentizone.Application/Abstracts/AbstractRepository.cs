using Dentizone.Application.Constants;
using Dentizone.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Application.Abstracts
{
    public abstract class AbstractRepository
    {
        protected readonly int DefaultPageSize = Pagination.DefaultPageSize;
        protected readonly AppDbContext dbContext;
        public AbstractRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

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