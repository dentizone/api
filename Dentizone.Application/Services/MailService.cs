using Dentizone.Application.DTOs.Mail;
using Dentizone.Application.Interfaces.ApiClient;
using Dentizone.Application.Interfaces.Mail;
using Dentizone.Application.Interfaces.Secret;

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
            var response = await _smtpApi.SendEmailAsync(
                                                         new TurboSmtpEmailRequest(_secretService
                                                                  .GetMailServiceSecret())
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