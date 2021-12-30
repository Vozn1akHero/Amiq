using System;
using System.Collections.Generic;

namespace Amiq.Services.Friendship.DataAccessLayer.Models.Models
{
    public partial class Friendship
    {
        public Guid FriendshipId { get; set; }
        public int FirstUserId { get; set; }
        public int SecondUserId { get; set; }
    }
}
