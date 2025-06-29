using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dentizone.Infrastructure.Repositories
{
    internal class QuestionRepository(AppDbContext dbContext) : AbstractRepository(dbContext), IQuestionRepository
    {
        public async Task<Question> CreateAsync(Question entity)
        {
            await DbContext.Questions.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Question?> FindBy(Expression<Func<Question, bool>> condition,
            Expression<Func<Question, object>>[]? includes)
        {
            IQueryable<Question> query = DbContext.Questions;
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(condition);
        }

        public async Task<Question?> DeleteAsync(string id)
        {
            var question = await GetByIdAsync(id);
            if (question == null)
            {
                return null;
            }

            DbContext.Questions.Remove(question);
            await DbContext.SaveChangesAsync();
            return question;
        }


        public async Task<Question?> GetByIdAsync(string id)
        {
            return await DbContext.Questions
                .Include(p => p.Post)
                .Where(q => q.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Question> UpdateAsync(Question entity)
        {
            DbContext.Questions.Update(entity);
            await DbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<List<Question>> FindAllBy(Expression<Func<Question, bool>> condition,
            Expression<Func<Question, object>>[]? includes = null)
        {
            IQueryable<Question> query = DbContext.Questions;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.Where(condition).ToListAsync();
        }
    }
}