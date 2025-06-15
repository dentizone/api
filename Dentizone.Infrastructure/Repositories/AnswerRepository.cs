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
            await dbContext.Answers.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }


        public async Task<Answer?> FindBy(Expression<Func<Answer, bool>> condition,
                                          Expression<Func<Answer, object>>[]? includes)
        {
            var query = dbContext.Answers.AsQueryable();

            if (includes == null)
                return await dbContext.Answers
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


            dbContext.Answers.Remove(answer);
            await dbContext.SaveChangesAsync();
            return answer;
        }


        public async Task<Answer?> GetByIdAsync(string id)
        {
            return await dbContext.Answers.FindAsync(id);
        }

        public async Task<Answer> UpdateAsync(Answer entity)
        {
            dbContext.Answers.Update(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }
    }
}