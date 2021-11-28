using Amiq.Business.Utils;
using Amiq.DataAccessLayer.User;
using System;

namespace Amiq.Business.User.BsRule
{
    /// <summary>
    /// Reguła sprawdzająca czy użytkownik jest zablokowany przez innego 
    /// użytkownika lub odwrotnie i może wykonać jakieś żądanie w stosunku do tego użytkownika.
    /// </summary>
    public class BsRuleCannotPerformActionOnCommonBlock : IBsRule
    {
        private int _requestIssuerId;
        private int _userId;

        public BsRuleCannotPerformActionOnCommonBlock(int requestIssuerId, int userId)
        {
            _requestIssuerId = requestIssuerId;
            _userId = userId;
        }

        public string ErrorContent => throw new NotImplementedException();

        public bool CheckBsRule() => !new DaoBlockedUser().OneWayBlockExists(_userId, _requestIssuerId);
    }
}
