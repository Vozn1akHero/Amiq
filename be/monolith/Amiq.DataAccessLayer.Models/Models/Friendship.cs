using System;
using System.Collections.Generic;

namespace Amiq.DataAccessLayer.Models.Models
{
    public partial class Friendship
    {
        public Guid FriendshipId { get; set; }
        public int FirstUserId { get; set; }
        public int SecondUserId { get; set; }

        public virtual User FirstUser { get; set; }
        public virtual User SecondUser { get; set; }
    }
}
