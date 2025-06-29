using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dentizone.Infrastructure.Repositories
{
    internal class UniversityRepository(AppDbContext dbContext) : AbstractRepository(dbContext), IUniversityRepository
    {
        public async Task<University?> GetByIdAsync(string id)
        {
            return await DbContext.Universities.Where(u => !u.IsDeleted).FirstOrDefaultAsync(u => u.Id == id);
        }


        public async Task<University> CreateAsync(University entity)
        {
            await DbContext.Universities.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<University?> FindBy(Expression<Func<University, bool>> condition,
            Expression<Func<University, object>>[]? includes)
        {
            IQueryable<University> query = DbContext.Universities.Where(u => !u.IsDeleted);
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(condition);
        }

        public async Task<University?> DeleteAsync(string id)
        {
            var toBeDeleted = await GetByIdAsync(id);

            if (toBeDeleted == null)
            {
                return null;
            }

            DbContext.Universities.Remove(toBeDeleted);
            await DbContext.SaveChangesAsync();

            return toBeDeleted;
        }

        public async Task<University> Update(University entity)
        {
            DbContext.Universities.Update(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }


        public async Task<IReadOnlyCollection<University>> GetAll()
        {
            return await DbContext.Universities
                .Where(u => !u.IsDeleted)
                .OrderByDescending(u => u.CreatedAt)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<PagedResult<University>> GetAll(int page, Expression<Func<University, bool>>? filter)
        {
            var query = DbContext.Universities.AsQueryable();
            query = query.Where(u => !u.IsDeleted)
                .OrderByDescending(u => u.CreatedAt);

            var totalCount = await query.CountAsync();


            if (filter != null)
            {
                query = query.Where(filter);
            }

            var items = await query
                .Skip(CalculatePagination(page))
                .Take(DefaultPageSize)
                .ToListAsync();
            return new PagedResult<University>
            {
                Items = items,
                TotalCount = totalCount,
                PageSize = DefaultPageSize,
                Page = page
            };
        }
    }
}