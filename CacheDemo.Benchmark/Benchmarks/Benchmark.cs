using CacheDemo.Data.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace CacheDemo.Benchmark.Benchmarks
{
    public class Benchmark
    {
        protected readonly UserRepository Repository;
        public Benchmark()
        {
            IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());
            Repository = new UserRepository(cache);
        }
    }
}