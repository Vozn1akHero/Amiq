using AmicaPlus.Contracts.Auth;
using AmicaPlus.DataAccess.Auth;
using AmicaPlus.DataAccess.Models;
using AmicaPlus.ResultSets.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmicaPlus.Business.Auth
{
    public class BsAuth
    {
        private DaAuth _daAuth;

        public BsAuth()
        {
            _daAuth = new DaAuth();
        }

        public RsUserRegistartionResult Register(RsUserRegistration rsUserRegistration)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(rsUserRegistration.Password);
            rsUserRegistration.Password = hashedPassword;
            return _daAuth.Register(rsUserRegistration);
        }

        public RsUserAuthenticationResult Authenticate(RsUserAuthentication rsUserAuthentication) {
            string rawPassword = rsUserAuthentication.Password;
            bool userExists = _daAuth.GetUserByLogin(rsUserAuthentication.Login) != null;
            if (!userExists) return new RsUserAuthenticationResult { Success = false };
            string password = _daAuth.GetUserHashedPasswordByLogin(rsUserAuthentication.Login);
            bool passwordCorrect = BCrypt.Net.BCrypt.Verify(rawPassword, password);
            if(passwordCorrect) return new RsUserAuthenticationResult { Success = true };
            else return new RsUserAuthenticationResult { Success = false };
        }
    }
}
