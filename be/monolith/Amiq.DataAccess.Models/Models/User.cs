using System;
using System.Collections.Generic;

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
            FriendshipFirstUsers = new HashSet<Friendship>();
            FriendshipSecondUsers = new HashSet<Friendship>();
            GroupBlockedUsers = new HashSet<GroupBlockedUser>();
            GroupEvents = new HashSet<GroupEvent>();
            GroupParticipants = new HashSet<GroupParticipant>();
            GroupPosts = new HashSet<GroupPost>();
            Groups = new HashSet<Group>();
            HiddenGroups = new HashSet<HiddenGroup>();
            Messages = new HashSet<Message>();
            NotificationQueues = new HashSet<NotificationQueue>();
            Notifications = new HashSet<Notification>();
            Posts = new HashSet<Post>();
            Sessions = new HashSet<Session>();
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
        public string ShortDescription { get; set; }

        public virtual ICollection<BlockedUser> BlockedUserDestUsers { get; set; }
        public virtual ICollection<BlockedUser> BlockedUserIssuers { get; set; }
        public virtual ICollection<Chat> ChatFirstUsers { get; set; }
        public virtual ICollection<Chat> ChatSecondUsers { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<FriendRequest> FriendRequestIssuers { get; set; }
        public virtual ICollection<FriendRequest> FriendRequestReceivers { get; set; }
        public virtual ICollection<Friendship> FriendshipFirstUsers { get; set; }
        public virtual ICollection<Friendship> FriendshipSecondUsers { get; set; }
        public virtual ICollection<GroupBlockedUser> GroupBlockedUsers { get; set; }
        public virtual ICollection<GroupEvent> GroupEvents { get; set; }
        public virtual ICollection<GroupParticipant> GroupParticipants { get; set; }
        public virtual ICollection<GroupPost> GroupPosts { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<HiddenGroup> HiddenGroups { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<NotificationQueue> NotificationQueues { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
        public virtual ICollection<UserDescriptionBlock> UserDescriptionBlocks { get; set; }
        public virtual ICollection<UserPost> UserPosts { get; set; }
    }
}
