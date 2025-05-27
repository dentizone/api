using Dentizone.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Infrastructure
{
    internal class DbContext : IdentityDbContext<AppUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbContext"/> class with the specified options.
        /// </summary>
        /// <param name="options">The options to be used by the DbContext.</param>
        public DbContext(DbContextOptions<DbContext> options)
            : base(options)
        {

          
        }

        /// <summary>
        /// Configures the model for the database context by invoking the base implementation.
        /// </summary>
        /// <param name="builder">The builder used to construct the model for the context.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
