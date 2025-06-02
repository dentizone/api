using Dentizone.Application.Services;

namespace Dentizone.Application.Interfaces.Secret;

public interface ISecretService
{
    /// <summary>
/// Retrieves the secrets required for mail service integration.
/// </summary>
/// <returns>A <see cref="MailSecrets"/> instance containing mail service credentials and configuration.</returns>
MailSecrets GetMailServiceSecret();
}