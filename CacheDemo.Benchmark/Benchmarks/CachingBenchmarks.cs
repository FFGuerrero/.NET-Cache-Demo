using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using CacheDemo.Data.Models;

namespace CacheDemo.Benchmark.Benchmarks
{
    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [MemoryDiagnoser]
    public class CachingBenchmarks : Benchmark
    {
        [Benchmark]
        public List<User> WithoutCachingNoAsync()
        {
            return Repository.GetUsers();
        }

        [Benchmark]
        public async Task<List<User>> WithoutCachingWithAsync()
        {
            return await Repository.GetUsersAsync();
        }

        [Benchmark]
        public async Task<List<User>> WithMemoryCachingAsync()
        {
            return await Repository.GetUsersMemoryCachedAsync();
        }

        [Benchmark]
        public async Task<List<User>> WithRedisCachingAsync()
        {
            return await Repository.GetUsersRedisCachedAsync();
        }
    }
}