using Amiq.Business.Utils;
using Amiq.Contracts.Friendship;
using Amiq.DataAccess.Friendship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Business.Friend
{
    public class BsFriendship : BsServiceBase
    {
        private DaFriendship _daFriendship = new DaFriendship();

        public async Task<IEnumerable<DtoFriend>> GetUserFriendListAsync(DtoFriendListRequest dtoFriendListRequest)
        {
            return await _daFriendship.GetUserFriendListAsync(dtoFriendListRequest);
        }
    }
}
