using System;
using System.Collections.Generic;

#nullable disable

namespace AmicaPlus.DataAccess.Models
{
    public partial class Friendship
    {
        public Guid FriendshipId { get; set; }
        public int FirstUserId { get; set; }
        public int SecondUserId { get; set; }
    }
}
