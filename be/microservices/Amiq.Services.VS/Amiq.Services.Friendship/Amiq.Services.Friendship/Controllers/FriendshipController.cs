using Amiq.Services.Common.Contracts;
using Amiq.Services.Friendship.Base;
using Amiq.Services.Friendship.BusinessLayer;
using Amiq.Services.Friendship.Contracts.Friendship;
using Amiq.Services.Friendship.HttpClients;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Amiq.Services.Friendship.Controllers
{
    //[Authorize]
    [Route("api/friendship")]
    public class FriendshipController : AmiqBaseController
    {
        private BlFriendship _bsFriend = new BlFriendship();
        
        private UserService _userService;

        public FriendshipController(UserService userService)
        {
            _userService = userService;
        }

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

            var friendList = await _bsFriend.GetUserFriendListAsync(dtoFriendListRequest);
            
            /*foreach (var friend in friendList.Entities)
            {
                var userData = await _userService.GetUserByIdAsync(friend.UserId);
                if(userData == null)
                {
                    return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
                }
                friend.Name = userData.Name;
                friend.Surname = userData.Surname;
                friend.AvatarPath = userData.AvatarPath;
            }*/

            return Ok(friendList);
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

            var searchResult = await _bsFriend.SearchAsync(JwtStoredUserId, paginatedRequest, text);

            if(searchResult.FoundFriends.Count() < paginatedRequest.Count)
            {
                searchResult.FoundUsers = await _userService.SearchAsync(JwtStoredUserId, text, paginatedRequest);
            }

            return Ok(searchResult);
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

        [HttpGet("pb-friendship-status")]
        [Produces(typeof(DtoFriendshipStatus))]
        public IActionResult GetFriendshipStatus(int fUserId, int sUserId)
        {
            var result = _bsFriend.GetFriendshipStatus(fUserId, sUserId);
            return Ok(result);
        }

    }
}
