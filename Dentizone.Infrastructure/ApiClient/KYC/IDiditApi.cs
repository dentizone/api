using Refit;

namespace Dentizone.Infrastructure.ApiClient.KYC
{
    public interface IDiditApi
    {
        [Post("/v2/session/")]
        Task<CreateSessionResponse> CreateSessionAsync(
            [Body] CreateSessionRequest request,
            [Header("x-api-key")] string apiKey
        );

        [Get("/v2/session/{sessionId}/decision/")]
        Task<SessionDecisionResponse> GetSessionDecisionAsync([AliasAs("sessionId")] string sessionId,
                                                              [Header("x-api-key")] string apiKey);
    }
}