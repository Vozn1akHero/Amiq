using System;
using System.Collections.Generic;

namespace Amiq.Services.Group.DataAccessLayer.Models.Models
{
    public partial class User
    {
        public User()
        {
            GroupBlockedUsers = new HashSet<GroupBlockedUser>();
            GroupEvents = new HashSet<GroupEvent>();
            GroupParticipants = new HashSet<GroupParticipant>();
            Groups = new HashSet<Group>();
            HiddenGroups = new HashSet<HiddenGroup>();
        }

        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public DateTime Birthdate { get; set; }
        public string Sex { get; set; } = null!;
        public string? AvatarPath { get; set; }
        public string? ShortDescription { get; set; }

        public virtual ICollection<GroupBlockedUser> GroupBlockedUsers { get; set; }
        public virtual ICollection<GroupEvent> GroupEvents { get; set; }
        public virtual ICollection<GroupParticipant> GroupParticipants { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<HiddenGroup> HiddenGroups { get; set; }
    }
}
