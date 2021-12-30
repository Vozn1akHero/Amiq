using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Services.User.Contracts.Friendship
{
    public class DtoFriendshipStatus
    {
        public bool IssuerReceivedFriendRequest { get; set; }
        public bool IssuerSentFriendRequest { get; set; }
        public bool IsIssuerFriend { get; set; }
    }
}
