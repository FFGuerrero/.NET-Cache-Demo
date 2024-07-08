using CacheDemo.Data.Models;

namespace CacheDemo.Data.Repositories
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        Task<List<User>> GetUsersAsync();
        Task<List<User>> GetUsersMemoryCachedAsync();
        Task<List<User>> GetUsersRedisCachedAsync();
    }
}