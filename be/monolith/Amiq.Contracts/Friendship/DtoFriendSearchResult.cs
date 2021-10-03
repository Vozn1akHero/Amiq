using Amiq.Contracts.User;
using System.Collections.Generic;

namespace Amiq.Contracts.Friendship
{
    public class DtoFriendSearchResult
    {
        public IEnumerable<DtoFriend> FoundsFriends { get; set; }
        public IEnumerable<DtoUserSearchResult> FoundsUsers { get; set; }
    }
}
