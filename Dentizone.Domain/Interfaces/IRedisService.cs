namespace Dentizone.Infrastructure.Cache;

public interface IRedisService
{
    Task SetValue(string key, string value);
    Task SetValue(string key, string value, TimeSpan expireTime);
    Task<string?> GetValue(string key);
}