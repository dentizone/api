using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Dentizone.Application.DI
{
    public static class ServiceCollectionExtensions
    {
        public static void ValidateAllDependencies(this IServiceCollection services, Assembly[] assembliesToScan)
        {
            var serviceTypes = assembliesToScan
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsInterface && t.Name.EndsWith("Service"))
                .ToList();

            foreach (var serviceType in serviceTypes)
            {
                var isRegistered = services.Any(sd => sd.ServiceType == serviceType);
                if (!isRegistered)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"❌ Service not registered: {serviceType.FullName}");
                    Console.ResetColor();
                    throw new InvalidOperationException($"Service not registered: {serviceType.FullName}");
                }
            }
        }
    }
}