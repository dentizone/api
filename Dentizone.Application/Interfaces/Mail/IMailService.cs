namespace Dentizone.Application.Interfaces.Mail;

public interface IMailService
{
    /// <summary>
/// Sends an email asynchronously to the specified recipient with the given subject and body.
/// </summary>
/// <param name="to">The recipient's email address.</param>
/// <param name="subject">The subject of the email.</param>
/// <param name="body">The content of the email message.</param>
/// <returns>A task representing the asynchronous operation, with a result that may be an object or null.</returns>
Task<object?> Send(string to, string subject, string body);
    /// <summary>
/// Sends an email to the specified recipient with the given body.
/// </summary>
/// <param name="to">The recipient's email address.</param>
/// <param name="body">The content of the email message.</param>
/// <returns>A task representing the asynchronous operation, with a result object related to the email sending process.</returns>
Task<object> Send(string to, string body);
}