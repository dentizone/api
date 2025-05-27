using Dentizone.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Infrastructure
{
    internal class DbContext : IdentityDbContext<AppUser>
    {
        public DbContext(DbContextOptions<DbContext> options)
            : base(options)
        {
        }
    }
}
