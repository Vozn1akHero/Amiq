using Amiq.Business.Friend;
using Amiq.Contracts.Auth;
using Amiq.Contracts.Friendship;
using Amiq.Contracts.Utils;
using Amiq.WebApi.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Amiq.WebApi.Controllers
{
    [Authorize]
    public class FriendshipController : AmiqBaseController
    {
        private FriendshipBsService _bsFriend = new FriendshipBsService();

        [HttpGet("friend-list/{userId}")]
        [Produces(typeof(IEnumerable<DtoFriend>))]
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
            var data = await _bsFriend.SearchAsync(JwtStoredUserInfo.UserId, paginatedRequest, text);
            return Ok(data);
        }

        [HttpDelete("{friendId}")]
        public IActionResult RemoveFriend(int friendId)
        {
            var result = _bsFriend.RemoveFriend(JwtStoredUserInfo.UserId, friendId);
            return Ok(result);
        }
    }
}
