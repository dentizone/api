using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}