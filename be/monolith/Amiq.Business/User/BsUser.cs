using Amiq.Business.Utils;
using Amiq.Contracts.User;
using Amiq.DataAccess.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Business.User
{
    public class BsUser : BsServiceBase
    {
        private DaUser _daUser = new DaUser();

        public async Task<DtoUserDescription> GetUserDescriptionAsync(int userId)
        {
            return await _daUser.GetUserDescriptionAsync(userId);
        }
    }
}
