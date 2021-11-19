﻿using Amiq.Business.Friend.BsRules;
using Amiq.Business.User.BsRule;
using Amiq.Business.Utils;
using Amiq.Contracts.User;
using Amiq.DataAccess.Models.Models;
using Amiq.DataAccess.User;
using System.Linq;
using System.Threading.Tasks;

namespace Amiq.Business.User
{
    public class BlBlockedUser : BusinessLayerBase
    {
        private DaoBlockedUser _daBlockedUser = new DaoBlockedUser();

        public bool IsUserBlockedByAnotherUser(int issuerId, int userId)
        {
            return _daBlockedUser.IsUserBlockedByAnotherUser(issuerId, userId);
        }

        public void BlockUser(DtoUserBlockRequest dtoUserBlockRequest)
        {
            CheckBsRule(new BsRuleCannotPerformActionOnCommonBlock(dtoUserBlockRequest.IssuerId, dtoUserBlockRequest.DestUserId));
            CheckBsRule(new BsRuleFriendRequestCannotExist(dtoUserBlockRequest.IssuerId, dtoUserBlockRequest.DestUserId));

            _daBlockedUser.BlockUser(dtoUserBlockRequest);
        }

        public async Task UnblockUser(DtoUserBlockRequest dtoUserBlockRequest)
        {
            await _daBlockedUser.UnblockUser(dtoUserBlockRequest);
        }
    }
}