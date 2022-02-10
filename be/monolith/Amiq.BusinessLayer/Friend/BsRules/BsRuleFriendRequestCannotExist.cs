using Amiq.BusinessLayer.Utils;
using Amiq.DataAccessLayer.Friendship;

namespace Amiq.BusinessLayer.Friend.BsRules
{
    public class BsRuleFriendRequestCannotExist : IBsRule
    {
        private int _fUserId;
        private int _sUserId;

        public BsRuleFriendRequestCannotExist(int fUserId, int sUserId)
        {
            _fUserId = fUserId;
            _sUserId = sUserId;
        }

        public string ErrorContent => "Zaproszenie do znajomych już istnieje";

        public bool CheckBsRule()
        {
            var dao = new DaoFriendRequest();
            return !dao.FriendRequestExists(_fUserId, _sUserId);
        }
    }
}
