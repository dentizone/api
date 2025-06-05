namespace Dentizone.Domain.Interfaces.Secret;

public interface ISecretService
{
    MailSecrets GetMailServiceSecret();
    string GetSecret(string name);
}