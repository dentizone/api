using Dentizone.Application.DI;
using Dentizone.Application.Interfaces;
using Dentizone.Infrastructure.DependencyInjection;
using Dentizone.Infrastructure.Filters;
using Dentizone.Presentaion.Context;
using Dentizone.Presentaion.Extensions;
using Dentizone.Presentaion.Middlewares;
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


            app.Run();
        }
    }
}