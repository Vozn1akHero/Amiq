using AmicaPlus.DataAccess.Auth;
using AmicaPlus.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmicaPlus.Business.Auth
{
    public class BsAuth
    {
        public List<Eftest> GetEftests()
        {
            return new DaAuth().GetEftests();
        }
    }
}
