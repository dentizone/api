namespace Dentizone.Application.Interfaces.Mail;

public interface IMailService
{
    Task<object?> Send(string to, string subject, string body);
    Task<object> Send(string to, string body);
}