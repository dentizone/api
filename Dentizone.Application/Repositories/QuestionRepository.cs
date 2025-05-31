using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Application.Repositories
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

        public async Task<Question?> DeleteAsync(string id)
        {
            var question = await GetByIdAsync(id);
            dbContext.Questions.Remove(question);
            await dbContext.SaveChangesAsync();
            return question;
        }

        public async Task<IEnumerable<Question>> GetAllAsync(int page = 1)
        {
            int skippedPages = CalculatePagination(page);
            return await dbContext.Questions
                .Skip(skippedPages)
                .Take(DefaultPageSize)
                .ToListAsync();
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