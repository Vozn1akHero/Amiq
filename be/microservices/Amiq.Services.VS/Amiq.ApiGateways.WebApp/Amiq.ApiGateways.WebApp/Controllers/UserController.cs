using Amiq.ApiGateways.WebApp.Contracts.User;
using Amiq.ApiGateways.WebApp.Core;
using Amiq.ApiGateways.WebApp.HttpClients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Amiq.ApiGateways.WebApp.Controllers
{
    public class UserController : AmiqBaseController
    {
        private UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId}")]
        [Authorize]
        [Produces(typeof(DtoBasicUserInfo))]
        public IActionResult GetUserById(int userId)
        {
            var user = _userService.GetUserById(JwtStoredUserId, userId);

            return Ok(user);
        }

    }
}
