using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure;
using Dentizone.Domain.Enums;

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

        public async Task<Answer?> DeleteAsync(int id)
        {
            var answer = await GetByIdAsync(id);
            if (answer == null)
            {
                return null;
            }
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

        public async Task<Answer?> GetByIdAsync(int id)
        {
            return await dbContext.Answers.FindAsync(id);
        }

        public async Task<Answer> UpdateAsync(Answer entity)
        {
            var isExists = await dbContext.Answers.FindAsync(entity.Id);
            if (isExists == null)
                return null;

            if (!string.IsNullOrEmpty(entity.Text))
                isExists.Text = entity.Text;

            if (Enum.IsDefined(typeof(AnswerStatus), entity.Status))
            {
                if (entity.Status != entity.Status)
                    isExists.Status = entity.Status;
            }

            isExists.UpdatedAt = DateTime.UtcNow;

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;   
            }
            return isExists;
        }

    }
}
