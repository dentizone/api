using Refit;

namespace Dentizone.Infrastructure.ApiClient;

public interface ITruboSMTP
{
    [Post("/mail/send")]
    Task<ApiResponse<object?>> SendEmailAsync([Body] TurboSmtpEmailRequest request);
}