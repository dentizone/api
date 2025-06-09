using Dentizone.Domain.Interfaces.Repositories;
using Dentizone.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Dentizone.Infrastructure.DependencyInjection
{
    internal static class AddRepositoriesServices
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserActivityRepository, UserActivityRepository>();
            services.AddScoped<IUniversityRepository, UniversityRepository>();

            return services;
        }
    }
}