using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace CacheDemo.Data.Helpers
{
    public static class CacheHelper
    {
        public static async Task SetRecordAsync<T>(this IDistributedCache cache,
                                                   string recordId,
                                                   T data,
                                                   TimeSpan? absoluteExpireTime = null,
                                                   TimeSpan? slidingExpireTime = null,
                                                   CancellationToken cancellationToken = default)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromMinutes(1),
                SlidingExpiration = slidingExpireTime ?? TimeSpan.FromMinutes(1)
            };

            var jsonData = JsonSerializer.Serialize(data);
            await cache.SetStringAsync(recordId, jsonData, options, cancellationToken);
        }

        public static async Task<T?> GetRecordAsync<T>(this IDistributedCache cache,
                                                       string recordId,
                                                       CancellationToken cancellationToken)
        {
            var jsonData = await cache.GetStringAsync(recordId, cancellationToken);

            if (jsonData is null)
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(jsonData);
        }
    }
}
