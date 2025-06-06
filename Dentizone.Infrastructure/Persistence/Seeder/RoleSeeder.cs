using Dentizone.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Dentizone.Infrastructure.Persistence.Seeder
{
    public class RoleSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Get all values from UserRoles enum
            var roles = Enum.GetNames(typeof(UserRoles));

            foreach (var role in roles)
            {
                // Check if the role already exists
                var roleExists = await roleManager.RoleExistsAsync(role);

                // If role doesn't exist, create it
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        // Constructor can remain empty as we're using a static method
        public RoleSeeder()
        {
        }
    }
}