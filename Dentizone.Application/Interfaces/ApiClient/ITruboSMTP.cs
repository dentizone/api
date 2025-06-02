using Dentizone.Application.DTOs.Mail;
using Refit;

namespace Dentizone.Application.Interfaces.ApiClient;

public interface ITruboSMTP
{
    /// <summary>
    /// Sends an email using the TurboSMTP service via a POST request to the /mail/send endpoint.
    /// </summary>
    /// <param name="request">The email request payload containing message details.</param>
    /// <returns>An asynchronous task containing the API response.</returns>
    [Post("/mail/send")]
    Task<ApiResponse<object?>> SendEmailAsync([Body] TurboSmtpEmailRequest request);
}