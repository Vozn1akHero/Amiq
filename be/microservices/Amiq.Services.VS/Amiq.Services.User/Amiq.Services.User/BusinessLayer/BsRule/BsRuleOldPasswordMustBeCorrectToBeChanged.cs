using Amiq.Services.User.BusinessLayer.Utils;
using Amiq.Services.User.DataAccessLayer;

namespace Amiq.Services.User.BusinessLayer.BsRule
{
    public class BsRuleOldPasswordMustBeCorrectToBeChanged : IBsRule
    {
        public string ErrorContent => "Stare hasło nie jest poprawne";

        private int _userId;
        private string _oldPassword;
        private DaoAuth _daoAuth;

        public BsRuleOldPasswordMustBeCorrectToBeChanged(DaoAuth daoAuth, int userId, string oldPassword)
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
