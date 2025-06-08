using Dentizone.Application.DTOs.User;
using Dentizone.Infrastructure.ApiClient.KYC;

namespace Dentizone.Application.Interfaces;

public interface IVerificationService
{
    Task<CreateSessionResponse> StartSessionAsync(string userId);
    Task<SessionDecisionResponse> GetVerificationStatusAsync(string sessionId);

    Task<UserView> UpdateUserNationalId(string userId, string nationalId);
}