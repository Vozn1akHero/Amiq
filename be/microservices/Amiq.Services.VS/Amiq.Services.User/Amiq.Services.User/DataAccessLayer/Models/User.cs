using System;
using System.Collections.Generic;

namespace Amiq.Services.User.DataAccessLayer.Models
{
    public partial class User
    {
        public User()
        {
            BlockedUserDestUsers = new HashSet<BlockedUser>();
            BlockedUserIssuers = new HashSet<BlockedUser>();
            UserDescriptionBlocks = new HashSet<UserDescriptionBlock>();
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

        public virtual ICollection<BlockedUser> BlockedUserDestUsers { get; set; }
        public virtual ICollection<BlockedUser> BlockedUserIssuers { get; set; }
        public virtual ICollection<UserDescriptionBlock> UserDescriptionBlocks { get; set; }
    }
}
