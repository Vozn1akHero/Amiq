using Amiq.BusinessLayer.Utils;
using Amiq.DataAccessLayer.Auth;

namespace Amiq.BusinessLayer.Auth.BsRules
{
    public class EmailShouldBeUniqueBsRule : IBsRule
    {
        private IDaoAuth _daAuth;
        private string _email;

        public EmailShouldBeUniqueBsRule(IDaoAuth daAuth, string email)
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
