﻿using Amiq.Business.Friend.BsRules;
using Amiq.Business.User;
using Amiq.Business.Utils;
using Amiq.Common.DbOperation;
using Amiq.Contracts.Friendship;
using Amiq.Contracts.Utils;
using Amiq.DataAccess.Friendship;
using Amiq.DataAccess.User;
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

        public DtoFriendRequest CreateFriendRequest(int issuerId, int receiverId)
        {
            CheckBsRule(new BsRuleFriendRequestAlreadyExists());
            CheckBsRule(new BsRuleRequestIssuerCannotBeBlockedByReceiver());
            CheckBsRule(new BsRuleFriendRequestAlreadyExists());

            return _daFriendship.CreateFriendRequest(issuerId, receiverId);
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
                var bsUser = new BsUser();
                var foundUsers = await bsUser.SearchAsync(issuerId, text, paginatedRequest);
                result.FoundUsers = foundUsers;
            }
            return result;
        }
    }
}
