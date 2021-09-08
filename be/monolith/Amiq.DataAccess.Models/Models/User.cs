﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Amiq.DataAccess.Models.Models
{
    public partial class User
    {
        public User()
        {
            BlockedUserDestUsers = new HashSet<BlockedUser>();
            BlockedUserIssuers = new HashSet<BlockedUser>();
            ChatFirstUsers = new HashSet<Chat>();
            ChatSecondUsers = new HashSet<Chat>();
            Comments = new HashSet<Comment>();
            FriendRequestIssuers = new HashSet<FriendRequest>();
            FriendRequestReceivers = new HashSet<FriendRequest>();
            GroupParticipants = new HashSet<GroupParticipant>();
            Messages = new HashSet<Message>();
            UserDescriptionBlocks = new HashSet<UserDescriptionBlock>();
            UserPosts = new HashSet<UserPost>();
        }

        public int UserId { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public DateTime Birthdate { get; set; }
        public string Sex { get; set; }
        public string Email { get; set; }
        public string AvatarPath { get; set; }

        public virtual ICollection<BlockedUser> BlockedUserDestUsers { get; set; }
        public virtual ICollection<BlockedUser> BlockedUserIssuers { get; set; }
        public virtual ICollection<Chat> ChatFirstUsers { get; set; }
        public virtual ICollection<Chat> ChatSecondUsers { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<FriendRequest> FriendRequestIssuers { get; set; }
        public virtual ICollection<FriendRequest> FriendRequestReceivers { get; set; }
        public virtual ICollection<GroupParticipant> GroupParticipants { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<UserDescriptionBlock> UserDescriptionBlocks { get; set; }
        public virtual ICollection<UserPost> UserPosts { get; set; }
    }
}
