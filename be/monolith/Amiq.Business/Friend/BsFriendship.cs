using Amiq.Business.Friend.BsRules;
using Amiq.Business.Utils;
using Amiq.Contracts.Friendship;
using Amiq.DataAccess.Friendship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Business.Friend
{
    public class BsFriendship : BsServiceBase
    {
        private DaFriendship _daFriendship = new DaFriendship();

        public async Task<IEnumerable<DtoFriend>> GetUserFriendListAsync(DtoFriendListRequest dtoFriendListRequest)
        {
            return await _daFriendship.GetUserFriendListAsync(dtoFriendListRequest);
        }

        public DtoFriendRequest CreateFriendRequest(int issuerId, int receiverId)
        {
            CheckBsRule(new BsRuleFriendRequestAlreadyExists());
            CheckBsRule(new BsRuleRequestIssuerCannotBeBlockedByReceiver());
            CheckBsRule(new BsRuleFriendRequestAlreadyExists());

            return _daFriendship.CreateFriendRequest(issuerId, receiverId);
        }

        public async Task CancelFriendRequestAsync(int issuerId, int receiverId)
        {

        }
    }
}
