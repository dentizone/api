using Dentizone.Application.Constants;

namespace Dentizone.Application.Abstracts
{
    public abstract class AbstractRepository
    {
        protected readonly int DefaultPageSize = Pagination.DefaultPageSize;

        public AbstractRepository()
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