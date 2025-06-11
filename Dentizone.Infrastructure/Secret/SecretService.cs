using Dentizone.Domain.Interfaces.Secret;
using Infisical.Sdk;
using System.Collections.Concurrent;

namespace Dentizone.Infrastructure.Secret
{
    internal class GetSecret : GetSecretOptions
    {
        public GetSecret()
        {
            Environment = "dev";
            ProjectId = System.Environment.GetEnvironmentVariable("ProjectId") ??
                        throw new ArgumentNullException("Can't find the project id");
        }
    }


    public class SecretService : ISecretService
    {
        private readonly InfisicalClient _infisicalClient;
        private readonly ConcurrentDictionary<string, string> _cache = new();

        private GetSecret CreateSecret(string name)
        {
            return new GetSecret
            {
                SecretName = name
            };
        }

        public SecretService(InfisicalClient infisicalClient)
        {
            _infisicalClient = infisicalClient;
        }

        public MailSecrets GetMailServiceSecret()
        {
            try
            {
                var email = _infisicalClient.GetSecret(CreateSecret("TurboSmtpAuthUser")).SecretValue;
                var password = _infisicalClient.GetSecret(CreateSecret("TurboSmtpAuthPass")).SecretValue;
                var from = _infisicalClient.GetSecret(CreateSecret("TurboSmtpFrom")).SecretValue;
                return new MailSecrets(email, password, from);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving secret: {ex.Message}");
                throw;
            }
        }


        public string GetSecret(string name)
        {
            try
            {
                if (_cache.TryGetValue(name, out var value))
                    return value;
                value = _infisicalClient.GetSecret(CreateSecret(name)).SecretValue;
                _cache[name] = value;
                return value;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving secret: {ex.Message}");
                throw;
            }
        }
    }
}