using System;
using System.Collections.Generic;

namespace Amiq.Services.Notification.DataAccessLayer.Models.Models
{
    public partial class User
    {
        public User()
        {
            GroupPostAuthors = new HashSet<GroupPost>();
            GroupPostEditedByNavigations = new HashSet<GroupPost>();
            Notifications = new HashSet<Notification>();
            UserPosts = new HashSet<UserPost>();
        }

        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string AvatarPath { get; set; } = null!;

        public virtual ICollection<GroupPost> GroupPostAuthors { get; set; }
        public virtual ICollection<GroupPost> GroupPostEditedByNavigations { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<UserPost> UserPosts { get; set; }
    }
}
