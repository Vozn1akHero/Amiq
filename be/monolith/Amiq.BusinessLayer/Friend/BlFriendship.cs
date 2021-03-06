using Amiq.BusinessLayer.Friend.BsRules;
using Amiq.BusinessLayer.User;
using Amiq.BusinessLayer.Utils;
using Amiq.Common.DbOperation;
using Amiq.Contracts.Friendship;
using Amiq.Contracts.Utils;
using Amiq.DataAccessLayer.Friendship;
using Amiq.DataAccessLayer.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.BusinessLayer.Friend
{
    public class BlFriendship : BusinessLayerBase
    {
        private DaoFriendship _daFriendship = new DaoFriendship();

        public async Task<DtoListResponseOf<DtoFriend>> GetUserFriendListAsync(DtoGetFriendListRequest dtoFriendListRequest)
        {
            return await _daFriendship.GetUserFriendListAsync(dtoFriendListRequest);
        }

        public async Task CancelFriendRequestAsync(int issuerId, int receiverId)
        {

        }

        /// <summary>
        /// Metoda wyszukująca znajomych według imienia i nazwiska
        /// Jeśli znaleziona lista znajomych jest mniejsza niż wskazana w dto
        /// liczba encji do wyjściowego dto zostanie dołączona lista innych użytkowników
        /// </summary>
        /// <param name="issuerId"></param>
        /// <param name="paginatedRequest"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public async Task<DtoFriendSearchResult> SearchAsync(int issuerId, DtoPaginatedRequest paginatedRequest, string text)
        {
            DtoFriendSearchResult result = new();
            var foundFriends = await _daFriendship.SearchForUserFriendsAsync(issuerId, paginatedRequest, text);
            result.FoundFriends = foundFriends;
            if (foundFriends.Count() < paginatedRequest.Count)
            {
                var bsUser = new BlUser();
                var foundUsers = await bsUser.SearchAsync(issuerId, text, paginatedRequest);
                result.FoundUsers = foundUsers;
            }
            return result;
        }

        public DbOperationResult RemoveFriend(int userId, int friendId)
        {
            return _daFriendship.RemoveFriend(userId, friendId);
        }
    }
}
