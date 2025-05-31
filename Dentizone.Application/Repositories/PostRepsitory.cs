using Dentizone.Application.Abstracts;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Post?> DeleteAsync(string id)
        {
            var post = await GetByIdAsync(id);


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

        public async Task<Post?> GetByIdAsync(string id)
        {
            return await dbContext.Posts.FindAsync(id);
        }

        public async Task<Post> UpdateAsync(Post entity)
        {
            dbContext.Posts.Update(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }
    }
}