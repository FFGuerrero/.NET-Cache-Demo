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
        public async Task<IActionResult> GetUsers()
        {
            var values = await _userRepository.GetUsersCachedAsync();
            return Ok(values);
        }
    }
}
