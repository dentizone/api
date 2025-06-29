using Bogus;
using Dentizone.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Infrastructure.Persistence.Seeder;

public static class CatalogSeeder
{
    public static async Task SeedCategoriesAndSubCategoriesAsync(AppDbContext context, int categoryCount = 10,
        int subCategoryPerCategory = 5)
    {
        if (await context.Categories.AnyAsync())
            return; // Already seeded

        // Generate Categories with random IconUrl
        var categoryFaker = new Faker<Category>()
            .RuleFor(c => c.Id, f => Guid.NewGuid().ToString())
            .RuleFor(c => c.Name, f => f.Commerce.Categories(1)[0])
            .RuleFor(c => c.IconUrl, f => f.Internet.Avatar())
            .RuleFor(c => c.CreatedAt, f => f.Date.Past())
            .RuleFor(c => c.UpdatedAt, f => f.Date.Recent())
            .RuleFor(c => c.IsDeleted, false);

        var categories = categoryFaker.Generate(categoryCount);

        // Generate SubCategories for each Category, ensuring CategoryId is set
        var subCategoryFaker = new Faker<SubCategory>()
            .RuleFor(sc => sc.Id, f => Guid.NewGuid().ToString())
            .RuleFor(sc => sc.Name, f => f.Commerce.Department())
            .RuleFor(sc => sc.CreatedAt, f => f.Date.Past())
            .RuleFor(sc => sc.UpdatedAt, f => f.Date.Recent())
            .RuleFor(sc => sc.IsDeleted, false);

        foreach (var category in categories)
        {
            var subCategories = subCategoryFaker.Clone()
                .RuleFor(sc => sc.CategoryId, _ => category.Id)
                .Generate(subCategoryPerCategory);


            context.SubCategories.AddRange(subCategories);
        }

        context.Categories.AddRange(categories);
        await context.SaveChangesAsync();
    }
}