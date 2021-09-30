using Amiq.Contracts.Auth;
using Amiq.Contracts.User;
using Amiq.DataAccess.Models;
using Amiq.DataAccess.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.DataAccess.Auth
{
    public class DaAuth : IDaAuth
    {
        private readonly AmiqContext _amiqContext = new AmiqContext();

        public DtoUserRegistartionResult Register(DtoUserRegistration dtoUserRegistration)
        {
            var rsUserRegistartionResult = new DtoUserRegistartionResult();
            var user = new Models.Models.User
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
            catch (DbUpdateException e)
            {
                rsUserRegistartionResult.Success = false;
            }
            rsUserRegistartionResult.Success = true;
            return rsUserRegistartionResult;
        }

        public DtoUserInfo GetUserByLogin(string login)
        {
            var user = _amiqContext.Users.SingleOrDefault(e => e.Login.Equals(login));
            if (user == null) return null;
            var userInfo = new DtoUserInfo {
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
            return _amiqContext.Users.Any(e=> e.Email.Equals(email));
        }
    }
}
