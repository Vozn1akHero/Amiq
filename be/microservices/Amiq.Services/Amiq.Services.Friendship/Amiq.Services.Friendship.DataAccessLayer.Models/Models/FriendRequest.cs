using System;
using System.Collections.Generic;

namespace Amiq.Services.Friendship.DataAccessLayer.Models.Models
{
    public partial class FriendRequest
    {
        public Guid FriendRequestId { get; set; }
        public int IssuerId { get; set; }
        public int ReceiverId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
