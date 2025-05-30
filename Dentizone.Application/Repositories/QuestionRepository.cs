using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<Question?> DeleteAsync(int id)
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

        public async Task<IEnumerable<Question>> GetAllAsync(int page = 1)
        {
            int skippedPages = CalculatePagination(page);
            return await dbContext.Questions
                            .Skip(skippedPages)
                            .Take(DefaultPageSize)
                            .ToListAsync();
        }

        public async Task<Question?> GetByIdAsync(int id)
        {
            return await dbContext.Questions.FindAsync(id);
        }
    }
}
