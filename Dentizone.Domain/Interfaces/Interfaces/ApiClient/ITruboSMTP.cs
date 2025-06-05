using Dentizone.Application.DTOs.Mail;
using Refit;

namespace Dentizone.Application.Interfaces.ApiClient;

public interface ITruboSMTP
{
    [Post("/mail/send")]
    Task<ApiResponse<object?>> SendEmailAsync([Body] TurboSmtpEmailRequest request);
}