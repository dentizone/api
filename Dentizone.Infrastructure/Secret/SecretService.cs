using Dentizone.Domain.Interfaces.Secret;
using Infisical.Sdk;
using System.Collections.Concurrent;

namespace Dentizone.Infrastructure.Secret
{
    internal class GetSecret : GetSecretOptions
    {
        public GetSecret()
        {
            Environment = System.Environment.GetEnvironmentVariable("env") ?? "unknown";
            ProjectId = System.Environment.GetEnvironmentVariable("ProjectId") ??
                        throw new ArgumentNullException("Can't find the project id");
        }
    }


    public class SecretService(InfisicalClient infisicalClient) : ISecretService
    {
        private readonly ConcurrentDictionary<string, string> _cache = new();

        private GetSecret CreateSecret(string name)
        {
            return new GetSecret
            {
                SecretName = name
            };
        }


        public string GetSecret(string name)
        {
            try
            {
                return _cache.GetOrAdd(name,
                    n => infisicalClient.GetSecret(CreateSecret(n)).SecretValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving secret: {ex.Message}");
                throw;
            }
        }
    }
}