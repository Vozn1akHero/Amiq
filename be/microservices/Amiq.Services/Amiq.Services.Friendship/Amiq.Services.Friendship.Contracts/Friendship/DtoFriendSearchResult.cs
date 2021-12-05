using Amiq.Contracts.User;
using System.Collections.Generic;

namespace Amiq.Services.Friendship.Contracts.Friendship
{
    public class DtoFriendSearchResult
    {
        public IEnumerable<DtoFriend> FoundFriends { get; set; }
        public IEnumerable<DtoUserSearchResult> FoundUsers { get; set; }
    }
}
