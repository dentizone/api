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

        /// <summary>
        /// Initializes a new instance of the <see cref="MailService"/> class with the specified SMTP API and secret service dependencies.
        /// </summary>
        public MailService(ITruboSMTP smtpApi, ISecretService secretService)
        {
            _smtpApi = smtpApi;
            _secretService = secretService;
        }

        /// <summary>
        /// Sends an email asynchronously to the specified recipient with the given subject and body.
        /// </summary>
        /// <param name="to">The recipient's email address.</param>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="body">The body content of the email.</param>
        /// <returns>The content of the response from the SMTP API, or null if unavailable.</returns>
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

        /// <summary>
        /// Sends an email to the specified recipient with the given body.
        /// </summary>
        /// <param name="to">The recipient's email address.</param>
        /// <param name="body">The content of the email.</param>
        /// <returns>An object representing the result of the send operation.</returns>
        /// <remarks>
        /// This method is not implemented and will throw a <see cref="NotImplementedException"/>.
        /// </remarks>
        public async Task<object> Send(string to, string body)
        {
            throw new NotImplementedException();
        }
    }
}