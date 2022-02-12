using Amiq.Services.User.BusinessLayer.BsRule;
using Amiq.Services.User.BusinessLayer.Utils;
using Amiq.Services.User.Contracts.User;
using Amiq.Services.User.DataAccessLayer;

namespace Amiq.Services.User.BusinessLayer
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

            //TODO
            //CheckBsRule(new BsRuleFriendRequestCannotExist(dtoUserBlockRequest.IssuerId, dtoUserBlockRequest.DestUserId));

            _daBlockedUser.BlockUser(dtoUserBlockRequest);
        }

        public async Task UnblockUser(DtoUserBlockRequest dtoUserBlockRequest)
        {
            await _daBlockedUser.UnblockUser(dtoUserBlockRequest);
        }
    }
}
