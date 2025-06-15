using System.Text;

namespace Dentizone.Infrastructure.Cache
{
    public static class CacheHelper
    {
        public static string GenerateCacheKey(string key)
        {
            var sb = new StringBuilder(key);
            sb.Append($":cache");

            return sb.ToString();
        }

        public static string GenerateCacheKey(string prefix, params object[] args)
        {
            var sb = new StringBuilder(prefix);
            foreach (var arg in args)
            {
                sb.Append($":{arg}");
            }

            return sb.ToString();
        }

        // Convert Dto to a cache key
        private static string GenerateCacheKey<T>(string prefix, T dto)
        {
            var sb = new StringBuilder(prefix);
            sb.Append($":{typeof(T).Name}");
            foreach (var prop in typeof(T).GetProperties())
            {
                var value = prop.GetValue(dto);
                sb.Append($":{prop.Name}:{value}");
            }

            return sb.ToString();
        }


        public static string GenerateCacheKeyHash<T>(string prefix, T dto)
        {
            using var md5 = System.Security.Cryptography.MD5.Create();
            var input = GenerateCacheKey(prefix, dto);
            var hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
        }
    }
}