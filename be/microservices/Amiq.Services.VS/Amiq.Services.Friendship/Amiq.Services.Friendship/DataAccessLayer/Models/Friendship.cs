using System;
using System.Collections.Generic;

namespace Amiq.Services.Friendship.DataAccessLayer.Models
{
    public partial class Friendship
    {
        public Guid FriendshipId { get; set; }
        public int FirstUserId { get; set; }
        public int SecondUserId { get; set; }

        public virtual User FirstUser { get; set; } = null!;
        public virtual User SecondUser { get; set; } = null!;
    }
}
