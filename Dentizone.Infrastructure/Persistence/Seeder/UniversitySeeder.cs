using Dentizone.Domain.Entity;

namespace Dentizone.Infrastructure.Persistence.Seeder
{
    public static class UniversitySeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            var universities = new List<University>
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
        }
    }
}