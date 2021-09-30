using Amiq.Business.Utils;
using Amiq.Contracts.User;
using Amiq.DataAccess.User;
using Amiq.Mapping;
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

        public async Task<IEnumerable<DtoUserDescriptionBlock>> GetUserDescriptionAsync(int userId)
        {
            return await _daUser.GetUserDescriptionAsync(userId);
        }

        public async Task<DtoExtendedUserInfo> GetUserByIdAsync(int requestIssuerId, int userId)
        {
            DtoExtendedUserInfo result = new();
            var user = await _daUser.GetUserByIdAsync(userId);
            if (user == null) return null;
            result = APAutoMapper.Instance.Map<DtoExtendedUserInfo>(user);
            result.UserDescriptionBlocks = await GetUserDescriptionAsync(userId);
            return result;
        }

    }
}
