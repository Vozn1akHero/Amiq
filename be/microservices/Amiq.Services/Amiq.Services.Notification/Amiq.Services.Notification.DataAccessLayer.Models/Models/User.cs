using System;
using System.Collections.Generic;

namespace Amiq.Services.Notification.DataAccessLayer.Models.Models
{
    public partial class User
    {
        public User()
        {
            FriendshipFirstUsers = new HashSet<Friendship>();
            FriendshipSecondUsers = new HashSet<Friendship>();
            Notifications = new HashSet<Notification>();
        }

        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string AvatarPath { get; set; } = null!;

        public virtual ICollection<Friendship> FriendshipFirstUsers { get; set; }
        public virtual ICollection<Friendship> FriendshipSecondUsers { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
