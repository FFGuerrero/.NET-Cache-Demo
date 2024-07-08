using CacheDemo.Data.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.StackExchangeRedis;

namespace CacheDemo.Benchmark.Benchmarks
{
    public class Benchmark
    {
        protected readonly UserRepository Repository;
        public Benchmark()
        {
            IMemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());
            IDistributedCache redisCache = new RedisCache(new RedisCacheOptions() { InstanceName = "RedisDemo_", Configuration = "redis:6379,password=MyRedisApplaudoSecurePassword" });

            Repository = new UserRepository(memoryCache, redisCache);
        }
    }
}