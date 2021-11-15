using Amiq.DataAccess.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Business.User
{
    public class BlUserSession
    {
        private DaoUserSession _daoUserOnlineStatus = new DaoUserSession();

        public async void SignalAsync(int userId)
        {
            await _daoUserOnlineStatus.SignalAsync(userId, DateTime.Now);
        }
    }
}
