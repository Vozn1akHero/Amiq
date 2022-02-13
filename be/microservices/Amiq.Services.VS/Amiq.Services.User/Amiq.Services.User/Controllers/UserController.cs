using Amiq.Services.Common.Contracts;
using Amiq.Services.User.Base;
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
            string requestCreatorId = HttpContext.Request.Headers["Amiq-UserId"];
            var user = await _bsUser.GetUserByIdAsync(int.Parse(requestCreatorId), userId);
            if (user == null) return NotFound();

            var friendshipStatus = _friendshipService.GetFriendshipStatusBetweenUsers(requestCreatorId, userId);
            user.IsIssuerFriend = friendshipStatus.IsIssuerFriend;
            user.IssuerReceivedFriendRequest = friendshipStatus.IssuerReceivedFriendRequest;
            user.IssuerSentFriendRequest = friendshipStatus.IssuerSentFriendRequest;

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
        public async Task<IActionResult> SearchAsync([FromQuery] string text,
            [FromQuery] DtoPaginatedRequest paginatedRequest,
            CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return new StatusCodeResult(499);
            }
            var res = await _bsUser.SearchAsync(JwtStoredUserId.Value, text, paginatedRequest);
            return Ok(res);
        }
     }
}
