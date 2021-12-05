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
        public string Login { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime Birthdate { get; set; }
        public string Sex { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? AvatarPath { get; set; }
        public string? ShortDescription { get; set; }

        public virtual ICollection<FriendRequest> FriendRequestIssuers { get; set; }
        public virtual ICollection<FriendRequest> FriendRequestReceivers { get; set; }
        public virtual ICollection<Friendship> FriendshipFirstUsers { get; set; }
        public virtual ICollection<Friendship> FriendshipSecondUsers { get; set; }
    }
}
