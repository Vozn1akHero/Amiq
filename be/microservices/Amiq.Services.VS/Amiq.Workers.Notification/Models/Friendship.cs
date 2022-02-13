using System;
using System.Collections.Generic;

namespace Amiq.Workers.Notification.Models
{
    public partial class Friendship
    {
        public Guid FriendshipId { get; set; }
        public int FirstUserId { get; set; }
        public int SecondUserId { get; set; }
    }
}
