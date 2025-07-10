using Bogus;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using Dentizone.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Diagnostics;

namespace Dentizone.Infrastructure.Persistence.Seeder
{
    public static class FullDataSeeder
    {
        // Configurable seeding options
        public class SeedingConfig
        {
            public int AdminCount { get; set; } = 6;
            public int UserCount { get; set; } = 0;
            public int AssetCount { get; set; } = 0;
            public int PostCount { get; set; } = 0;
            public int CartCount { get; set; } = 0;
            public int FavouriteCount { get; set; } = 0;
            public int OrderCount { get; set; } = 0;
            public int OrderItemCount { get; set; } = 0;
            public int OrderStatusCount { get; set; } = 0;
            public int OrderPickupCount { get; set; } = 0;
            public int ShipInfoCount { get; set; } = 0;
            public int ReviewCount { get; set; } = 0;
            public int WalletCount { get; set; } = 0;
            public int UserAssetCount { get; set; } = 0;
            public int UserActivityCount { get; set; } = 0;
            public int PaymentCount { get; set; } = 0;
            public int SalesTransactionCount { get; set; } = 0;
            public int WithdrawalRequestCount { get; set; } = 0;
            public int QuestionCount { get; set; } = 0;
            public int AnswerCount { get; set; } = 0;
            public int ShipmentActivityCount { get; set; } = 0;
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
            List<University>? universities = new List<University>();
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


            //// --- 1. SEED ADMIN USERS ---

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


            List<AppUser> appUsers = new List<AppUser>();
            // Add admin AppUsers
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
                    UniversityId = universities?.Find(u => u.Name == "Alexandria University").Id ?? "1",
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
    }

}