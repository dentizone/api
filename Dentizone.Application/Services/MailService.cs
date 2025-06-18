using Dentizone.Domain.Interfaces.Mail;
using Dentizone.Domain.Interfaces.Secret;
using Dentizone.Infrastructure.ApiClient;

namespace Dentizone.Application.Services
{
    internal class MailService(ITruboSMTP smtpApi, ISecretService secretService) : IMailService
    {
        public async Task<object?> Send(string to, string subject, string body)
        {
            var mailSecrets = new MailSecrets(secretService.GetSecret("TurboSmtpAuthUser"),
                                              secretService.GetSecret("TurboSmtpAuthPass"),
                                              secretService.GetSecret("TurboSmtpFrom"));


            var response = await smtpApi.SendEmailAsync(
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