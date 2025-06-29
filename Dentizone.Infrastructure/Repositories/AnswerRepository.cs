using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dentizone.Infrastructure.Repositories
{
    internal class AnswerRepository(AppDbContext dbContext) : AbstractRepository(dbContext), IAnswerRepository
    {
        public async Task<Answer> CreateAsync(Answer entity)
        {
            await DbContext.Answers.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }


        public async Task<Answer?> FindBy(
            Expression<Func<Answer, bool>> condition,
            Expression<Func<Answer, object>>[]? includes = null)
        {
            var query = DbContext.Answers.AsQueryable();

            if (includes == null)
                return await DbContext.Answers
                    .FirstOrDefaultAsync(condition);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query
                .FirstOrDefaultAsync(condition);
        }


        public async Task<Answer?> DeleteAsync(string id)
        {
            var answer = await GetByIdAsync(id);


            DbContext.Answers.Remove(answer);
            await DbContext.SaveChangesAsync();
            return answer;
        }


        public async Task<Answer?> GetByIdAsync(string id)
        {
            return await DbContext.Answers.FindAsync(id);
        }

        public async Task<Answer> UpdateAsync(Answer entity)
        {
            DbContext.Answers.Update(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }
    }
}