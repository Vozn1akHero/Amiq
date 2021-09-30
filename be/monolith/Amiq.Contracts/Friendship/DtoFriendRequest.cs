using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Contracts.Friendship
{
    public class DtoFriendRequest
    {
        public Guid FriendRequestId { get; set; }
        public int IssuerId { get; set; }
        public int ReceiverId { get; set; }
    }
}
