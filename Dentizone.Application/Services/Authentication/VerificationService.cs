using Dentizone.Application.DTOs.User;
using Dentizone.Application.Interfaces;
using Dentizone.Application.Interfaces.User;
using Dentizone.Domain.Enums;
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


    public class VerificationService : IVerificationService
    {
        private readonly IDiditApi _diditApi;
        private readonly ISecretService _secretService;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;


        public static Dictionary<string, KycStatus> MapVerificationStatusToEnum()
        {
            return new Dictionary<string, KycStatus>
                   {
                       { "approved", KycStatus.APPROVED },
                       { "declined", KycStatus.REJECTED },
                       { "pending", KycStatus.PENDING }
                   };
        }

        public VerificationService(IDiditApi diditApi, ISecretService secretService, IAuthService authService,
                                   IUserService userService)
        {
            _diditApi = diditApi;
            _secretService = secretService;
            _authService = authService;
            _userService = userService;
        }

        public async Task<CreateSessionResponse> StartSessionAsync(string userId)
        {
            var user = await _authService.GetById(userId);

            var request = new CreateSessionRequest
            {
                WorkflowId = _secretService.GetSecret("DiditWorkflowId"),
                VendorData = userId,
                Callback = "https://dentizone.com/auth/kyc/status",
                Metadata = JsonConvert.SerializeObject(new Metadata()
                {
                    Email = user.Email,
                    UserId = userId
                }),
                ContactDetails = new ContactDetails
                {
                    Email = user.Email,
                },
                ExpectedDetails = new ExpectedDetails()
                {
                    Country = "EGY"
                }
            };

            var session = await _diditApi.CreateSessionAsync(request, _secretService.GetSecret("DiditApi"));
            await _userService.SetKycStatusAsync(userId, KycStatus.NOT_SUBMITTED);

            return session;
        }

        public async Task<SessionDecisionResponse> GetVerificationStatusAsync(string sessionId)
        {
            return await _diditApi.GetSessionDecisionAsync(sessionId, _secretService.GetSecret("DiditApi"));
        }


        public async Task<UserView> UpdateUserVerificationState(string userId, string status)
        {
            if (!MapVerificationStatusToEnum().TryGetValue(status.ToLower(), out var kycStatus))
            {
                throw new ArgumentException($"Invalid verification status: {status}");
            }

            var output = await _userService.SetKycStatusAsync(userId, kycStatus);

            return output;
        }
    }
}