using Amiq.Services.Base.Controllers;
using Amiq.Services.Common.Contracts;
using Amiq.Services.Common.Enums;
using Amiq.Services.Friendship.BusinessLayer;
using Amiq.Services.Friendship.Contracts.Friendship;
using Amiq.Services.Friendship.DataAccessLayer;
using Amiq.Services.Friendship.Messaging;
using Amiq.Services.Friendship.Messaging.IntegrationEvents;
using Microsoft.AspNetCore.Mvc;

namespace Amiq.Services.Friendship.Controllers
{
    public class FriendRequestController : AmiqBaseController
    {
        private BlFriendRequest _bsFriendRequest = new BlFriendRequest();
        private DaoFriendRequest _daoFriendRequest = new DaoFriendRequest();
        private DaoFriendship _daoFriendship = new DaoFriendship();


        [HttpGet("friend-requests")]
        public async Task<IActionResult> GetFriendRequestList([FromQuery] string friendRequestType)
        {
            FriendRequestType enFriendRequestType = EnumExtensions.GetValueByAlt<FriendRequestType>(friendRequestType);

            var friendRequests = await _bsFriendRequest.GetFriendRequestsAsync(JwtStoredUserId, enFriendRequestType);

            /*foreach (var friendRequest in friendRequests)
            {
                var receiver = await _userService.GetUserByIdAsync(friendRequest.ReceiverId);
                var creator = await _userService.GetUserByIdAsync(friendRequest.IssuerId);

                if (receiver == null || creator == null)
                {
                    return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
                }

                friendRequest.Receiver.Name = receiver.Name;
                friendRequest.Receiver.Surname = receiver.Surname;
                friendRequest.Receiver.AvatarPath = receiver.AvatarPath;

                friendRequest.Creator.Name = creator.Name;
                friendRequest.Creator.Surname = creator.Surname;
                friendRequest.Creator.AvatarPath = creator.AvatarPath;
            }*/

            return Ok(friendRequests);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DtoCreateFriendRequest dtoCreateFriendRequest)
        {
            var result = await _bsFriendRequest.CreateFriendRequestAsync(JwtStoredUserId, dtoCreateFriendRequest);
            return CreatedAtAction(nameof(Create), result);
        }

        [HttpPost("accept-friend-request/{friendRequestId}")]
        public async Task<IActionResult> AcceptFriendRequest([FromRoute] Guid friendRequestId)
        {
            DtoCreateEntityResponse createEntityResponse = await _bsFriendRequest.AcceptFriendRequestAsync(JwtStoredUserId, friendRequestId);

            if (createEntityResponse.Result)
            {
                var friendRequest = await _daoFriendRequest.GetFriendRequestByIdAsync(friendRequestId);
                RabbitMQPublisher.Publish(new FriendshipRequestAccepted(friendRequest.FriendRequestId, friendRequest.IssuerId, friendRequest.ReceiverId));

                var obj = (DataAccessLayer.Models.Friendship)createEntityResponse.Entity;
                RabbitMQPublisher.Publish(new FriendshipModificationEvent(obj.FriendshipId, obj.FirstUserId, obj.SecondUserId, "C"));
            }

            return Ok(createEntityResponse);
        }

        [HttpPost("accept-friend-request-by-dest-user/{destUserId}")]
        public async Task<IActionResult> AcceptFriendRequestByDestUserId([FromRoute] int destUserId)
        {
            var result = await _bsFriendRequest.AcceptFriendRequestAsync(JwtStoredUserId, destUserId);
            return Ok(result);
        }

        [HttpPost("cancel-friend-request/{friendRequestId}")]
        public async Task<IActionResult> CancelFriendRequest([FromRoute] Guid friendRequestId)
        {
            var result = await _bsFriendRequest.CancelFriendRequestAsync(JwtStoredUserId, friendRequestId);
            return Ok(result);
        }

        [HttpPost("cancel-friend-request-by-dest-user/{destUserId}")]
        public async Task<IActionResult> CancelFriendRequestByDestUserId([FromRoute] int destUserId)
        {
            var result = await _bsFriendRequest.CancelFriendRequestAsync(JwtStoredUserId, destUserId);
            return Ok(result);
        }

        [HttpPost("reject-friend-request/{friendRequestId}")]
        public async Task<IActionResult> RejectFriendRequest([FromRoute] Guid friendRequestId)
        {
            var result = await _bsFriendRequest.RejectFriendRequestAsync(JwtStoredUserId, friendRequestId);
            return Ok(result);
        }

        [HttpPost("reject-friend-request-by-dest-user/{destUserId}")]
        public async Task<IActionResult> RejectFriendRequestByDestUserId([FromRoute] int destUserId)
        {
            var result = await _bsFriendRequest.RejectFriendRequestAsync(JwtStoredUserId, destUserId);
            return Ok(result);
        }

    }
}
