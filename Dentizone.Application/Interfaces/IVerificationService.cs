using Dentizone.Application.DTOs.User;
using Dentizone.Infrastructure.ApiClient.KYC;

namespace Dentizone.Application.Interfaces;

public interface IVerificationService
{
    Task<SessionDecisionResponse> GetVerificationStatusAsync(string sessionId);
    Task<CreateSessionResponse> StartSessionAsync(string userId);
    Task<UserView> UpdateUserNationalId(string userId, string nationalId);
}