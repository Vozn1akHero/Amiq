using Amiq.Services.Base.Auth;
using Amiq.Services.Base.Controllers;
using Amiq.Services.Common.Contracts;
using Amiq.Services.User.BusinessLayer;
using Amiq.Services.User.Contracts.User;
using Amiq.Services.User.HttpClients;
using Microsoft.AspNetCore.Mvc;

namespace Amiq.Services.User.Controllers
{
    [Route("api/user")]
    public class UserController : AmiqBaseController
    {
        private FriendshipService _friendshipService;
        private BlUser _bsUser = new BlUser();

        public UserController(FriendshipService friendshipService)
        {
            _friendshipService = friendshipService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserByIdAsync(int userId)
        {
            var user = await _bsUser.GetUserByIdAsync(JwtStoredUserId, userId);
            if (user == null) return NotFound();

            if (JwtStoredUserId != userId)
            {
                var friendshipStatus = _friendshipService.GetFriendshipStatusBetweenUsers(JwtStoredUserId.ToString(), userId);
                user.IsIssuerFriend = friendshipStatus.IsIssuerFriend;
                user.IssuerReceivedFriendRequest = friendshipStatus.IssuerReceivedFriendRequest;
                user.IssuerSentFriendRequest = friendshipStatus.IssuerSentFriendRequest;
            }

            return Ok(user);
        }

        [HttpGet("basic-user-data/{userId}")]
        [Produces(typeof(DtoBasicUserInfo))]
        public async Task<IActionResult> GetBasicUserDataByIdAsync(int userId)
        {
            var user = await _bsUser.GetBasicUserDataByIdAsync(userId);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpGet("search")]
        [AmiqAuthorize]
        public async Task<IActionResult> SearchAsync([FromQuery] string text,
            [FromQuery] DtoPaginatedRequest paginatedRequest,
            CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return new StatusCodeResult(499);
            }
            var res = await _bsUser.SearchAsync(JwtStoredUserId, text, paginatedRequest);
            return Ok(res);
        }
     }
}
