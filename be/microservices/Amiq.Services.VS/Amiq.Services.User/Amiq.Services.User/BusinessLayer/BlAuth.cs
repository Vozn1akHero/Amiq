using Amiq.Services.Common.DbOperation;
using Amiq.Services.User.BusinessLayer.BsRule;
using Amiq.Services.User.BusinessLayer.Utils;
using Amiq.Services.User.Contracts.Auth;
using Amiq.Services.User.DataAccessLayer;

namespace Amiq.Services.User.BusinessLayer
{
    public class BlAuth : BusinessLayerBase
    {
        private DaoAuth _daAuth = new DaoAuth();

        public DbOperationResult ChangePassword(int userId, DtoChangeUserPassword dtoChangeUserPassword)
        {
            DbOperationResult result = new();

            try
            {
                if (!string.IsNullOrEmpty(dtoChangeUserPassword.OldPassword))
                {
                    string hashedPassedOldPassword = BCrypt.Net.BCrypt.HashPassword(dtoChangeUserPassword.OldPassword);
                    CheckBsRule(new BsRuleOldPasswordMustBeCorrectToBeChanged(_daAuth, userId, hashedPassedOldPassword));
                    string hashedNewPassword = BCrypt.Net.BCrypt.HashPassword(dtoChangeUserPassword.NewPassword);
                    _daAuth.ChangeUserPassword(userId, hashedNewPassword);
                }
            }
            catch (BsRuleIsBrokenException bsRuleBrokenException)
            {
                result.Success = false;
                result.Message = bsRuleBrokenException.Message;
            }

            return result;
        }

        public DbOperationResult ChangeEmail(int userId, string email)
        {
            DbOperationResult result = new();

            try
            {
                if (!string.IsNullOrEmpty(email))
                {
                    CheckBsRule(new BsRuleEmailShouldBeUniqueBsRule(_daAuth, email));
                    _daAuth.ChangeUserEmail(userId, email);
                }
            }
            catch (BsRuleIsBrokenException bsRuleBrokenException)
            {
                result.Success = false;
                result.Message = bsRuleBrokenException.Message;
            }

            return result;
        }

        public DtoUserRegistartionResult Register(DtoUserRegistration dtoUserRegistration)
        {
            var output = new DtoUserRegistartionResult();
            try
            {
                CheckBsRule(new BsRuleEmailShouldBeUniqueBsRule(_daAuth, dtoUserRegistration.Email));
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dtoUserRegistration.Password);
                dtoUserRegistration.Password = hashedPassword;
                return _daAuth.Register(dtoUserRegistration);
            }
            catch (BsRuleIsBrokenException businessRuleBrokenException)
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

        public DtoUserAuthenticationResult Authenticate(DtoUserAuthentication rsUserAuthentication)
        {
            string rawPassword = rsUserAuthentication.Password;
            var user = _daAuth.GetUserByLogin(rsUserAuthentication.Login);
            bool userExists = user != null;
            if (!userExists) return new DtoUserAuthenticationResult { Success = false };
            string password = _daAuth.GetUserHashedPasswordByLogin(rsUserAuthentication.Login);
            bool passwordCorrect = BCrypt.Net.BCrypt.Verify(rawPassword, password);
            if (passwordCorrect) return new DtoUserAuthenticationResult
            {
                Success = true,
                JwtBase = new DtoJwtBase
                {
                    UserId = user.UserId,
                    UserEmail = user.Email,
                    UserName = user.Name,
                    UserSurname = user.Surname
                }
            };
            else return new DtoUserAuthenticationResult { Success = false };
        }
    }
}
