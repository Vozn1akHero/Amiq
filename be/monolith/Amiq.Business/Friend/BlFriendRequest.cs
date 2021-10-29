using Amiq.Business.Friend.BsRules;
using Amiq.Business.Utils;
using Amiq.Common.DbOperation;
using Amiq.Common.Enums;
using Amiq.Contracts;
using Amiq.Contracts.Friendship;
using Amiq.DataAccess.Friendship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Business.Friend
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
            CheckBsRule(new BsRuleRequestIssuerCannotBeBlockedByReceiver());
            CheckBsRule(new BsRuleFriendRequestAlreadyExists());

            return await daoFriendRequest.CreateFriendRequestAsync(creatorId, createFriendRequest);
        }

        public async Task<DbOperationResult> AcceptFriendRequestAsync(int userId, Guid friendRequestId)
        {
            return await daoFriendRequest.AcceptFriendRequestAsync(friendRequestId);
        }

        public async Task<DbOperationResult> CancelFriendRequestAsync(int userId, Guid friendRequestId)
        {
            CheckBsRule(new BsRuleFriendRequestCanBeCancelledByCreator(daoFriendRequest, userId, friendRequestId));
            return await daoFriendRequest.CancelFriendRequestAsync(friendRequestId);
        }

        public async Task<DbOperationResult> RejectFriendRequestAsync(int userId, Guid friendRequestId)
        {
            
            return await daoFriendRequest.RejectFriendRequestAsync(friendRequestId);
        }
    }
}
