using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dentizone.Infrastructure.Repositories
{
    internal class QuestionRepository : AbstractRepository, IQuestionRepository
    {
        public QuestionRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Question> CreateAsync(Question entity)
        {
            await dbContext.Questions.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Question?> FindBy(Expression<Func<Question, bool>> condition,
                                            Expression<Func<Question, object>>[]? includes)
        {
            IQueryable<Question> query = dbContext.Questions;
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

            dbContext.Questions.Remove(question);
            await dbContext.SaveChangesAsync();
            return question;
        }


        public async Task<Question?> GetByIdAsync(string id)
        {
            return await dbContext.Questions.FindAsync(id);
        }

        public async Task<Question> UpdateAsync(Question entity)
        {
            dbContext.Questions.Update(entity);
            await dbContext.SaveChangesAsync();

            return entity;
        }
    }
}