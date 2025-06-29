using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using Dentizone.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Dentizone.Infrastructure.Persistence.Seeder
{
    public static class FullDataSeeder
    {
        // Configurable seeding options
        public class SeedingConfig
        {
            public int AdminCount { get; set; } = 6;
            public int UserCount { get; set; } = 1000;
            public int AssetCount { get; set; } = 2000;
            public int PostCount { get; set; } = 1000;
            public int CartCount { get; set; } = 1000;
            public int FavouriteCount { get; set; } = 1000;
            public int OrderCount { get; set; } = 500;
            public int OrderItemCount { get; set; } = 1000;
            public int OrderStatusCount { get; set; } = 1000;
            public int OrderPickupCount { get; set; } = 500;
            public int ShipInfoCount { get; set; } = 500;
            public int ReviewCount { get; set; } = 500;
            public int WalletCount { get; set; } = 1000;
            public int UserAssetCount { get; set; } = 1000;
            public int UserActivityCount { get; set; } = 2000;
            public int PaymentCount { get; set; } = 500;
            public int SalesTransactionCount { get; set; } = 500;
            public int WithdrawalRequestCount { get; set; } = 200;
            public int QuestionCount { get; set; } = 1000;
            public int AnswerCount { get; set; } = 1000;
            public int ShipmentActivityCount { get; set; } = 500;
            public bool ForceReseed { get; set; } = false;
        }

        public static async Task SeedAsync(AppDbContext context, UserManager<ApplicationUser> userManager,
            SeedingConfig? config = null)
        {
            config ??= new SeedingConfig();
            Randomizer.Seed = new Random(8675309);
            var totalSw = Stopwatch.StartNew();

            if (config.ForceReseed)
            {
                Console.WriteLine("[Seeding] ForceReseed is enabled. Deleting all data from all tables...");
                // Order matters: delete children before parents
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [ShipmentActivities]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [Answers]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [Questions]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [UserActivities]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [UserAssets]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [Reviews]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [ShipInfos]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [OrderPickups]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [OrderStatuses]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [OrderItems]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [Orders]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [Favourites]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [Carts]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [PostAssets]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [Posts]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [Assets]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [SalesTransactions]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [Payments]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [WithdrawalRequests]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [Wallets]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [AppUsers]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [SubCategories]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [Categories]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [Universities]");
                // Optionally: delete AspNetUsers, AspNetRoles, AspNetUserRoles, etc. if you want to reseed identity
                Console.WriteLine("[Seeding] All data deleted. Proceeding to reseed...");
            }

            // --- 0. SEED ROLES ---
            var sw = Stopwatch.StartNew();
            var roleManager = context.GetService<RoleManager<IdentityRole>>();
            var roles = Enum.GetNames(typeof(UserRoles));
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            sw.Stop();
            Console.WriteLine($"[Seeding] Roles seeded in {sw.Elapsed.TotalSeconds:F2}s");

            // --- 0.1 SEED UNIVERSITIES ---
            sw.Restart();
            List<University>? universities;
            if (!await context.Universities.AnyAsync())
            {
                universities = new List<University>
                {
                    new() { Name = "Arab Academy for Science & Technology", Domain = "aast.edu" },
                    new() { Name = "Akhbar El Yom Academy", Domain = "akhbaracademy.edu.eg" },
                    new() { Name = "Alexandria University", Domain = "alex.edu.eg" },
                    new() { Name = "Arab Open University", Domain = "aou.edu.eg" },
                    new() { Name = "American University in Cairo", Domain = "aucegypt.edu" },
                    new() { Name = "Assiut University", Domain = "aun.edu.eg" },
                    new() { Name = "Al Azhar University", Domain = "azhar.edu.eg" },
                    new() { Name = "Beni Suef University", Domain = "bsu.edu.eg" },
                    new() { Name = "Benha University", Domain = "bu.edu.eg" },
                    new() { Name = "Cairo University", Domain = "cu.edu.eg" },
                    new() { Name = "Damanhour University", Domain = "damanhour.edu.eg" },
                    new() { Name = "Damietta University", Domain = "du.edu.eg" },
                    new() { Name = "El Shorouk Academy", Domain = "elshoroukacademy.edu.eg" },
                    new() { Name = "Fayoum University", Domain = "fayoum.edu.eg" },
                    new() { Name = "Future University", Domain = "futureuniversity.edu.eg" },
                    new() { Name = "German University in Cairo", Domain = "guc.edu.eg" },
                    new() { Name = "Helwan University", Domain = "helwan.edu.eg" },
                    new() { Name = "Higher Technological Institute", Domain = "hti.edu.eg" },
                    new() { Name = "Kafr El-Sheikh University", Domain = "kfs.edu.eg" },
                    new() { Name = "Mansoura University", Domain = "mans.edu.eg" },
                    new() { Name = "Menoufia University", Domain = "menofia.edu.eg" },
                    new() { Name = "Minia University", Domain = "minia.edu.eg" },
                    new() { Name = "Misr International University", Domain = "miuegypt.edu.eg" },
                    new() { Name = "Modern Acadmy", Domain = "modern-academy.edu.eg" },
                    new() { Name = "Modern Sciences & Arts University", Domain = "msa.eun.eg" },
                    new() { Name = "Military Technical College", Domain = "mtc.edu.eg" },
                    new() { Name = "Modern University For Technology and Information", Domain = "mti.edu.eg" },
                    new() { Name = "Misr University for Sience and Technology", Domain = "must.edu.eg" },
                    new() { Name = "Nile University", Domain = "nileu.edu.eg" },
                    new() { Name = "October 6 university", Domain = "o6u.edu.eg" },
                    new() { Name = "Pharos International University", Domain = "pua.edu.eg" },
                    new() { Name = "Sadat Academy for Management Sciences", Domain = "sadatacademy.edu.eg" },
                    new() { Name = "Ain Shams University", Domain = "shams.edu.eg" },
                    new() { Name = "Sohag University", Domain = "sohag-univ.edu.eg" },
                    new() { Name = "Sinai University", Domain = "su.edu.eg" },
                    new() { Name = "Suez Canal University", Domain = "suez.edu.eg" },
                    new() { Name = "South Valley University", Domain = "svu.edu.eg" },
                    new() { Name = "Tanta University", Domain = "tanta.edu.eg" },
                    new() { Name = "Université Française d'Égypte", Domain = "ufe.edu.eg" },
                    new() { Name = "Université Senghor d'Alexandrie", Domain = "usenghor-francophonie.org" },
                    new() { Name = "Zagazig University", Domain = "zu.edu.eg" },
                    new() { Name = "CIC - Canadian International College", Domain = "cic-cairo.com" },
                    new() { Name = "Deraya University", Domain = "deraya.edu.eg" },
                    new() { Name = "Badr University in Cairo", Domain = "buc.edu.eg" }
                };
                await context.Universities.AddRangeAsync(universities);
                await context.SaveChangesAsync();
                Console.WriteLine($"[Seeding] Universities seeded in {sw.Elapsed.TotalSeconds:F2}s");
            }
            else
            {
                Console.WriteLine("[Seeding] Universities already exist, skipping.");
            }

            // --- 0.2 SEED CATEGORIES & SUBCATEGORIES ---
            sw.Restart();
            List<Category>? categories;
            if (!await context.Categories.AnyAsync())
            {
                var categoryFaker = new Faker<Category>()
                    .RuleFor(c => c.Id, f => Guid.NewGuid().ToString())
                    .RuleFor(c => c.Name, f => f.Commerce.Categories(1)[0])
                    .RuleFor(c => c.IconUrl, f => f.Internet.Avatar())
                    .RuleFor(c => c.CreatedAt, f => f.Date.Past())
                    .RuleFor(c => c.UpdatedAt, f => f.Date.Recent())
                    .RuleFor(c => c.IsDeleted, false);
                categories = categoryFaker.Generate(10);
                var seededSubCategories = new List<SubCategory>();
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
                        .Generate(5);
                    seededSubCategories.AddRange(subCategories);
                }

                await context.Categories.AddRangeAsync(categories);
                await context.SubCategories.AddRangeAsync(seededSubCategories);
                await context.SaveChangesAsync();
                Console.WriteLine($"[Seeding] Categories & SubCategories seeded in {sw.Elapsed.TotalSeconds:F2}s");
            }
            else
            {
                Console.WriteLine("[Seeding] Categories already exist, skipping.");
            }

            // --- PRE-REQUISITES: Fetch existing data for relationships ---
            sw = new Stopwatch();
            sw.Restart();
            universities = await context.Universities.ToListAsync();
            categories = await context.Categories.Include(c => c.SubCategories).ToListAsync();
            if (!universities.Any() || !categories.Any())
                throw new Exception("Seed universities and categories first.");
            var universityIds = universities.Select(u => u.Id).ToList();
            var allSubCategories = categories.SelectMany(c => c.SubCategories).ToList();
            sw.Stop();
            Console.WriteLine($"[Seeding] Pre-requisites loaded in {sw.Elapsed.TotalSeconds:F2}s");

            // --- 1. SEED ADMIN USERS ---
            if (!await context.AppUsers.AnyAsync(u => u.Email.EndsWith("@gitnasr.com")))
            {
                sw.Restart();
                var adminNames = new[] { "Nasr", "Nourhane", "Nouran", "Nourleyy", "Mariam", "Yara" };
                var adminEmails = adminNames.Select(n => $"{n.ToLower()}@gitnasr.com").ToList();
                var adminUsers = new List<ApplicationUser>();
                for (int i = 0; i < config.AdminCount && i < adminNames.Length; i++)
                {
                    var user = new ApplicationUser
                    {
                        UserName = adminEmails[i],
                        Email = adminEmails[i],
                        EmailConfirmed = true
                    };
                    var result = await userManager.CreateAsync(user, "AdminPassword123!");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, UserRoles.Admin.ToString());
                        adminUsers.Add(user);
                    }
                }

                sw.Stop();
                Console.WriteLine($"[Seeding] Admin users seeded in {sw.Elapsed.TotalSeconds:F2}s");
            }
            else
            {
                Console.WriteLine("[Seeding] Admin users already exist, skipping.");
            }

            // --- 2. SEED REGULAR USERS (Identity + AppUser) ---
            if (!await context.AppUsers.AnyAsync())
            {
                sw.Restart();
                var identityUsers = new List<ApplicationUser>();
                var appUsers = new List<AppUser>();
                var userFaker = new Faker();
                for (int i = 0; i < config.UserCount; i++)
                {
                    var email = userFaker.Internet.Email();
                    var identityUser = new ApplicationUser
                    {
                        UserName = email,
                        Email = email,
                        EmailConfirmed = true
                    };
                    var result = await userManager.CreateAsync(identityUser, "UserPassword123!");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(identityUser, UserRoles.Ghost.ToString());
                        identityUsers.Add(identityUser);
                        appUsers.Add(new AppUser
                        {
                            Id = identityUser.Id,
                            FullName = userFaker.Name.FullName(),
                            Username = userFaker.Internet.UserName(),
                            Email = email,
                            AcademicYear = userFaker.Random.Int(1, 5),
                            KycStatus = userFaker.PickRandom<KycStatus>(),
                            Status = userFaker.PickRandom<UserState>(),
                            UniversityId = userFaker.PickRandom(universityIds),
                            IsDeleted = false,
                            CreatedAt = userFaker.Date.Past(2),
                            UpdatedAt = userFaker.Date.Recent()
                        });
                    }
                }

                // Add admin AppUsers
                var adminNames = new[] { "Nasr", "Nourhane", "Nouran", "Nourleyy", "Mariam", "Yara" };
                var adminEmails = adminNames.Select(n => $"{n.ToLower()}@gitnasr.com").ToList();
                var adminUsers = await userManager.Users.Where(u => adminEmails.Contains(u.Email)).ToListAsync();
                for (int i = 0; i < adminUsers.Count; i++)
                {
                    appUsers.Add(new AppUser
                    {
                        Id = adminUsers[i].Id,
                        FullName = adminNames[i],
                        Username = adminNames[i],
                        Email = adminEmails[i],
                        AcademicYear = 5,
                        KycStatus = KycStatus.Approved,
                        Status = UserState.Active,
                        UniversityId = universityIds[i % universityIds.Count],
                        IsDeleted = false,
                        CreatedAt = DateTime.UtcNow.AddYears(-1),
                        UpdatedAt = DateTime.UtcNow
                    });
                }

                await context.AppUsers.AddRangeAsync(appUsers);
                await context.SaveChangesAsync();
                sw.Stop();
                Console.WriteLine($"[Seeding] Regular users seeded in {sw.Elapsed.TotalSeconds:F2}s");
            }
            else
            {
                Console.WriteLine("[Seeding] AppUsers already exist, skipping.");
            }

            // --- 3. SEED WALLETS ---
            if (!await context.Wallets.AnyAsync())
            {
                sw.Restart();
                // One wallet per user
                var allUserIds = await context.AppUsers.Select(u => u.Id).ToListAsync();
                var walletFaker = new Faker<Wallet>()
                    .RuleFor(w => w.Balance, f => f.Finance.Amount(0, 10000))
                    .RuleFor(w => w.Status, f => f.PickRandom<UserWallet>())
                    .RuleFor(w => w.CreatedAt, f => f.Date.Past(2))
                    .RuleFor(w => w.UpdatedAt, f => f.Date.Recent());
                var wallets = allUserIds.Select(userId =>
                {
                    var wallet = walletFaker.Generate();
                    wallet.UserId = userId;
                    return wallet;
                }).ToList();
                await context.Wallets.AddRangeAsync(wallets);
                await context.SaveChangesAsync();
                sw.Stop();
                Console.WriteLine($"[Seeding] Wallets seeded in {sw.Elapsed.TotalSeconds:F2}s");
            }
            else
            {
                Console.WriteLine("[Seeding] Wallets already exist, skipping.");
            }

            // --- 4. SEED ASSETS ---
            if (!await context.Assets.AnyAsync())
            {
                sw.Restart();
                var allUserIds = await context.AppUsers.Select(u => u.Id).ToListAsync();
                if (allUserIds.Count == 0)
                {
                    Console.WriteLine("[Seeding] No users found for asset seeding, skipping.");
                }
                else
                {
                    var assetFaker = new Faker<Asset>()
                        .RuleFor(a => a.Url, f => f.Image.PicsumUrl())
                        .RuleFor(a => a.Type, f => f.PickRandom<AssetType>())
                        .RuleFor(a => a.Status, f => f.PickRandom<AssetStatus>())
                        .RuleFor(a => a.IsDeleted, false)
                        .RuleFor(a => a.UserId, f => f.PickRandom(allUserIds))
                        .RuleFor(a => a.Size, f => f.Random.Long(10000, 10000000))
                        .RuleFor(a => a.CreatedAt, f => f.Date.Past(2))
                        .RuleFor(a => a.UpdatedAt, f => f.Date.Recent());
                    var assets = assetFaker.Generate(config.AssetCount);
                    assets = assets.Where(a => !string.IsNullOrEmpty(a.UserId) && allUserIds.Contains(a.UserId))
                        .ToList();
                    await context.Assets.AddRangeAsync(assets);
                    await context.SaveChangesAsync();
                    sw.Stop();
                    Console.WriteLine($"[Seeding] Assets seeded in {sw.Elapsed.TotalSeconds:F2}s");
                }
            }
            else
            {
                Console.WriteLine("[Seeding] Assets already exist, skipping.");
            }

            // --- 5. SEED POSTS ---
            if (!await context.Posts.AnyAsync())
            {
                sw.Restart();
                var allUserIds = await context.AppUsers.Select(u => u.Id).ToListAsync();
                var allCategoryIds = await context.Categories.Select(c => c.Id).ToListAsync();
                var allSubCategoryIds = await context.SubCategories.Select(sc => sc.Id).ToListAsync();
                if (allUserIds.Count == 0 || allCategoryIds.Count == 0 || allSubCategoryIds.Count == 0)
                {
                    Console.WriteLine("[Seeding] Missing users/categories/subcategories for post seeding, skipping.");
                }
                else
                {
                    var postFaker = new Faker<Post>()
                        .RuleFor(p => p.SellerId, f => f.PickRandom(allUserIds))
                        .RuleFor(p => p.Title, f => f.Commerce.ProductName())
                        .RuleFor(p => p.Description, f => f.Lorem.Paragraphs(2))
                        .RuleFor(p => p.Price, f => f.Random.Decimal(50, 1000))
                        .RuleFor(p => p.Condition, f => f.PickRandom<PostItemCondition>())
                        .RuleFor(p => p.Status, f => f.PickRandom<PostStatus>())
                        .RuleFor(p => p.IsDeleted, false)
                        .RuleFor(p => p.City, f => f.Address.City())
                        .RuleFor(p => p.Street, f => f.Address.StreetAddress())
                        .RuleFor(p => p.CreatedAt, f => f.Date.Past(1))
                        .RuleFor(p => p.UpdatedAt, f => f.Date.Recent())
                        .RuleFor(p => p.ExpireDate, f => f.Date.Future(1))
                        .RuleFor(p => p.Slug, (f, p) => f.Lorem.Slug())
                        .RuleFor(p => p.CategoryId, f => f.PickRandom(allCategoryIds))
                        .RuleFor(p => p.SubCategoryId, f => f.PickRandom(allSubCategoryIds));
                    var posts = postFaker.Generate(config.PostCount);
                    posts = posts.Where(p => !string.IsNullOrEmpty(p.SellerId) && allUserIds.Contains(p.SellerId)
                                                                               && !string.IsNullOrEmpty(p.CategoryId) &&
                                                                               allCategoryIds.Contains(p.CategoryId)
                                                                               && !string.IsNullOrEmpty(
                                                                                   p.SubCategoryId) &&
                                                                               allSubCategoryIds.Contains(
                                                                                   p.SubCategoryId)).ToList();
                    await context.Posts.AddRangeAsync(posts);
                    await context.SaveChangesAsync();
                    sw.Stop();
                    Console.WriteLine($"[Seeding] Posts seeded in {sw.Elapsed.TotalSeconds:F2}s");
                }
            }
            else
            {
                Console.WriteLine("[Seeding] Posts already exist, skipping.");
            }

            // --- 6. SEED POSTASSETS ---
            if (!await context.PostAssets.AnyAsync())
            {
                sw.Restart();
                var allPostIds = await context.Posts.Select(p => p.Id).ToListAsync();
                var allAssetIds = await context.Assets.Select(a => a.Id).ToListAsync();
                if (allPostIds.Count == 0 || allAssetIds.Count == 0)
                {
                    Console.WriteLine("[Seeding] Missing posts/assets for postasset seeding, skipping.");
                }
                else
                {
                    var userFaker = new Faker();
                    var postAssets = new List<PostAsset>();
                    foreach (var postId in allPostIds)
                    {
                        var numberOfAssets = userFaker.Random.Int(1, 3);
                        var selectedAssets = allAssetIds.OrderBy(x => Guid.NewGuid()).Take(numberOfAssets);
                        foreach (var assetId in selectedAssets)
                        {
                            postAssets.Add(new PostAsset
                            {
                                PostId = postId,
                                AssetId = assetId,
                                IsDeleted = false,
                                CreatedAt = userFaker.Date.Past(1),
                                UpdatedAt = userFaker.Date.Recent()
                            });
                        }
                    }

                    postAssets = postAssets.Where(pa => !string.IsNullOrEmpty(pa.PostId) &&
                                                        allPostIds.Contains(pa.PostId)
                                                        && !string.IsNullOrEmpty(pa.AssetId) &&
                                                        allAssetIds.Contains(pa.AssetId)).ToList();
                    await context.PostAssets.AddRangeAsync(postAssets);
                    await context.SaveChangesAsync();
                    sw.Stop();
                    Console.WriteLine($"[Seeding] PostAssets seeded in {sw.Elapsed.TotalSeconds:F2}s");
                }
            }
            else
            {
                Console.WriteLine("[Seeding] PostAssets already exist, skipping.");
            }

            // --- 7. SEED CARTS ---
            if (!await context.Carts.AnyAsync())
            {
                sw.Restart();
                var allUserIds = await context.AppUsers.Select(u => u.Id).ToListAsync();
                var allPostIds = await context.Posts.Select(p => p.Id).ToListAsync();
                if (allUserIds.Count == 0 || allPostIds.Count == 0)
                {
                    Console.WriteLine("[Seeding] Missing users/posts for cart seeding, skipping.");
                }
                else
                {
                    var cartFaker = new Faker<Cart>()
                        .RuleFor(c => c.UserId, f => f.PickRandom(allUserIds))
                        .RuleFor(c => c.PostId, f => f.PickRandom(allPostIds))
                        .RuleFor(c => c.CreatedAt, f => f.Date.Recent())
                        .RuleFor(c => c.IsDeleted, false);
                    var carts = cartFaker.Generate(config.CartCount);
                    carts = carts.Where(c => !string.IsNullOrEmpty(c.UserId) && allUserIds.Contains(c.UserId)
                                                                             && !string.IsNullOrEmpty(c.PostId) &&
                                                                             allPostIds.Contains(c.PostId)).ToList();
                    await context.Carts.AddRangeAsync(carts);
                    await context.SaveChangesAsync();
                    sw.Stop();
                    Console.WriteLine($"[Seeding] Carts seeded in {sw.Elapsed.TotalSeconds:F2}s");
                }
            }
            else
            {
                Console.WriteLine("[Seeding] Carts already exist, skipping.");
            }

            // --- 8. SEED FAVOURITES ---
            if (!await context.Favourites.AnyAsync())
            {
                sw.Restart();
                var allUserIds = await context.AppUsers.Select(u => u.Id).ToListAsync();
                var allPostIds = await context.Posts.Select(p => p.Id).ToListAsync();
                var favFaker = new Faker<Favourite>()
                    .RuleFor(fv => fv.UserId, f => f.PickRandom(allUserIds))
                    .RuleFor(fv => fv.PostId, f => f.PickRandom(allPostIds))
                    .RuleFor(fv => fv.CreatedAt, f => f.Date.Recent())
                    .RuleFor(fv => fv.IsDeleted, false);
                var favourites = favFaker.Generate(config.FavouriteCount);
                await context.Favourites.AddRangeAsync(favourites);
                await context.SaveChangesAsync();
                sw.Stop();
                Console.WriteLine($"[Seeding] Favourites seeded in {sw.Elapsed.TotalSeconds:F2}s");
            }
            else
            {
                Console.WriteLine("[Seeding] Favourites already exist, skipping.");
            }

            // --- 9. SEED ORDERS ---
            if (!await context.Orders.AnyAsync())
            {
                sw.Restart();
                var allUserIds = await context.AppUsers.Select(u => u.Id).ToListAsync();
                var orderFaker = new Faker<Order>()
                    .RuleFor(o => o.BuyerId, f => f.PickRandom(allUserIds))
                    .RuleFor(o => o.CommissionAmount, f => f.Random.Decimal(1, 100))
                    .RuleFor(o => o.TotalAmount, f => f.Random.Decimal(50, 1000))
                    .RuleFor(o => o.CreatedAt, f => f.Date.Past(1))
                    .RuleFor(o => o.UpdatedAt, f => f.Date.Recent())
                    .RuleFor(o => o.IsDeleted, false);
                var orders = orderFaker.Generate(config.OrderCount);
                await context.Orders.AddRangeAsync(orders);
                await context.SaveChangesAsync();
                sw.Stop();
                Console.WriteLine($"[Seeding] Orders seeded in {sw.Elapsed.TotalSeconds:F2}s");
            }
            else
            {
                Console.WriteLine("[Seeding] Orders already exist, skipping.");
            }

            // --- 10. SEED ORDER ITEMS ---
            if (!await context.OrderItems.AnyAsync())
            {
                sw.Restart();
                var allOrderIds = await context.Orders.Select(o => o.Id).ToListAsync();
                var allPostIds = await context.Posts.Select(p => p.Id).ToListAsync();
                var orderItemFaker = new Faker<OrderItem>()
                    .RuleFor(oi => oi.OrderId, f => f.PickRandom(allOrderIds))
                    .RuleFor(oi => oi.PostId, f => f.PickRandom(allPostIds))
                    .RuleFor(oi => oi.CreatedAt, f => f.Date.Recent());
                var orderItems = orderItemFaker.Generate(config.OrderItemCount);
                await context.OrderItems.AddRangeAsync(orderItems);
                await context.SaveChangesAsync();
                sw.Stop();
                Console.WriteLine($"[Seeding] OrderItems seeded in {sw.Elapsed.TotalSeconds:F2}s");
            }
            else
            {
                Console.WriteLine("[Seeding] OrderItems already exist, skipping.");
            }

            // --- 11. SEED ORDER STATUS ---
            if (!await context.OrderStatuses.AnyAsync())
            {
                sw.Restart();
                var allOrderIds = await context.Orders.Select(o => o.Id).ToListAsync();
                var orderStatusFaker = new Faker<OrderStatus>()
                    .RuleFor(os => os.OrderId, f => f.PickRandom(allOrderIds))
                    .RuleFor(os => os.Status, f => f.PickRandom<OrderStatues>())
                    .RuleFor(os => os.CreatedAt, f => f.Date.Recent())
                    .RuleFor(os => os.Comment, f => f.Lorem.Sentence());
                var orderStatuses = orderStatusFaker.Generate(config.OrderStatusCount);
                await context.OrderStatuses.AddRangeAsync(orderStatuses);
                await context.SaveChangesAsync();
                sw.Stop();
                Console.WriteLine($"[Seeding] OrderStatuses seeded in {sw.Elapsed.TotalSeconds:F2}s");
            }
            else
            {
                Console.WriteLine("[Seeding] OrderStatuses already exist, skipping.");
            }

            // --- 12. SEED ORDER PICKUPS ---
            if (!await context.OrderPickups.AnyAsync())
            {
                sw.Restart();
                var allOrderIds = await context.Orders.Select(o => o.Id).ToListAsync();
                var orderPickupFaker = new Faker<OrderPickup>()
                    .RuleFor(op => op.OrderId, f => f.PickRandom(allOrderIds))
                    .RuleFor(op => op.Street, f => f.Address.StreetAddress())
                    .RuleFor(op => op.City, f => f.Address.City())
                    .RuleFor(op => op.CreatedAt, f => f.Date.Past(1))
                    .RuleFor(op => op.UpdatedAt, f => f.Date.Recent())
                    .RuleFor(op => op.IsDeleted, false);
                var orderPickups = orderPickupFaker.Generate(config.OrderPickupCount);
                await context.OrderPickups.AddRangeAsync(orderPickups);
                await context.SaveChangesAsync();
                sw.Stop();
                Console.WriteLine($"[Seeding] OrderPickups seeded in {sw.Elapsed.TotalSeconds:F2}s");
            }
            else
            {
                Console.WriteLine("[Seeding] OrderPickups already exist, skipping.");
            }

            // --- 13. SEED SHIP INFO ---
            if (!await context.ShipInfos.AnyAsync())
            {
                sw.Restart();
                var allOrderIds = await context.Orders.Select(o => o.Id).ToListAsync();
                var allUserIds = await context.AppUsers.Select(u => u.Id).ToListAsync();
                var shipInfoFaker = new Faker<ShipInfo>()
                    .RuleFor(si => si.OrderId, f => f.PickRandom(allOrderIds))
                    .RuleFor(si => si.UserId, f => f.PickRandom(allUserIds))
                    .RuleFor(si => si.Street, f => f.Address.StreetAddress())
                    .RuleFor(si => si.City, f => f.Address.City())
                    .RuleFor(si => si.CreatedAt, f => f.Date.Past(1));
                var shipInfos = shipInfoFaker.Generate(config.ShipInfoCount);
                // Ensure unique OrderId (skip duplicates)
                var uniqueShipInfos = shipInfos
                    .GroupBy(si => si.OrderId)
                    .Select(g => g.First())
                    .ToList();
                await context.ShipInfos.AddRangeAsync(uniqueShipInfos);
                await context.SaveChangesAsync();
                sw.Stop();
                Console.WriteLine($"[Seeding] ShipInfos seeded in {sw.Elapsed.TotalSeconds:F2}s");
            }
            else
            {
                Console.WriteLine("[Seeding] ShipInfos already exist, skipping.");
            }

            // --- 14. SEED REVIEWS ---
            if (!await context.Reviews.AnyAsync())
            {
                sw.Restart();
                var allUserIds = await context.AppUsers.Select(u => u.Id).ToListAsync();
                var allOrderIds = await context.Orders.Select(o => o.Id).ToListAsync();
                var reviewFaker = new Faker<Review>()
                    .RuleFor(r => r.UserId, f => f.PickRandom(allUserIds))
                    .RuleFor(r => r.OrderId, f => f.PickRandom(allOrderIds))
                    .RuleFor(r => r.Stars, f => f.Random.Int(1, 5))
                    .RuleFor(r => r.Text, f => f.Lorem.Sentence())
                    .RuleFor(r => r.CreatedAt, f => f.Date.Past(1))
                    .RuleFor(r => r.UpdatedAt, f => f.Date.Recent())
                    .RuleFor(r => r.IsDeleted, false);
                var reviews = reviewFaker.Generate(config.ReviewCount);
                // Ensure unique OrderId (skip duplicates)
                var uniqueReviews = reviews
                    .GroupBy(r => r.OrderId)
                    .Select(g => g.First())
                    .ToList();
                await context.Reviews.AddRangeAsync(uniqueReviews);
                await context.SaveChangesAsync();
                sw.Stop();
                Console.WriteLine($"[Seeding] Reviews seeded in {sw.Elapsed.TotalSeconds:F2}s");
            }
            else
            {
                Console.WriteLine("[Seeding] Reviews already exist, skipping.");
            }

            // --- 15. SEED USER ASSETS ---
            if (!await context.UserAssets.AnyAsync())
            {
                sw.Restart();
                var allUserIds = await context.AppUsers.Select(u => u.Id).ToListAsync();
                var allAssets = await context.Assets.ToListAsync();
                var allAssetIds = allAssets.Select(a => a.Id).ToList();
                var userAssetFaker = new Faker<UserAsset>()
                    .RuleFor(ua => ua.AssetId, f => f.PickRandom(allAssetIds))
                    .RuleFor(ua => ua.Type, f => f.PickRandom<UserAssetsType>())
                    .RuleFor(ua => ua.CreatedAt, f => f.Date.Past(1))
                    .RuleFor(ua => ua.IsDeleted, false);
                var userAssets = new List<UserAsset>();
                for (int i = 0; i < config.UserAssetCount; i++)
                {
                    var ua = userAssetFaker.Generate();
                    var asset = allAssets.FirstOrDefault(a => a.Id == ua.AssetId);
                    if (asset != null && !string.IsNullOrEmpty(asset.UserId))
                    {
                        ua.UserId = asset.UserId;
                        userAssets.Add(ua);
                    }
                }

                await context.UserAssets.AddRangeAsync(userAssets);
                await context.SaveChangesAsync();
                sw.Stop();
                Console.WriteLine($"[Seeding] UserAssets seeded in {sw.Elapsed.TotalSeconds:F2}s");
            }
            else
            {
                Console.WriteLine("[Seeding] UserAssets already exist, skipping.");
            }

            // --- 16. SEED USER ACTIVITIES ---
            if (!await context.UserActivities.AnyAsync())
            {
                sw.Restart();
                var allUserIds = await context.AppUsers.Select(u => u.Id).ToListAsync();
                var userActivityFaker = new Faker<UserActivity>()
                    .RuleFor(ua => ua.UserId, f => f.PickRandom(allUserIds))
                    .RuleFor(ua => ua.FingerprintToken, f => f.Random.Hash())
                    .RuleFor(ua => ua.Device, f => f.Commerce.ProductName())
                    .RuleFor(ua => ua.UserAgent, f => f.Internet.UserAgent())
                    .RuleFor(ua => ua.DetectedAt, f => f.Date.Recent())
                    .RuleFor(ua => ua.IpAddress, f => f.Internet.Ip())
                    .RuleFor(ua => ua.ActivityType, f => f.PickRandom<UserActivities>())
                    .RuleFor(ua => ua.CreatedAt, f => f.Date.Recent());
                var userActivities = userActivityFaker.Generate(config.UserActivityCount);
                await context.UserActivities.AddRangeAsync(userActivities);
                await context.SaveChangesAsync();
                sw.Stop();
                Console.WriteLine($"[Seeding] UserActivities seeded in {sw.Elapsed.TotalSeconds:F2}s");
            }
            else
            {
                Console.WriteLine("[Seeding] UserActivities already exist, skipping.");
            }

            // --- 17. SEED PAYMENTS ---
            if (!await context.Payments.AnyAsync())
            {
                sw.Restart();
                var allOrders = await context.Orders.ToListAsync();
                var allOrderIds = allOrders.Select(o => o.Id).ToList();
                var paymentFaker = new Faker<Payment>()
                    .RuleFor(p => p.OrderId, f => f.PickRandom(allOrderIds))
                    .RuleFor(p => p.Amount, f => f.Random.Decimal(50, 1000))
                    .RuleFor(p => p.Method, f => f.PickRandom<PaymentMethod>())
                    .RuleFor(p => p.Status, f => f.PickRandom<PaymentStatus>())
                    .RuleFor(p => p.CreatedAt, f => f.Date.Past(1))
                    .RuleFor(p => p.UpdatedAt, f => f.Date.Recent());
                var payments = new List<Payment>();
                for (int i = 0; i < config.PaymentCount; i++)
                {
                    var payment = paymentFaker.Generate();
                    var order = allOrders.FirstOrDefault(o => o.Id == payment.OrderId);
                    if (order != null && !string.IsNullOrEmpty(order.BuyerId))
                    {
                        payment.BuyerId = order.BuyerId;
                        payments.Add(payment);
                    }
                }

                await context.Payments.AddRangeAsync(payments);
                await context.SaveChangesAsync();
                sw.Stop();
                Console.WriteLine($"[Seeding] Payments seeded in {sw.Elapsed.TotalSeconds:F2}s");
            }
            else
            {
                Console.WriteLine("[Seeding] Payments already exist, skipping.");
            }

            // --- 18. SEED SALES TRANSACTIONS ---
            if (!await context.SalesTransactions.AnyAsync())
            {
                sw.Restart();
                var allPayments = await context.Payments.ToListAsync();
                var allPaymentIds = allPayments.Select(p => p.Id).ToList();
                var allWallets = await context.Wallets.ToListAsync();
                var salesTransactionFaker = new Faker<SalesTransaction>()
                    .RuleFor(st => st.PaymentId, f => f.PickRandom(allPaymentIds))
                    .RuleFor(st => st.Amount, f => f.Random.Decimal(10, 1000))
                    .RuleFor(st => st.Status, f => f.PickRandom<SaleStatus>())
                    .RuleFor(st => st.CreatedAt, f => f.Date.Past(1))
                    .RuleFor(st => st.UpdatedAt, f => f.Date.Recent());
                var salesTransactions = new List<SalesTransaction>();
                for (int i = 0; i < config.SalesTransactionCount; i++)
                {
                    var st = salesTransactionFaker.Generate();
                    var payment = allPayments.FirstOrDefault(p => p.Id == st.PaymentId);
                    if (payment != null && !string.IsNullOrEmpty(payment.BuyerId))
                    {
                        var wallet = allWallets.FirstOrDefault(w => w.UserId == payment.BuyerId);
                        if (wallet != null)
                        {
                            st.WalletId = wallet.Id;
                            salesTransactions.Add(st);
                        }
                    }
                }

                await context.SalesTransactions.AddRangeAsync(salesTransactions);
                await context.SaveChangesAsync();
                sw.Stop();
                Console.WriteLine($"[Seeding] SalesTransactions seeded in {sw.Elapsed.TotalSeconds:F2}s");
            }
            else
            {
                Console.WriteLine("[Seeding] SalesTransactions already exist, skipping.");
            }

            // --- 19. SEED WITHDRAWAL REQUESTS ---
            if (!await context.WithdrawalRequests.AnyAsync())
            {
                sw.Restart();
                var allWallets = await context.Wallets.ToListAsync();
                var allWalletIds = allWallets.Select(w => w.Id).ToList();
                var withdrawalRequestFaker = new Faker<WithdrawalRequest>()
                    .RuleFor(wr => wr.WalletId, f => f.PickRandom(allWalletIds))
                    .RuleFor(wr => wr.Amount, f => f.Random.Decimal(10, 1000))
                    .RuleFor(wr => wr.Status, f => f.PickRandom<WithdrawalRequestStatus>())
                    .RuleFor(wr => wr.ProcessingFee, f => f.Random.Decimal(0, 50))
                    .RuleFor(wr => wr.CreatedAt, f => f.Date.Past(1))
                    .RuleFor(wr => wr.UpdatedAt, f => f.Date.Recent())
                    .RuleFor(wr => wr.AdminNotes, f => f.Lorem.Sentence());
                var withdrawalRequests = new List<WithdrawalRequest>();
                for (int i = 0; i < config.WithdrawalRequestCount; i++)
                {
                    var wr = withdrawalRequestFaker.Generate();
                    var wallet = allWallets.FirstOrDefault(w => w.Id == wr.WalletId);
                    if (wallet != null && !string.IsNullOrEmpty(wallet.UserId))
                    {
                        withdrawalRequests.Add(wr);
                    }
                }

                await context.WithdrawalRequests.AddRangeAsync(withdrawalRequests);
                await context.SaveChangesAsync();
                sw.Stop();
                Console.WriteLine($"[Seeding] WithdrawalRequests seeded in {sw.Elapsed.TotalSeconds:F2}s");
            }
            else
            {
                Console.WriteLine("[Seeding] WithdrawalRequests already exist, skipping.");
            }

            // --- 20. SEED QUESTIONS ---
            if (!await context.Questions.AnyAsync())
            {
                sw.Restart();
                var allPostIds = await context.Posts.Select(p => p.Id).ToListAsync();
                var allUserIds = await context.AppUsers.Select(u => u.Id).ToListAsync();
                var questionFaker = new Faker<Question>()
                    .RuleFor(q => q.PostId, f => f.PickRandom(allPostIds))
                    .RuleFor(q => q.AskerId, f => f.PickRandom(allUserIds))
                    .RuleFor(q => q.Text, f => f.Lorem.Sentence())
                    .RuleFor(q => q.Status, f => f.PickRandom<QuestionStatus>())
                    .RuleFor(q => q.CreatedAt, f => f.Date.Past(1))
                    .RuleFor(q => q.UpdatedAt, f => f.Date.Recent())
                    .RuleFor(q => q.IsDeleted, false);
                var questions = questionFaker.Generate(config.QuestionCount);
                // Ensure AskerId and PostId are valid
                var validQuestions = questions
                    .Where(q => allUserIds.Contains(q.AskerId) && allPostIds.Contains(q.PostId))
                    .ToList();
                await context.Questions.AddRangeAsync(validQuestions);
                await context.SaveChangesAsync();
                sw.Stop();
                Console.WriteLine($"[Seeding] Questions seeded in {sw.Elapsed.TotalSeconds:F2}s");
            }
            else
            {
                Console.WriteLine("[Seeding] Questions already exist, skipping.");
            }

            // --- 21. SEED ANSWERS ---
            if (!await context.Answers.AnyAsync())
            {
                sw.Restart();
                var allQuestionIds = await context.Questions.Select(q => q.Id).ToListAsync();
                var answerFaker = new Faker<Answer>()
                    .RuleFor(a => a.QuestionId, f => f.PickRandom(allQuestionIds))
                    .RuleFor(a => a.Text, f => f.Lorem.Paragraph())
                    .RuleFor(a => a.Status, f => f.PickRandom<AnswerStatus>())
                    .RuleFor(a => a.CreatedAt, f => f.Date.Past(1))
                    .RuleFor(a => a.UpdatedAt, f => f.Date.Recent())
                    .RuleFor(a => a.IsDeleted, false);
                var answers = answerFaker.Generate(config.AnswerCount);
                // Ensure unique and valid QuestionId (skip duplicates)
                var uniqueAnswers = answers
                    .Where(a => !string.IsNullOrEmpty(a.QuestionId) && allQuestionIds.Contains(a.QuestionId))
                    .GroupBy(a => a.QuestionId)
                    .Select(g => g.First())
                    .ToList();
                await context.Answers.AddRangeAsync(uniqueAnswers);
                await context.SaveChangesAsync();
                sw.Stop();
                Console.WriteLine($"[Seeding] Answers seeded in {sw.Elapsed.TotalSeconds:F2}s");
            }
            else
            {
                Console.WriteLine("[Seeding] Answers already exist, skipping.");
            }

            // --- 22. SEED SHIPMENT ACTIVITIES ---
            if (!await context.ShipmentActivities.AnyAsync())
            {
                sw.Restart();
                var allOrderItems = await context.OrderItems.ToListAsync();
                var allItemIds = allOrderItems.Select(oi => oi.Id).ToList();
                var shipmentActivityFaker = new Faker<ShipmentActivity>()
                    .RuleFor(sa => sa.Status, f => f.PickRandom<ShipmentActivityStatus>())
                    .RuleFor(sa => sa.ActivityDescription, f => f.Lorem.Sentence())
                    .RuleFor(sa => sa.CreatedAt, f => f.Date.Past(1));
                var shipmentActivities = new List<ShipmentActivity>();
                // Ensure every OrderItem has at least one ShipmentActivity
                foreach (var item in allOrderItems)
                {
                    var sa = shipmentActivityFaker.Generate();
                    sa.ItemId = item.Id;
                    shipmentActivities.Add(sa);
                }

                // Add more random activities if needed
                int remaining = config.ShipmentActivityCount - shipmentActivities.Count;
                if (remaining > 0)
                {
                    var extraActivities = new List<ShipmentActivity>();
                    for (int i = 0; i < remaining; i++)
                    {
                        var sa = shipmentActivityFaker.Generate();
                        sa.ItemId = allItemIds[new Random().Next(allItemIds.Count)];
                        extraActivities.Add(sa);
                    }

                    shipmentActivities.AddRange(extraActivities);
                }

                await context.ShipmentActivities.AddRangeAsync(shipmentActivities);
                await context.SaveChangesAsync();
                sw.Stop();
                Console.WriteLine($"[Seeding] ShipmentActivities seeded in {sw.Elapsed.TotalSeconds:F2}s");
            }
            else
            {
                Console.WriteLine("[Seeding] ShipmentActivities already exist, skipping.");
            }

            totalSw.Stop();
            Console.WriteLine($"[Seeding] TOTAL seeding time: {totalSw.Elapsed.TotalSeconds:F2}s");
        }
    }
}