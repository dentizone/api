using Dentizone.Application.Interfaces.Secret;
using Infisical.Sdk;

namespace Dentizone.Application.Services
{
    internal class GetSecret : GetSecretOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetSecret"/> class with the environment set to "dev" and the project ID loaded from the "ProjectId" environment variable.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if the "ProjectId" environment variable is not set.</exception>
        public GetSecret()
        {
            Environment = "dev";
            ProjectId = System.Environment.GetEnvironmentVariable("ProjectId") ??
                        throw new ArgumentNullException("Can't find the project id");
        }
    }

    public record MailSecrets(string Email, string Password, string From);


    public class SecretService : ISecretService
    {
        private readonly InfisicalClient _infisicalClient;

        /// <summary>
        /// Creates a <see cref="GetSecret"/> instance with the specified secret name.
        /// </summary>
        /// <param name="name">The name of the secret to retrieve.</param>
        /// <returns>A <see cref="GetSecret"/> object configured with the given secret name.</returns>
        private GetSecret CreateSecret(string name)
        {
            return new GetSecret
            {
                SecretName = name
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecretService"/> class with the specified Infisical client.
        /// </summary>
        public SecretService(InfisicalClient infisicalClient)
        {
            _infisicalClient = infisicalClient;
        }

        /// <summary>
        /// Retrieves mail service credentials from the secret management system.
        /// </summary>
        /// <returns>A <see cref="MailSecrets"/> record containing the email, password, and sender address for the mail service.</returns>
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
    }
}