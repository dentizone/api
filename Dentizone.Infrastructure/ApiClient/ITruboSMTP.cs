using Refit;

namespace Dentizone.Infrastructure.ApiClient;

public interface ITruboSmtp
{
    [Post("/mail/send")]
    Task<ApiResponse<object?>> SendEmailAsync([Body] TurboSmtpEmailRequest request);
}