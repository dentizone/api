using Dentizone.Application.Services;
using System.Text.Json.Serialization;

namespace Dentizone.Application.DTOs.Mail;

public class TurboSmtpEmailRequest
{
    public TurboSmtpEmailRequest(MailSecrets mailSecrets)
    {
        AuthUser = mailSecrets.Email;
        AuthPass = mailSecrets.Password;
        From = mailSecrets.From;
    }

    [JsonPropertyName("authuser")] public string AuthUser { get; private set; }

    [JsonPropertyName("authpass")] public string AuthPass { get; private set; }

    [JsonPropertyName("from")] public string From { get; private set; }

    [JsonPropertyName("to")] public string To { get; set; }

    [JsonPropertyName("subject")] public string Subject { get; set; }

    [JsonPropertyName("content")] public string Content { get; set; }

    [JsonPropertyName("html_content")] public string HtmlContent { get; set; }
}