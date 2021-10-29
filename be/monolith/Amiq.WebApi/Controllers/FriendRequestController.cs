using Amiq.Business.Friend;
using Amiq.Common.Enums;
using Amiq.Contracts.Friendship;
using Amiq.WebApi.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Amiq.WebApi.Controllers
{
    public class FriendRequestController : AmiqBaseController
    {
        private BlFriendRequest _bsFriendRequest = new BlFriendRequest();

        [HttpGet("friend-requests")]
        public async Task<IActionResult> GetFriendRequestList([FromQuery] string friendRequestType)
        {
            FriendRequestType enFriendRequestType = EnumExtensions.GetValueByAlt<FriendRequestType>(friendRequestType);
            var data = await _bsFriendRequest.GetFriendRequestsAsync(JwtStoredUserInfo.UserId, enFriendRequestType);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DtoCreateFriendRequest dtoCreateFriendRequest)
        {
            var result = await _bsFriendRequest.CreateFriendRequestAsync(JwtStoredUserInfo.UserId, dtoCreateFriendRequest);
            return CreatedAtAction(nameof(Create), result);
        }

        [HttpPost("accept-friend-request/{friendRequestId}")]
        public async Task<IActionResult> AcceptFriendRequest([FromRoute] Guid friendRequestId)
        {
            var result = await _bsFriendRequest.AcceptFriendRequestAsync(JwtStoredUserInfo.UserId, friendRequestId);
            return Ok(result);
        }

        [HttpPost("cancel-friend-request")]
        public async Task<IActionResult> CancelFriendRequest([FromRoute] Guid friendRequestId)
        {
            var result = await _bsFriendRequest.CancelFriendRequestAsync(JwtStoredUserInfo.UserId, friendRequestId);
            return Ok(result);
        }

        [HttpPost("reject-friend-request")]
        public async Task<IActionResult> RejectFriendRequest([FromRoute] Guid friendRequestId)
        {
            var result = await _bsFriendRequest.RejectFriendRequestAsync(JwtStoredUserInfo.UserId, friendRequestId);
            return Ok(result);
        }

    }
}
