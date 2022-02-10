using Amiq.DataAccessLayer.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.BusinessLayer.User
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
