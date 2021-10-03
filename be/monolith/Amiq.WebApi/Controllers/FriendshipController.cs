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
        private BsFriendship _bsFriend = new BsFriendship();

        [HttpGet("friend-list/{userId}")]
        [Produces(typeof(IEnumerable<DtoFriend>))]
        [ProducesResponseType(((int)HttpStatusCode.OK))]
        [ProducesResponseType(499)]
        public async Task<IActionResult> GetUserFriendListAsync(int userId, [FromQuery]DtoFriendListRequest dtoFriendListRequest, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return new StatusCodeResult(499);
            }
            //dtoFriendListRequest.IssuerId = JwtStoredUserInfo.UserId;
            dtoFriendListRequest.IssuerId = userId;
            var data = await _bsFriend.GetUserFriendListAsync(dtoFriendListRequest);
            return Ok(data);
        }

        [HttpPost("create-friend-request")]
        public IActionResult CreateFriendRequest([FromQuery] int receiverId)
        {
            var userId = JwtStoredUserInfo.UserId;
            var friendRequest = _bsFriend.CreateFriendRequest(userId, receiverId);
            return CreatedAtAction(nameof(CreateFriendRequest), friendRequest);
        }

        [HttpPost("remove-friend-request")]
        public async Task<IActionResult> RemoveFriendRequestAsync([FromQuery] int receiverId)
        {
            await _bsFriend.CancelFriendRequestAsync(JwtStoredUserInfo.UserId, receiverId);
            return Ok();
        }

        [HttpGet("friend-requests")]
        public async Task<IActionResult> GetFriendRequest([FromQuery] DtoFriendListRequest dtoFriendListRequest)
        {
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
    }
}
