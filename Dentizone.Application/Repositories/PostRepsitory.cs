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

namespace Dentizone.Application.Repositories
{
    internal class PostRepository : AbstractRepository, IPostRepository
    {
        public PostRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Post> CreateAsync(Post entity)
        {
            await dbContext.Posts.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Post?> DeleteAsync(int id)
        {
            var post = await GetByIdAsync(id);
            if (post == null)
            {
                return null;
            }

            dbContext.Posts.Remove(post);
            await dbContext.SaveChangesAsync();
            return post;
        }

        public async Task<IEnumerable<Post>> GetAllAsync(int page = 1)
        {
            int skippedPages = CalculatePagination(page);
            return await dbContext.Posts
                                  .Skip(skippedPages)
                                  .Take(DefaultPageSize)
                                  .ToListAsync();
        }

        public async Task<Post?> GetByIdAsync(int id)
        {
            return await dbContext.Posts.FindAsync(id);
        }
    }
}
