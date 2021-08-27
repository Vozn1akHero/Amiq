using AmicaPlus.Contracts.Auth;
using AmicaPlus.Contracts.User;
using AmicaPlus.DataAccess.Models;
using AmicaPlus.DataAccess.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AmicaPlus.DataAccess.Auth
{
    public class DaAuth : IDaAuth
    {
        private readonly AmicaPlusContext _amicaPlusContext = new AmicaPlusContext();

        public DtoUserRegistartionResult Register(DtoUserRegistration dtoUserRegistration)
        {
            var rsUserRegistartionResult = new DtoUserRegistartionResult();
            var user = new User {
                Name = dtoUserRegistration.Name,
                Surname = dtoUserRegistration.Surname,
                Birthdate = dtoUserRegistration.Birthdate,
                Login = dtoUserRegistration.Login,
                Password = dtoUserRegistration.Password
            };
            try
            {
                _amicaPlusContext.Users.Add(user);
                _amicaPlusContext.SaveChanges();
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
            var user = _amicaPlusContext.Users.SingleOrDefault(e => e.Login.Equals(login));
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
            var user = _amicaPlusContext.Users.SingleOrDefault(e => e.Login.Equals(login));
            return user?.Password;
        }

        public bool EmailExists(string email)
        {
            throw new NotImplementedException();
        }
    }
}
