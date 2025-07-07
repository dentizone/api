using Dentizone.Application.DI;
using Dentizone.Application.Interfaces;
using Dentizone.Application.Services;
using Dentizone.Infrastructure.DependencyInjection;
using Dentizone.Infrastructure.Filters;
using Dentizone.Presentaion.Context;
using Dentizone.Presentaion.Extensions;
using Dentizone.Presentaion.Middlewares;
using Hangfire;
using Scalar.AspNetCore;

namespace Dentizone.Presentaion
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers((options) => options.Filters.Add<BlacklistAuthenticationFilter>());
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddInfrastructure();
            builder.Services.AddAutoMapper(typeof(Application.IAssemblyReference).Assembly);

            builder.Services.AddApplicationServices();
            builder.Services.AddCors(c =>
            {
                c.AddDefaultPolicy(d => d.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IRequestContextService, RequestContextService>();
            builder.Services.AddSwaggerWithJwt();
            builder.Services.ValidateAllDependencies([typeof(BaseService).Assembly]);


            var app = builder.Build();
            app.UseCors();
            // Configure the HTTP request pipeline.
            app.UseSwagger(opt => { opt.RouteTemplate = "openapi/{documentName}.json"; });
            app.MapScalarApiReference(opt =>
            {
                opt.Title = "Dentizone API";
                opt.Theme = ScalarTheme.Mars;
            });

            app.MapGet("/", context =>
            {
                context.Response.Redirect("/scalar", permanent: true);
                return Task.CompletedTask;
            });

            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            Console.WriteLine($"Current ENV:{Environment.GetEnvironmentVariable("ENV")}");

            if (Environment.GetEnvironmentVariable("ENV") != "prod")
            {
                // SEEDING: Run full data seeder if DB is empty
                using (var scope = app.Services.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<Infrastructure.AppDbContext>();
                    var userManager = scope.ServiceProvider
                        .GetRequiredService<Microsoft.AspNetCore.Identity.UserManager<
                            Infrastructure.Identity.ApplicationUser>>();
                    try
                    {
                        Console.WriteLine("[Seeding] Starting full data seeding...");
                        Infrastructure.Persistence.Seeder.FullDataSeeder.SeedingConfig
                            config = new(); // Use default or customize
                        Infrastructure.Persistence.Seeder.FullDataSeeder.SeedAsync(db, userManager, config)
                            .GetAwaiter().GetResult();
                        Console.WriteLine("[Seeding] Full data seeding completed.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[Seeding] Error: {ex.Message}");
                    }
                }
            }

            app.UseHangfireDashboard("/background");

            app.Run();
        }
    }
}