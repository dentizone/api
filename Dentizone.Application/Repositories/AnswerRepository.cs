using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Application.Repositories
{
    internal class AnswerRepository : AbstractRepository, IAnswerRepository
    {
        public AnswerRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Answer> CreateAsync(Answer entity)
        {
            await dbContext.Answers.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Answer?> DeleteAsync(string id)
        {
            var answer = await GetByIdAsync(id);


            dbContext.Answers.Remove(answer);
            await dbContext.SaveChangesAsync();
            return answer;
        }

        public async Task<IEnumerable<Answer>> GetAllAsync(int page = 1)
        {
            int skippedPages = CalculatePagination(page);
            return await dbContext.Answers
                .Skip(skippedPages)
                .Take(DefaultPageSize)
                .ToListAsync();
        }

        public async Task<Answer?> GetByIdAsync(string id)
        {
            return await dbContext.Answers.FindAsync(id);
        }

        public async Task<Answer?> UpdateAsync(Answer entity)
        {
            dbContext.Answers.Update(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }
    }
}