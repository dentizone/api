using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Dentizone.Domain.Interfaces.Secret;
using Dentizone.Infrastructure.ApiClient.KYC;
using Newtonsoft.Json;

namespace Dentizone.Application.Services.Authentication
{
    public class Metadata
    {
        public string Email { get; set; }
        public string UserId { get; set; }
    }

    public interface IVerificationService
    {
        Task<CreateSessionResponse> StartSessionAsync(string userId, string email, string country);
    }

    public class VerificationService : IVerificationService
    {
        private readonly IDiditApi _diditApi;
        private readonly ISecretService _secretService;

        public VerificationService(IDiditApi diditApi, ISecretService secretService)
        {
            _diditApi = diditApi;
            _secretService = secretService;
        }

        public async Task<CreateSessionResponse> StartSessionAsync(string userId, string email, string country)
        {
            var request = new CreateSessionRequest
            {
                WorkflowId = _secretService.GetSecret("DiditWorkflowId"),
                VendorData = userId,
                Callback = "https://dentizone.com/auth/kyc/status",
                Metadata = JsonConvert.SerializeObject(new Metadata()
                {
                    Email = email,
                    UserId = userId
                }),
                ContactDetails = new ContactDetails
                {
                    Email = email,
                },
                ExpectedDetails = new ExpectedDetails
                {
                    Country = country,
                }
            };

            return await _diditApi.CreateSessionAsync(request, _secretService.GetSecret("DiditApi"));
        }
    }
}