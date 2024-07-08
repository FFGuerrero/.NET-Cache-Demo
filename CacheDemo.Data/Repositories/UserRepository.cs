using CacheDemo.Data.Models;
using Microsoft.Extensions.Caching.Memory;

namespace CacheDemo.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMemoryCache _memoryCache;

        public UserRepository(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public List<User> GetUsers()
        {
            List<User> output = new()
            {
                new() { FirstName = "Tim", LastName = "Corey" },
                new() { FirstName = "Sue", LastName = "Storm" },
                new() { FirstName = "Jane", LastName = "Jones" }
            };

            Thread.Sleep(3000);

            return output;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            List<User> output = new()
            {
                new() { FirstName = "Tim", LastName = "Corey" },
                new() { FirstName = "Sue", LastName = "Storm" },
                new() { FirstName = "Jane", LastName = "Jones" }
            };

            await Task.Delay(3000);

            return output;
        }

        public async Task<List<User>> GetUsersCachedAsync()
        {
            #region TryGetValue

            if (_memoryCache.TryGetValue("users", out List<User>? users))
            {
                return users!;
            }
            #endregion

            users = new()
            {
                new() { FirstName = "Tim", LastName = "Corey" },
                new() { FirstName = "Sue", LastName = "Storm" },
                new() { FirstName = "Jane", LastName = "Jones" }
            };

            await Task.Delay(3000);

            #region MemoryCacheEntryOptions
            var cacheOptions = new MemoryCacheEntryOptions()
                //.SetSize(1)
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(1))
                .SetSlidingExpiration(TimeSpan.FromMinutes(1));

            _memoryCache.Set("users", users, cacheOptions);
            #endregion

            return users;
        }
    }
}
