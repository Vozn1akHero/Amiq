using AmicaPlus.Contracts.Auth;
using AmicaPlus.DataAccess.Models;
using AmicaPlus.ResultSets.Auth;
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
            var user = new User { };
            return null;
        }
    }
}
