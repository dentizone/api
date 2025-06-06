using Dentizone.Application.Interfaces;
using DeviceDetectorNET.Parser;

public class RequestContextService : IRequestContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHostEnvironment _environment;
    private readonly BotParser _botParser;

    public RequestContextService(IHttpContextAccessor httpContextAccessor, IHostEnvironment environment)
    {
        _botParser = new BotParser();
        _httpContextAccessor = httpContextAccessor;
        _environment = environment;
    }

    public string? GetUserId()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        return user?.Identity?.IsAuthenticated == true
            ? user.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
            : null;
    }

    public string GetUserAgent()
    {
        return _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString() ?? "Unknown";
    }

    public string GetFingerprint()
    {
        return _httpContextAccessor.HttpContext?.Request.Headers["X-Fingerprint"].ToString() ?? "Unknown";
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
        var context = _httpContextAccessor.HttpContext;

        if (context == null)
            return "Unknown";

        var forwardedHeader = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        if (!string.IsNullOrEmpty(forwardedHeader))
            return forwardedHeader.Split(',').First().Trim();

        if (_environment.IsDevelopment())
        {
            return "127.0.0.1";
        }

        return context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
    }
}