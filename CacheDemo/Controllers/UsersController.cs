using CacheDemo.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CacheDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        // GET api/users
        [HttpGet]
        public async Task<IActionResult> GetUsersWithMemoryCache(CancellationToken cancellationToken)
        {
            var values = await _userRepository.GetUsersMemoryCachedAsync(cancellationToken);
            return Ok(values);
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        // GET api/users
        [HttpGet]
        [Route("redis")]
        public async Task<IActionResult> GetUsersWithRedisCache(CancellationToken cancellationToken)
        {
            var values = await _userRepository.GetUsersRedisCachedAsync(cancellationToken);
            return Ok(values);
        }
    }
}
