using CacheDemo.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CacheDemo.Benchmark.Benchmarks
{
    public class Benchmark
    {
        protected readonly IUserRepository Repository;
        public Benchmark()
        {
            var builder = Host.CreateDefaultBuilder()
                .ConfigureHostConfiguration(ConfigureHostConfiguration)
                .ConfigureServices(ConfigureServices)
            .Build();

            var serviceProvider = builder.Services.GetRequiredService<IServiceProvider>();
            using IServiceScope scope = serviceProvider.CreateScope();
            Repository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
        }

        void ConfigureHostConfiguration(IConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.AddJsonFile("appsettings.json")
                                .AddEnvironmentVariables();
        }

        void ConfigureServices(HostBuilderContext hostBuilderContext, IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "redis:6379,password=MyRedisSecurePassword,abortConnect=false,allowAdmin=true,ssl=true";
                options.InstanceName = "RedisDemo_";
            });
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}