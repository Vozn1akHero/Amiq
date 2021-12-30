using Amiq.Services.User.BusinessLayer.Utils;
using Amiq.Services.User.DataAccessLayer;

namespace Amiq.Services.User.BusinessLayer.BsRule
{
    public class BsRuleEmailShouldBeUniqueBsRule : IBsRule
    {
        private DaoAuth _daAuth;
        private string _email;

        public BsRuleEmailShouldBeUniqueBsRule(DaoAuth daAuth, string email)
        {
            _daAuth = daAuth;
            _email = email;
        }

        public string ErrorContent => "Email already taken";

        public bool CheckBsRule()
        {
            return _daAuth.EmailExists(_email);
        }
    }
}
