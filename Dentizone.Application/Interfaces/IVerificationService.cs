using Dentizone.Infrastructure.ApiClient.KYC;

namespace Dentizone.Application.Interfaces;

public interface IVerificationService
{
    Task<CreateSessionResponse> StartSessionAsync(string userId);
    Task<SessionDecisionResponse> GetVerificationStatusAsync(string sessionId);
}