using Bogus;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using Dentizone.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Infrastructure.Persistence.Seeder
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            Randomizer.Seed = new Random(8675309);

            // Check if data already exists to prevent re-seeding
            // if (await context.Users.AnyAsync() || await context.Posts.AnyAsync())
            // {
            //     return; // DB has been seeded
            // }

            // --- PRE-REQUISITES: Fetch existing data for relationships ---
            var universities = await context.Universities.ToListAsync();
            var categoriesWithSubcategories = await context.Categories
                                                           .Include(c => c.SubCategories)
                                                           .Where(c => c.SubCategories.Any())
                                                           .ToListAsync();

            if (!universities.Any() || !categoriesWithSubcategories.Any())
            {
                // Can't proceed without universities and categories, which should be seeded by CatalogSeeder.
                return;
            }

            var universityIds = universities.Select(u => u.Id).ToList();


            // --- 1. SEED IDENTITY USERS (ApplicationUser) ---
            var identityUsersToCreate = new List<ApplicationUser>();
            var identityUserFaker = new Faker<ApplicationUser>()
                                    .RuleFor(u => u.UserName,
                                             f => f.Internet.Email(f.Name.FirstName(), f.Name.LastName()))
                                    .RuleFor(u => u.Email, (f, u) => u.UserName)
                                    .RuleFor(u => u.EmailConfirmed, false);

            var generatedUsers = identityUserFaker.Generate(10);
            foreach (var user in generatedUsers)
            {
                var result = await userManager.CreateAsync(user, "Password123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, UserRoles.GHOST.ToString());
                    identityUsersToCreate.Add(user);
                }
                else
                {
                    // Handle errors if user creation fails
                    Console.WriteLine($"Failed to create user {user.UserName}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }


            // --- 2. SEED DOMAIN USERS (AppUser) ---
            var appUserFaker = new Faker<AppUser>()
                .CustomInstantiator(f => new AppUser
                {
                    Id = identityUsersToCreate[f.IndexFaker].Id,

                    FullName = f.Name.FullName(),
                    Username = f.Name.FullName(),

                    IsDeleted = false,
                    CreatedAt = f.Date.Past(2),
                    UniversityId = f.PickRandom(universityIds)
                });

            var appUsers = appUserFaker.Generate(identityUsersToCreate.Count);
            await context.AppUsers.AddRangeAsync(appUsers);

            await context.SaveChangesAsync(); // Save AppUsers to get IDs

            var userIds = identityUsersToCreate.Select(u => u.Id).ToList();

            // --- 3. SEED ASSETS ---
            var assetFaker = new Faker<Asset>()
                             .RuleFor(a => a.Url, f => f.Image.PicsumUrl())
                             .RuleFor(a => a.Type, AssetType.Image)
                             .RuleFor(a => a.Status, AssetStatus.Active)
                             .RuleFor(a => a.IsDeleted, false)
                             .RuleFor(a => a.UserId, f => f.PickRandom(userIds));


            var assets = assetFaker.Generate(50);
            await context.Assets.AddRangeAsync(assets);
            await context.SaveChangesAsync(); // Save assets to get IDs

            // --- 4. SEED POSTS ---
            var postFaker = new Faker<Post>()
                            .RuleFor(p => p.SellerId, f => f.PickRandom(userIds))
                            .RuleFor(p => p.Title, f => f.Commerce.ProductName())
                            .RuleFor(p => p.Description, f => f.Lorem.Paragraphs(5))
                            .RuleFor(p => p.Price, f => f.Random.Decimal(50, 1000))
                            .RuleFor(p => p.Condition, f => f.PickRandom<PostItemCondition>())
                            .RuleFor(p => p.Status, PostStatus.Active)
                            .RuleFor(p => p.IsDeleted, false)
                            .RuleFor(p => p.City, f => f.Address.City())
                            .RuleFor(p => p.Street, f => f.Address.StreetAddress())
                            .RuleFor(p => p.CreatedAt, f => f.Date.Past(1))
                            .RuleFor(p => p.UpdatedAt, f => f.Date.Recent())
                            .RuleFor(p => p.ExpireDate, f => f.Date.Future(30))
                            .RuleFor(p => p.Slug, (f, p) => f.Lorem.Slug())
                            .FinishWith((f, p) =>
                            {
                                var randomCategory = f.PickRandom(categoriesWithSubcategories);
                                p.CategoryId = randomCategory.Id;
                                p.SubCategoryId = f.PickRandom(randomCategory.SubCategories).Id;
                            });

            var posts = postFaker.Generate(20);
            await context.Posts.AddRangeAsync(posts);
            await context.SaveChangesAsync(); // Save posts to get IDs

            // --- 5. SEED POSTASSETS (Link Posts to Assets) ---
            var postAssets = new List<PostAsset>();
            foreach (var post in posts)
            {
                var numberOfAssets = new Random().Next(1, 4);
                var selectedAssets = assets.OrderBy(x => Guid.NewGuid()).Take(numberOfAssets);

                foreach (var asset in selectedAssets)
                {
                    postAssets.Add(new PostAsset
                    {
                        PostId = post.Id,
                        AssetId = asset.Id,
                        IsDeleted = false
                    });
                }
            }

            await context.PostAssets.AddRangeAsync(postAssets);


            await context.SaveChangesAsync();
        }
    }
}