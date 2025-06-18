using Dentizone.Application.Interfaces;
using DeviceDetectorNET.Parser;

namespace Dentizone.Presentaion.Context;

public class RequestContextService(IHttpContextAccessor httpContextAccessor, IHostEnvironment environment)
    : IRequestContextService
{
    private readonly BotParser _botParser = new();

    public string? GetUserId()
    {
        var user = httpContextAccessor.HttpContext?.User;
        return user?.Identity?.IsAuthenticated == true
            ? user.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
            : null;
    }

    public string GetUserAgent()
    {
        return httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString() ?? "Unknown";
    }

    public string GetFingerprint()
    {
        return httpContextAccessor.HttpContext?.Request.Headers["X-Fingerprint"].ToString() ?? "Unknown";
    }

    public string GetDeviceType()
    {
        var userAgent = GetUserAgent();
        if (string.IsNullOrEmpty(userAgent))
            return "Unknown";
        var device = _botParser.Parse();
        return device?.ParserName ?? "Unknown";
    }

    public string GetIpAddress()
    {
        var context = httpContextAccessor.HttpContext;

        if (context == null)
            return "Unknown";

        var forwardedHeader = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        if (!string.IsNullOrEmpty(forwardedHeader))
            return forwardedHeader.Split(',').First().Trim();

        if (environment.IsDevelopment())
        {
            return "127.0.0.1";
        }

        return context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
    }
}