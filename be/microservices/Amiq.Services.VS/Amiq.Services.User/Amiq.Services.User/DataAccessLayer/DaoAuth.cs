using Amiq.Services.User.Contracts.Auth;
using Amiq.Services.User.Contracts.User;
using Amiq.Services.User.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Amiq.Services.User.DataAccessLayer
{
    public class DaoAuth
    {
        private readonly AmiqUserContext _amiqContext = new AmiqUserContext();

        public DtoUserRegistartionResult Register(DtoUserRegistration dtoUserRegistration)
        {
            var rsUserRegistartionResult = new DtoUserRegistartionResult();
            var user = new Models.User
            {
                Name = dtoUserRegistration.Name,
                Surname = dtoUserRegistration.Surname,
                Birthdate = dtoUserRegistration.Birthdate,
                Login = dtoUserRegistration.Login,
                Password = dtoUserRegistration.Password,
                Email = dtoUserRegistration.Email,
                Sex = dtoUserRegistration.Sex
            };
            try
            {
                _amiqContext.Users.Add(user);
                _amiqContext.SaveChanges();
            }
            catch (DbUpdateException)
            {
                rsUserRegistartionResult.Success = false;
            }
            rsUserRegistartionResult.Success = true;
            rsUserRegistartionResult.BasicUserInfo = new DtoBasicUserInfo { 
                UserId = user.UserId,
                Name = user.Name,
                Surname = user.Surname,
                AvatarPath = user.AvatarPath
            };
            return rsUserRegistartionResult;
        }

        public DtoUserInfo GetUserByLogin(string login)
        {
            var user = _amiqContext.Users.SingleOrDefault(e => e.Login.Equals(login));
            if (user == null) return null;
            var userInfo = new DtoUserInfo
            {
                Name = user.Name,
                Surname = user.Surname,
                Birthdate = user.Birthdate,
                Email = user.Email,
                UserId = user.UserId
            };
            return userInfo;
        }

        public string GetUserHashedPasswordByLogin(string login)
        {
            var user = _amiqContext.Users.SingleOrDefault(e => e.Login.Equals(login));
            return user?.Password;
        }

        public bool EmailExists(string email)
        {
            return _amiqContext.Users.Any(e => e.Email.Equals(email));
        }

        public bool IsPasswordCorrect(int userId, string password)
        {
            var user = _amiqContext.Users.AsNoTracking().SingleOrDefault(e => e.UserId == userId);
            if (user != null)
            {
                string currentPass = user.Password;
                return currentPass.Equals(password);
            }
            return false;
        }

        public void ChangeUserPassword(int userId, string encryptedPassword)
        {
            var user = _amiqContext.Users.Find(userId);
            user.Password = encryptedPassword;
            _amiqContext.SaveChanges();
        }

        public void ChangeUserEmail(int userId, string email)
        {
            var user = _amiqContext.Users.Find(userId);
            user.Password = email;
            _amiqContext.SaveChanges();
        }
    }
}
