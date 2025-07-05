using Dentizone.Application.DTOs.User;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces.Mail;
using Dentizone.Domain.Interfaces.Secret;
using Dentizone.Infrastructure.ApiClient.KYC;
using Newtonsoft.Json;

namespace Dentizone.Application.Services.Authentication
{
    public class Metadata
    {
        public required string Email { get; set; }
        public required string UserId { get; set; }
    }


    public class VerificationService(IDiditApi diditApi, ISecretService secretService, IAuthService authService,
        IUserService userService, IMailService mailService) : IVerificationService
    {
        private readonly IDiditApi _diditApi = diditApi;
        private readonly ISecretService _secretService = secretService;
        private readonly IAuthService _authService = authService;
        private readonly IUserService _userService = userService;



        private static Dictionary<string, KycStatus> MapVerificationStatusToEnum()
        {
            return new Dictionary<string, KycStatus>
            {
                { "approved", KycStatus.Approved },
                { "declined", KycStatus.Rejected },
                { "pending", KycStatus.Pending }
            };
        }

        public async Task<CreateSessionResponse> StartSessionAsync(string userId)
        {
            var user = await _authService.GetById(userId);
            var domainUser = await _userService.GetByIdAsync(userId);

            if (domainUser.KycStatus.Equals(KycStatus.NotSubmitted.ToString()))

            {
                throw new BadActionException("KYC is not submitted yet");
            }
            if (domainUser.KycStatus.Equals(KycStatus.Approved.ToString()))
            {
                throw new BadActionException("Your account is Already Active");
            }


            var request = new CreateSessionRequest
            {
                WorkflowId = _secretService.GetSecret("DiditWorkflowId"),
                VendorData = userId,
                Callback = "https://dentizone.vercel.app/auth/kyc/status",
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
            await _userService.SetKycStatusAsync(userId, KycStatus.NotSubmitted);
            await mailService.Send(user.Email!, "Dentizone: Verification Started",
                "Thank you for starting the email verification process." +
                " You can use this url to verify your identity" +
                $" <a href=\"{session.Url}\">Verify Now</a>"
            );

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

        public async Task<UserView> UpdateUserNationalId(string userId, string nationalId)
        {
            var output = await _userService.SetNationalId(userId, nationalId);
            return output;
        }
    }
}