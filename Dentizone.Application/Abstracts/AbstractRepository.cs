using AutoMapper;
using Dentizone.Application.Constants;
using Dentizone.Infrastructure;

namespace Dentizone.Application.Abstracts
{
    public abstract class AbstractRepository
    {
        protected readonly int DefaultPageSize = Pagination.DefaultPageSize;

        public AbstractRepository(IMapper mapper)
        {
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