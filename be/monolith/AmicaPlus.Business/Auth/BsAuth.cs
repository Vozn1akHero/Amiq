using AmicaPlus.Business.Auth.Rules;
using AmicaPlus.Business.Utils;
using AmicaPlus.Contracts;
using AmicaPlus.Contracts.Auth;
using AmicaPlus.DataAccess.Auth;
using AmicaPlus.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmicaPlus.Business.Auth
{
    public class BsAuth : BsServiceBase
    {
        private DaAuth _daAuth;

        public BsAuth()
        {
            _daAuth = new DaAuth();
        }

        public DtoUserRegistartionResult Register(DtoUserRegistration dtoUserRegistration)
        {
            var output = new DtoUserRegistartionResult();
            try
            {
                CheckBsRule(new EmailShouldBeUniqueBsRule(_daAuth, dtoUserRegistration.Email));
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dtoUserRegistration.Password);
                dtoUserRegistration.Password = hashedPassword;
                return _daAuth.Register(dtoUserRegistration);
            }
            catch (BsIsBrokenException businessRuleBrokenException)
            {
                output.Success = false;
                output.Message = businessRuleBrokenException.Message;
            }
            catch (Exception e)
            {
                output.Success = false;
                output.Message = e.Message;
            }
            return output;
        }

        public DtoUserAuthenticationResult Authenticate(DtoUserAuthentication rsUserAuthentication) {
            string rawPassword = rsUserAuthentication.Password;
            var user = _daAuth.GetUserByLogin(rsUserAuthentication.Login);
            bool userExists = user != null;
            if (!userExists) return new DtoUserAuthenticationResult { Success = false };
            string password = _daAuth.GetUserHashedPasswordByLogin(rsUserAuthentication.Login);
            bool passwordCorrect = BCrypt.Net.BCrypt.Verify(rawPassword, password);
            if(passwordCorrect) return new DtoUserAuthenticationResult { 
                Success = true, 
                JwtBase = new DtoJwtBase { 
                    UserId = user.UserId,
                    UserEmail = user.Email
                } 
            };
            else return new DtoUserAuthenticationResult { Success = false };
        }
    }
}
