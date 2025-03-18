using InternalAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace InternalAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }
        
        [HttpPost]
        public IActionResult AddUser([FromBody] string userName)
        {
            int userId = _userService.AddUser(userName);
            return Ok(new { UserId = userId });
        }
    }
}