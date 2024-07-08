using CacheDemo.Data.Helpers;
using CacheDemo.Data.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace CacheDemo.Data.Repositories;

public class CachedUserRepository : IUserRepository
{
    private readonly IUserRepository _userRepository;
    private readonly IMemoryCache _memoryCache;
    private readonly IDistributedCache _cache;

    public CachedUserRepository(IUserRepository userRepository,
        IMemoryCache memoryCache,
        IDistributedCache cache)
    {
        _userRepository = userRepository;
        _memoryCache = memoryCache;
        _cache = cache;
    }

    public List<User> GetUsers()
    {
        return _userRepository.GetUsers();
    }

    public Task<List<User>> GetUsersAsync(CancellationToken cancellationToken)
    {
        return _userRepository.GetUsersAsync(cancellationToken);
    }

    public async Task<List<User>> GetUsersMemoryCachedAsync(CancellationToken cancellationToken)
    {
        return (await _memoryCache!.GetOrCreateAsync(
            "users",
            async entry =>
            {
                //entry.SetSize(1)
                entry!.SetAbsoluteExpiration(TimeSpan.FromMinutes(1));
                entry!.SetSlidingExpiration(TimeSpan.FromMinutes(1));

                return await _userRepository.GetUsersAsync(cancellationToken);
            }
        ))!;
    }

    public async Task<List<User>> GetUsersRedisCachedAsync(CancellationToken cancellationToken)
    {
        List<User>? users;
        string recordKey = $"Users_List";
        users = await _cache.GetRecordAsync<List<User>>(recordKey, cancellationToken);

        if (users is null)
        {
            users = await _userRepository.GetUsersAsync(cancellationToken);
            await _cache.SetRecordAsync(recordKey, users, cancellationToken: cancellationToken);
        }

        return users;
    }
}
