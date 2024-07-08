using CacheDemo.Data.Models;

namespace CacheDemo.Data.Repositories
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        Task<List<User>> GetUsersAsync(CancellationToken cancellationToken);
        Task<List<User>> GetUsersMemoryCachedAsync(CancellationToken cancellationToken);
        Task<List<User>> GetUsersRedisCachedAsync(CancellationToken cancellationToken);
    }
}