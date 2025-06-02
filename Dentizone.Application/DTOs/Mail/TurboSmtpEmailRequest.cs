using Dentizone.Application.Services;
using System.Text.Json.Serialization;

namespace Dentizone.Application.DTOs.Mail;

public class TurboSmtpEmailRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TurboSmtpEmailRequest"/> class with authentication and sender details from the specified mail secrets.
    /// </summary>
    /// <param name="mailSecrets">The mail secrets containing authentication credentials and sender address.</param>
    public TurboSmtpEmailRequest(MailSecrets mailSecrets)
    {
        AuthUser = mailSecrets.Email;
        AuthPass = mailSecrets.Password;
        From = mailSecrets.From;
    }

    [JsonPropertyName("authuser")] private string AuthUser { get; set; }

    [JsonPropertyName("authpass")] private string AuthPass { get; set; }

    [JsonPropertyName("from")] private string From { get; set; }

    [JsonPropertyName("to")] public string To { get; set; }

    [JsonPropertyName("subject")] public string Subject { get; set; }

    [JsonPropertyName("content")] public string Content { get; set; }

    [JsonPropertyName("html_content")] public string HtmlContent { get; set; }
}