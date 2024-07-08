using CacheDemo.Data.Helpers;
using CacheDemo.Data.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace CacheDemo.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IDistributedCache _cache;

        public UserRepository(IMemoryCache memoryCache, IDistributedCache cache)
        {
            _memoryCache = memoryCache;
            _cache = cache;
        }

        public List<User> GetUsers()
        {
            List<User> output = new()
            {
                new() { FirstName = "William", LastName = "Jackson" },
                new() { FirstName = "Maria", LastName = "Moody" },
                new() { FirstName = "Sarah", LastName = "King" },
                new() { FirstName = "Gregory", LastName = "Estrada" },
                new() { FirstName = "Juan", LastName = "Russell" },
                new() { FirstName = "James", LastName = "Bryant" },
                new() { FirstName = "Patrick", LastName = "Mullins" },
                new() { FirstName = "Sandra", LastName = "Fleming" },
                new() { FirstName = "Miguel", LastName = "Ramsey" },
                new() { FirstName = "Monica", LastName = "Howell" }
            };

            Thread.Sleep(3000);

            return output;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            List<User> output = new()
            {
                new() { FirstName = "William", LastName = "Jackson" },
                new() { FirstName = "Maria", LastName = "Moody" },
                new() { FirstName = "Sarah", LastName = "King" },
                new() { FirstName = "Gregory", LastName = "Estrada" },
                new() { FirstName = "Juan", LastName = "Russell" },
                new() { FirstName = "James", LastName = "Bryant" },
                new() { FirstName = "Patrick", LastName = "Mullins" },
                new() { FirstName = "Sandra", LastName = "Fleming" },
                new() { FirstName = "Miguel", LastName = "Ramsey" },
                new() { FirstName = "Monica", LastName = "Howell" }
            };

            await Task.Delay(3000);

            return output;
        }

        public async Task<List<User>> GetUsersMemoryCachedAsync()
        {
            #region TryGetValue

            if (_memoryCache.TryGetValue("users", out List<User>? users))
            {
                return users!;
            }
            #endregion

            users = new()
            {
                new() { FirstName = "William", LastName = "Jackson" },
                new() { FirstName = "Maria", LastName = "Moody" },
                new() { FirstName = "Sarah", LastName = "King" },
                new() { FirstName = "Gregory", LastName = "Estrada" },
                new() { FirstName = "Juan", LastName = "Russell" },
                new() { FirstName = "James", LastName = "Bryant" },
                new() { FirstName = "Patrick", LastName = "Mullins" },
                new() { FirstName = "Sandra", LastName = "Fleming" },
                new() { FirstName = "Miguel", LastName = "Ramsey" },
                new() { FirstName = "Monica", LastName = "Howell" }
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

        public async Task<List<User>> GetUsersRedisCachedAsync()
        {
            List<User>? users;
            string recordKey = $"Users_List";
            users = await _cache.GetRecordAsync<List<User>>(recordKey);

            if (users is null)
            {
                users = await GetUsersAsync();
                await _cache.SetRecordAsync(recordKey, users);
            }

            return users;
        }
    }
}
