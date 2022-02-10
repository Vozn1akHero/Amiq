using Amiq.BusinessLayer.Utils;
using Amiq.DataAccessLayer.Auth;
using System;

namespace Amiq.BusinessLayer.Auth.BsRules
{
    public class BsRuleOldPasswordMustBeCorrectToBeChanged : IBsRule
    {
        public string ErrorContent => "Stare hasło nie jest poprawne";

        private int _userId;
        private string _oldPassword;
        private IDaoAuth _daoAuth;

        public BsRuleOldPasswordMustBeCorrectToBeChanged(IDaoAuth daoAuth, int userId, string oldPassword)
        {
            _userId = userId;
            _oldPassword = oldPassword ?? throw new ArgumentNullException();
            _daoAuth = daoAuth;
        }

        public bool CheckBsRule()
        {
            return _daoAuth.IsPasswordCorrect(_userId, _oldPassword);
        }
    }
}
