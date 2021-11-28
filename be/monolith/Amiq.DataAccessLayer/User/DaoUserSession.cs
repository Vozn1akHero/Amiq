using Amiq.DataAccessLayer.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.DataAccessLayer.User
{
    public class DaoUserSession
    {
        private AmiqContext _amiqContext = new AmiqContext();

        public async Task SignalAsync(int userId, DateTime time)
        {
            var entity = new Session
            {
                UserId = userId,
                StartedAt = time,

            };
            _amiqContext.Sessions.Add(entity);
            await _amiqContext.SaveChangesAsync();
        }
    }
}
