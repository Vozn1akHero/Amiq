using System;
using System.Collections.Generic;

namespace Amiq.Services.Friendship.DataAccessLayer.Models.Models
{
    public partial class User
    {
        public User()
        {
            FriendRequestIssuers = new HashSet<FriendRequest>();
            FriendRequestReceivers = new HashSet<FriendRequest>();
            FriendshipFirstUsers = new HashSet<Friendship>();
            FriendshipSecondUsers = new HashSet<Friendship>();
        }

        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string AvatarPath { get; set; } = null!;

        public virtual ICollection<FriendRequest> FriendRequestIssuers { get; set; }
        public virtual ICollection<FriendRequest> FriendRequestReceivers { get; set; }
        public virtual ICollection<Friendship> FriendshipFirstUsers { get; set; }
        public virtual ICollection<Friendship> FriendshipSecondUsers { get; set; }
    }
}
