using Amiq.Services.Common.Contracts;
using Amiq.Services.User.BusinessLayer.Utils;
using Amiq.Services.User.Contracts.User;
using Amiq.Services.User.DataAccessLayer;

namespace Amiq.Services.User.BusinessLayer
{
    public class BlUser : BusinessLayerBase
    {
        private DaoUser _daUser = new DaoUser();
        private BlBlockedUser _blockedUser = new BlBlockedUser();

        public async Task<IEnumerable<DtoUserDescriptionBlock>> GetUserDescriptionAsync(int userId)
        {
            return await _daUser.GetUserDescriptionAsync(userId);
        }

        public async Task<DtoExtendedUserInfo> GetUserByIdAsync(int requestIssuerId, int userId)
        {
            var user = await _daUser.GetUserByIdAsync(requestIssuerId, userId);
            user.UserDescriptionBlocks = await GetUserDescriptionAsync(userId);
            return user;
        }

        public async Task<IEnumerable<DtoUserSearchResult>> SearchAsync(int issuerId, string text, DtoPaginatedRequest paginatedRequest)
        {
            return await _daUser.SearchAsync(issuerId, text, paginatedRequest);
        }

        public async Task<DtoBasicUserInfo> GetBasicUserDataByIdAsync(int userId)
            => await _daUser.GetBasicUserDataByIdAsync(userId);
    }
}
