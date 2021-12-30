using Amiq.Services.Friendship.Base;
using Amiq.Services.Friendship.BusinessLayer;
using Amiq.Services.Friendship.Contracts.Friendship;
using Amiq.Services.Friendship.Contracts.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Amiq.Services.Friendship.Controllers
{
    //[Authorize]
    [Route("api/friendship")]
    public class FriendshipController : AmiqBaseController
    {
        private BlFriendship _bsFriend = new BlFriendship();

        [HttpGet("friend-list/{userId}")]
        [Produces(typeof(DtoListResponseOf<DtoFriend>))]
        [ProducesResponseType(((int)HttpStatusCode.OK))]
        [ProducesResponseType(499)]
        public async Task<IActionResult> GetUserFriendListAsync(int userId,
            [FromQuery] DtoGetFriendListRequest dtoFriendListRequest,
            CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return new StatusCodeResult(499);
            }
            dtoFriendListRequest.IssuerId = userId;
            var data = await _bsFriend.GetUserFriendListAsync(dtoFriendListRequest);
            return Ok(data);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchAmongFriendsAsync([FromQuery] string text,
            [FromQuery] DtoPaginatedRequest paginatedRequest,
            CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return new StatusCodeResult(499);
            }
            var data = await _bsFriend.SearchAsync(JwtStoredUserId, paginatedRequest, text);
            return Ok(data);
        }

        [HttpDelete("{friendId}")]
        public IActionResult RemoveFriend(int friendId)
        {
            var result = _bsFriend.RemoveFriend(JwtStoredUserId, friendId);
            return Ok(result);
        }

        [HttpGet("friendship-status/{userId}")]
        [Produces(typeof(DtoFriendshipStatus))]
        public IActionResult GetFriendshipStatus(int userId)
        {
            var result = _bsFriend.GetFriendshipStatus(JwtStoredUserId, userId);
            return Ok(result);
        }
    }
}
