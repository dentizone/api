using Dentizone.Application.Services.Authentication;
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

public class TokenValidationResult
{
    public bool IsValid { get; set; }
    public string? ErrorMessage { get; set; }
    public ClaimsPrincipal? Principal { get; set; }

    public static TokenValidationResult Success(ClaimsPrincipal principal) =>
        new() { IsValid = true, Principal = principal };

    public static TokenValidationResult Failure(string errorMessage) =>
        new() { IsValid = false, ErrorMessage = errorMessage };
}