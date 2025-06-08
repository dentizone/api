namespace Dentizone.Application.Interfaces;

public interface IRequestContextService
{
    string GetUserAgent();
    string GetFingerprint();
    string GetIpAddress();

    string GetDeviceType();
    string? GetUserId();
}