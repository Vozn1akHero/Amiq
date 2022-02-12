using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Services.Friendship.Contracts.User
{
    public class DtoUserSearchResult
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string AvatarPath { get; set; }
        public bool BlockedByIssuer { get; set; }
        public bool IssuerBlocked { get; set; }
        public bool IsIssuerFriend { get; set; }
        public bool IssuerReceivedFriendRequest { get; set; }
        public bool IssuerSentFriendRequest { get; set; }
    }
}
