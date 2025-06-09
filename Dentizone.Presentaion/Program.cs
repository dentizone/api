using Dentizone.Application.DI;
using Dentizone.Application.Interfaces;
using Dentizone.Infrastructure;
using Dentizone.Infrastructure.DependencyInjection;
using Dentizone.Infrastructure.Filters;
using Dentizone.Infrastructure.Persistence.Seeder;
using Dentizone.Presentaion.Context;
using Microsoft.OpenApi.Models;
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
            builder.Services.AddAutoMapper(typeof(Application.AssemblyReference).Assembly);
            builder.Services.AddApplicationServices();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IRequestContextService, RequestContextService>();
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    BearerFormat = "JWT",
                    Description =
                                                            "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                                           {
                                               {
                                                   new OpenApiSecurityScheme
                                                   {
                                                       Reference = new OpenApiReference
                                                                   {
                                                                       Id = "Bearer",
                                                                       Type = ReferenceType.SecurityScheme
                                                                   }
                                                   },
                                                   Array.Empty<string>()
                                               }
                                           });
            });

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            app.UseSwagger(opt => { opt.RouteTemplate = "openapi/{documentName}.json"; });
            app.MapScalarApiReference(opt =>
            {
                opt.Title = "Dentizone API";
                opt.Theme = ScalarTheme.Mars;
                opt.DefaultHttpClient = new(ScalarTarget.Http, ScalarClient.Http11);
            });

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            RoleSeeder.SeedRolesAsync(app.Services).Wait();
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                CatalogSeeder.SeedCategoriesAndSubCategoriesAsync(dbContext).Wait();
            }

            app.Run();
        }
    }
}