using Amiq.DataAccessLayer.Friendship;
using Amiq.Services.Friendship.BusinessLayer.Utils;
using System;

namespace Amiq.Services.Friendship.BusinessLayer.BsRules
{
    public class BsRuleFriendRequestCanBeCancelledByCreator : IBsRule
    {
        public string ErrorContent => "Prośba o dodanie do znajomych może zostać usunięta przez twórcę";

        private DaoFriendRequest _daoFriendRequest;

        private int _userId;
        private Guid _friendRequestId;

        public BsRuleFriendRequestCanBeCancelledByCreator(DaoFriendRequest daoFriendRequest, int userId, Guid friendRequestId)
        {
            _daoFriendRequest = daoFriendRequest;
            _userId = userId;
            _friendRequestId = friendRequestId;
        }

        public bool CheckBsRule()
        {
            return _daoFriendRequest.IsFriendRequestCreatedByUser(_userId, _friendRequestId);
        }
    }
}
