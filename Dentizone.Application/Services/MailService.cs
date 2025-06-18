using Dentizone.Domain.Interfaces.Mail;
using Dentizone.Domain.Interfaces.Secret;
using Dentizone.Infrastructure.ApiClient;

namespace Dentizone.Application.Services
{
    internal class MailService : IMailService
    {
        private readonly ITruboSMTP _smtpApi;
        private readonly ISecretService _secretService;

        public MailService(ITruboSMTP smtpApi, ISecretService secretService)
        {
            _smtpApi = smtpApi;
            _secretService = secretService;
        }

        public async Task<object?> Send(string to, string subject, string body)
        {
            var mailSecrets = new MailSecrets(_secretService.GetSecret("TurboSmtpAuthUser"),
                                              _secretService.GetSecret("TurboSmtpAuthPass"),
                                              _secretService.GetSecret("TurboSmtpFrom"));


            var response = await _smtpApi.SendEmailAsync(
                                                         new TurboSmtpEmailRequest(mailSecrets)
                                                         {
                                                             To = to,
                                                             Subject = subject,
                                                             Content = body
                                                         });
            return response.Content;
        }

        public async Task<object> Send(string to, string body)
        {
            throw new NotImplementedException();
        }
    }
}