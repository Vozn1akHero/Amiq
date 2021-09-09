using Amiq.Business.Utils;
using Amiq.DataAccess.User;
using System;

namespace Amiq.Business.User.BsRule
{
    /// <summary>
    /// Reguła sprawdzająca czy użytkownik jest zablokowany przez innego 
    /// użytkownika i może wykonać jakieś żądanie w stosunku do tego użytkownika.
    /// Wykorzystywana w wielu kontekstach 
    /// </summary>
    public class BsRuleBlockedUserCannotPerformAction : IBsRule
    {
        private DaBlockedUser _daBlockedUser;
        private int _requestIssuerId;
        private int _userId;

        public BsRuleBlockedUserCannotPerformAction(DaBlockedUser daBlockedUser, int requestIssuerId, int userId)
        {
            _daBlockedUser = daBlockedUser;
            _requestIssuerId = requestIssuerId;
            _userId = userId;
        }

        public string ErrorContent => throw new NotImplementedException();

        public bool CheckBsRule() => _daBlockedUser.IsUserBlockedByAnotherUser(_userId, _requestIssuerId);
    }
}
