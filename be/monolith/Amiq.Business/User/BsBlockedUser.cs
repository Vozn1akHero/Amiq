using Amiq.Contracts.User;
using Amiq.DataAccess.Models.Models;
using Amiq.DataAccess.User;
using System.Linq;
using System.Threading.Tasks;

namespace Amiq.Business.User
{
    public class BsBlockedUser
    {
        private DaBlockedUser _daBlockedUser = new DaBlockedUser();

        public bool IsUserBlockedByAnotherUser(int issuerId, int userId)
        {
            return _daBlockedUser.IsUserBlockedByAnotherUser(issuerId, userId);
        }

        public void BlockUser(DtoUserBlockRequest dtoUserBlockRequest)
        {
            _daBlockedUser.BlockUser(dtoUserBlockRequest);
        }

        public async Task UnblockUser(DtoUserBlockRequest dtoUserBlockRequest)
        {
            await _daBlockedUser.UnblockUser(dtoUserBlockRequest);
        }
    }
}
