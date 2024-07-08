using BenchmarkDotNet.Attributes;
using CacheDemo.Data.Models;

namespace CacheDemo.Benchmark.Benchmarks
{
    [MemoryDiagnoser]
    public class CachingBenchmarks : Benchmark
    {
        [Benchmark]
        public List<User> WithoutCachingNoAsync()
        {
            return Repository.GetUsers();
        }

        [Benchmark(Baseline = true)]
        public async Task<List<User>> WithoutCachingWithAsync()
        {
            return await Repository.GetUsersAsync();
        }

        [Benchmark]
        public async Task<List<User>> WithCachingWithAsync()
        {
            return await Repository.GetUsersCachedAsync();
        }
    }
}