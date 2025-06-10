using Dentizone.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Infrastructure.Persistence.Seeder
{
    public static class UniversitySeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            await context.Database.ExecuteSqlRawAsync("DELETE FROM [Universities]");


            var universities = new List<University>
                               {
                                   new University
                                   { Name = "Arab Academy for Science & Technology", Domain = "aast.edu" },
                                   new University { Name = "Akhbar El Yom Academy", Domain = "akhbaracademy.edu.eg" },
                                   new University { Name = "Alexandria University", Domain = "alex.edu.eg" },
                                   new University { Name = "Arab Open University", Domain = "aou.edu.eg" },
                                   new University { Name = "American University in Cairo", Domain = "aucegypt.edu" },
                                   new University { Name = "Assiut University", Domain = "aun.edu.eg" },
                                   new University { Name = "Al Azhar University", Domain = "azhar.edu.eg" },
                                   new University { Name = "Beni Suef University", Domain = "bsu.edu.eg" },
                                   new University { Name = "Benha University", Domain = "bu.edu.eg" },
                                   new University { Name = "Cairo University", Domain = "cu.edu.eg" },
                                   new University { Name = "Damanhour University", Domain = "damanhour.edu.eg" },
                                   new University { Name = "Damietta University", Domain = "du.edu.eg" },
                                   new University { Name = "El Shorouk Academy", Domain = "elshoroukacademy.edu.eg" },
                                   new University { Name = "Fayoum University", Domain = "fayoum.edu.eg" },
                                   new University { Name = "Future University", Domain = "futureuniversity.edu.eg" },
                                   new University { Name = "German University in Cairo", Domain = "guc.edu.eg" },
                                   new University { Name = "Helwan University", Domain = "helwan.edu.eg" },
                                   new University { Name = "Higher Technological Institute", Domain = "hti.edu.eg" },
                                   new University { Name = "Kafr El-Sheikh University", Domain = "kfs.edu.eg" },
                                   new University { Name = "Mansoura University", Domain = "mans.edu.eg" },
                                   new University { Name = "Menoufia University", Domain = "menofia.edu.eg" },
                                   new University { Name = "Minia University", Domain = "minia.edu.eg" },
                                   new University
                                   { Name = "Misr International University", Domain = "miuegypt.edu.eg" },
                                   new University { Name = "Modern Acadmy", Domain = "modern-academy.edu.eg" },
                                   new University { Name = "Modern Sciences & Arts University", Domain = "msa.eun.eg" },
                                   new University { Name = "Military Technical College", Domain = "mtc.edu.eg" },
                                   new University
                                   { Name = "Modern University For Technology and Information", Domain = "mti.edu.eg" },
                                   new University
                                   { Name = "Misr University for Sience and Technology", Domain = "must.edu.eg" },
                                   new University { Name = "Nile University", Domain = "nileu.edu.eg" },
                                   new University { Name = "October 6 university", Domain = "o6u.edu.eg" },
                                   new University { Name = "Pharos International University", Domain = "pua.edu.eg" },
                                   new University
                                   { Name = "Sadat Academy for Management Sciences", Domain = "sadatacademy.edu.eg" },
                                   new University { Name = "Ain Shams University", Domain = "shams.edu.eg" },
                                   new University { Name = "Sohag University", Domain = "sohag-univ.edu.eg" },
                                   new University { Name = "Sinai University", Domain = "su.edu.eg" },
                                   new University { Name = "Suez Canal University", Domain = "suez.edu.eg" },
                                   new University { Name = "South Valley University", Domain = "svu.edu.eg" },
                                   new University { Name = "Tanta University", Domain = "tanta.edu.eg" },
                                   new University { Name = "Université Française d'Égypte", Domain = "ufe.edu.eg" },
                                   new University
                                   { Name = "Université Senghor d'Alexandrie", Domain = "usenghor-francophonie.org" },
                                   new University { Name = "Zagazig University", Domain = "zu.edu.eg" },
                                   new University
                                   { Name = "CIC - Canadian International College", Domain = "cic-cairo.com" },
                                   new University { Name = "Deraya University", Domain = "deraya.edu.eg" },
                                   new University { Name = "Badr University in Cairo", Domain = "buc.edu.eg" }
                               };

            await context.Universities.AddRangeAsync(universities);
            await context.SaveChangesAsync();
        }
    }
}