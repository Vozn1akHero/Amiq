using Amiq.Services.Friendship.BusinessLayer.Utils;
using Amiq.Services.Friendship.Common.DbOperation;
using Amiq.Services.Friendship.Common.Enums;
using Amiq.Services.Friendship.Contracts.Friendship;
using Amiq.Services.Friendship.Contracts.Utils;
using Amiq.Services.Friendship.DataAccessLayer;

namespace Amiq.Services.Friendship.BusinessLayer
{
    public class BlFriendRequest : BusinessLayerBase
    {
        private DaoFriendRequest daoFriendRequest = new DaoFriendRequest();

        public async Task<IEnumerable<DtoFriendRequest>> GetFriendRequestsAsync(int userId, FriendRequestType friendRequestType)
        {
            return await daoFriendRequest.GetFriendRequestsAsync(userId, friendRequestType);
        }

        public async Task<DtoCreateEntityResponse> CreateFriendRequestAsync(int creatorId, DtoCreateFriendRequest createFriendRequest)
        {
            /*CheckBsRule(new BsRuleCannotPerformActionOnCommonBlock(creatorId, createFriendRequest.ReceiverId));
            CheckBsRule(new BsRuleFriendRequestCannotExist(creatorId, createFriendRequest.ReceiverId));*/

            return await daoFriendRequest.CreateFriendRequestAsync(creatorId, createFriendRequest);
        }

        public async Task<DbOperationResult> AcceptFriendRequestAsync(int userId, Guid friendRequestId)
        {
            return await daoFriendRequest.AcceptFriendRequestAsync(friendRequestId);
        }

        public async Task<DbOperationResult> AcceptFriendRequestAsync(int userId, int destUserId)
        {
            var friendRequest = daoFriendRequest.GetFriendRequestByUserIds(userId, destUserId);
            return await daoFriendRequest.AcceptFriendRequestAsync(friendRequest.FriendRequestId);
        }

        public async Task<DbOperationResult> CancelFriendRequestAsync(int userId, Guid friendRequestId)
        {
            //CheckBsRule(new BsRuleFriendRequestCanBeCancelledByCreator(daoFriendRequest, userId, friendRequestId));
            return await daoFriendRequest.CancelFriendRequestAsync(friendRequestId);
        }

        public async Task<DbOperationResult> CancelFriendRequestAsync(int userId, int destUserId)
        {
            var friendRequest = daoFriendRequest.GetFriendRequestByUserIds(userId, destUserId);
            //CheckBsRule(new BsRuleFriendRequestCanBeCancelledByCreator(daoFriendRequest, userId, friendRequest.FriendRequestId));
            return await daoFriendRequest.CancelFriendRequestAsync(friendRequest.FriendRequestId);
        }

        public async Task<DbOperationResult> RejectFriendRequestAsync(int userId, Guid friendRequestId)
        {
            return await daoFriendRequest.RejectFriendRequestAsync(friendRequestId);
        }

        public async Task<DbOperationResult> RejectFriendRequestAsync(int userId, int destUserId)
        {
            var friendRequest = daoFriendRequest.GetFriendRequestByUserIds(userId, destUserId);
            return await daoFriendRequest.RejectFriendRequestAsync(friendRequest.FriendRequestId);
        }
    }
}
