using BenchmarkDotNet.Running;
using CacheDemo.Benchmark.Benchmarks;

BenchmarkRunner.Run<CachingBenchmarks>();