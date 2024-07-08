using CacheDemo.Data.Models;

namespace CacheDemo.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        public UserRepository()
        {
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

        public async Task<List<User>> GetUsersAsync(CancellationToken cancellationToken)
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

            await Task.Delay(3000, cancellationToken);

            return output;
        }

        public async Task<List<User>> GetUsersMemoryCachedAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetUsersRedisCachedAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
