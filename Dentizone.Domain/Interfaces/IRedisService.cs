namespace Dentizone.Infrastructure.Cache;

public interface IRedisService
{
    void SetValue(string key, string value);
    void SetValue(string key, string value, TimeSpan expireTime);
    string? GetValue(string key);
}