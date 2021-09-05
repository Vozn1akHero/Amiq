using Amiq.Business.Friend;
using Amiq.Contracts.Auth;
using Amiq.Contracts.Friendship;
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

        [HttpGet("list")]
        [Produces(typeof(IEnumerable<DtoFriend>))]
        [ProducesResponseType(((int)HttpStatusCode.OK))]
        [ProducesResponseType(499)]
        public async Task<IActionResult> GetUserFriendListAsync([FromQuery]DtoFriendListRequest dtoFriendListRequest, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return new StatusCodeResult(499);
            }
            DtoJwtStoredUserInfo dtoJwtStoredUserInfo = (DtoJwtStoredUserInfo)HttpContext.Items["user"];
            dtoFriendListRequest.IssuerId = dtoJwtStoredUserInfo.UserId;
            var data = await _bsFriend.GetUserFriendListAsync(dtoFriendListRequest);
            return Ok(data);
        }

        [HttpPost("post-request")]
        public async Task<IActionResult> PostFriendRequestAsync()
        {
            return Ok();
        }
    }
}
