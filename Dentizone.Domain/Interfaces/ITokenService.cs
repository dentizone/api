namespace Dentizone.Domain.Interfaces;

public interface ITokenService
{
    string GenerateToken(string id, string email, string role);

    Task BlacklistToken(string token);
    bool IsBlacklisted(string tokenId);
}