using System;
using System.Collections.Generic;

#nullable disable

namespace Amiq.DataAccess.Models.Models
{
    public partial class FriendRequest
    {
        public Guid FriendRequestId { get; set; }
        public int IssuerId { get; set; }
        public int ReceiverId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual User Issuer { get; set; }
        public virtual User Receiver { get; set; }
    }
}
