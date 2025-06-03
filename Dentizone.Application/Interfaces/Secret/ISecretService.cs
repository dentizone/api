using Dentizone.Application.Services;

namespace Dentizone.Application.Interfaces.Secret;

public interface ISecretService
{
    MailSecrets GetMailServiceSecret();
}