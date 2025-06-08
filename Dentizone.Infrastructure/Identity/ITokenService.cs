using System.Security.Claims;

namespace Dentizone.Infrastructure.Identity;

public interface ITokenService
{
    string GenerateAccessToken(string userId, string email, string role);
    string GenerateRefreshToken(string userId);
    Task<TokenValidationResult> ValidateAccessTokenAsync(string token);
    Task<TokenValidationResult> ValidateRefreshTokenAsync(string token);
    Task<bool> BlacklistAccessTokenAsync(string token);
    Task<bool> BlacklistRefreshTokenAsync(string token);
    Task<bool> IsBlacklistedAsync(string tokenId);
    string? ExtractTokenId(string token);
    ClaimsPrincipal? ExtractClaims(string token);
}