using Dentizone.Application.DI;
using Dentizone.Infrastructure.DependencyInjection;

namespace Dentizone.Presentaion
{
    public class Program
    {
        /// <summary>
        /// Entry point for the web application, configuring services, middleware, and starting the HTTP server.
        /// </summary>
        /// <param name="args">Command-line arguments for application configuration.</param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddSecretManager();
            builder.Services.AddSQLServer(builder.Configuration);
            builder.Services.AddAutoMapper(typeof(Application.AssemblyReference).Assembly);
            builder.Services.AddApplicationServices();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}