using Amiq.Business.Friend.BsRules;
using Amiq.Business.User.BsRule;
using Amiq.Common.DbOperation;
using Amiq.Common.Enums;
using Amiq.Contracts.Friendship;
using Amiq.DataAccessLayer.Friendship;
using Amiq.DataAccessLayer.User;
using Amiq.Services.Friendship.BusinessLayer.Utils;
using Amiq.Services.Friendship.Contracts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Services.Friendship.BusinessLayer
{
    public class BlFriendRequest : BusinessLayerBase
    {
        private DaoFriendRequest daoFriendRequest = new DaoFriendRequest();
        private DaoBlockedUser _daBlockedUser = new DaoBlockedUser();

        public async Task<IEnumerable<DtoFriendRequest>> GetFriendRequestsAsync(int userId, FriendRequestType friendRequestType)
        {
            return await daoFriendRequest.GetFriendRequestsAsync(userId, friendRequestType);
        }

        public async Task<DtoCreateEntityResponse> CreateFriendRequestAsync(int creatorId, DtoCreateFriendRequest createFriendRequest)
        {
            CheckBsRule(new BsRuleCannotPerformActionOnCommonBlock(creatorId, createFriendRequest.ReceiverId));
            CheckBsRule(new BsRuleFriendRequestCannotExist(creatorId, createFriendRequest.ReceiverId));

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
            CheckBsRule(new BsRuleFriendRequestCanBeCancelledByCreator(daoFriendRequest, userId, friendRequestId));
            return await daoFriendRequest.CancelFriendRequestAsync(friendRequestId);
        }

        public async Task<DbOperationResult> CancelFriendRequestAsync(int userId, int destUserId)
        {
            var friendRequest = daoFriendRequest.GetFriendRequestByUserIds(userId, destUserId);
            CheckBsRule(new BsRuleFriendRequestCanBeCancelledByCreator(daoFriendRequest, userId, friendRequest.FriendRequestId));
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
