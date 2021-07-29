using AmicaPlus.Contracts.Auth;
using AmicaPlus.DataAccess.Models;
using AmicaPlus.ResultSets.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmicaPlus.DataAccess.Auth
{
    public class DaAuth
    {
        private readonly AmicaPlusContext _amicaPlusContext;

        public DaAuth()
        {
            _amicaPlusContext = new AmicaPlusContext();
        }

        public RsUserRegistartionResult Register(RsUserRegistration rsUserRegistration)
        {
            var rsUserRegistartionResult = new RsUserRegistartionResult();
            var user = new User {
                Name = rsUserRegistration.Name,
                Surname = rsUserRegistration.Surname,
                Birthdate = rsUserRegistration.Birthdate,
                Login = rsUserRegistration.Login,
                Password = rsUserRegistration.Password
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

        public List<Eftest> GetEftests()
        {
            return _amicaPlusContext.Eftests.ToList();
        }
    }
}
