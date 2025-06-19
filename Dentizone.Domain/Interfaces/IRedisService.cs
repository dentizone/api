namespace Dentizone.Domain.Interfaces;

public interface IRedisService
{
    Task SetValue(string key, string value);
    Task SetValue(string key, string value, TimeSpan expireTime);
    Task<string?> GetValue(string key);
    Task InvalidateCache(string key);
}