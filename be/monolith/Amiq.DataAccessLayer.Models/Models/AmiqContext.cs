using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Amiq.DataAccessLayer.Models.Models
{
    public partial class AmiqContext : DbContext
    {
        public AmiqContext()
        {
        }

        public AmiqContext(DbContextOptions<AmiqContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BlockedUser> BlockedUsers { get; set; } = null!;
        public virtual DbSet<Chat> Chats { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<FriendRequest> FriendRequests { get; set; } = null!;
        public virtual DbSet<Friendship> Friendships { get; set; } = null!;
        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<GroupBlockedUser> GroupBlockedUsers { get; set; } = null!;
        public virtual DbSet<GroupDescriptionBlock> GroupDescriptionBlocks { get; set; } = null!;
        public virtual DbSet<GroupEvent> GroupEvents { get; set; } = null!;
        public virtual DbSet<GroupEventParticipant> GroupEventParticipants { get; set; } = null!;
        public virtual DbSet<GroupParticipant> GroupParticipants { get; set; } = null!;
        public virtual DbSet<GroupPost> GroupPosts { get; set; } = null!;
        public virtual DbSet<GroupPostComment> GroupPostComments { get; set; } = null!;
        public virtual DbSet<GroupVisitation> GroupVisitations { get; set; } = null!;
        public virtual DbSet<HiddenGroup> HiddenGroups { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<ProfileVisitation> ProfileVisitations { get; set; } = null!;
        public virtual DbSet<Session> Sessions { get; set; } = null!;
        public virtual DbSet<TextBlock> TextBlocks { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserDescriptionBlock> UserDescriptionBlocks { get; set; } = null!;
        public virtual DbSet<UserPost> UserPosts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=host.docker.internal,1423;Database=Amiq;User Id=sa;Password=123Dimon!!!;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlockedUser>(entity =>
            {
                entity.HasKey(e => e.BlockedUsersId);

                entity.ToTable("BlockedUsers", "User");

                entity.Property(e => e.BlockedUsersId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.DestUser)
                    .WithMany(p => p.BlockedUserDestUsers)
                    .HasForeignKey(d => d.DestUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BlockedUsers_DestUser");

                entity.HasOne(d => d.Issuer)
                    .WithMany(p => p.BlockedUserIssuers)
                    .HasForeignKey(d => d.IssuerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BlockedUsers_IssuerUser");
            });

            modelBuilder.Entity<Chat>(entity =>
            {
                entity.ToTable("Chat", "Chat");

                entity.Property(e => e.ChatId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.FirstUser)
                    .WithMany(p => p.ChatFirstUsers)
                    .HasForeignKey(d => d.FirstUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Chat_User");

                entity.HasOne(d => d.SecondUser)
                    .WithMany(p => p.ChatSecondUsers)
                    .HasForeignKey(d => d.SecondUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Chat_User1");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment", "Post");

                entity.HasComment("Id grupy, jeśli komentarz został stworzony przez administratora grupy");

                entity.Property(e => e.CommentId).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EditedAt).HasColumnType("datetime");

                entity.Property(e => e.TextContent).HasMaxLength(250);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_User");

                entity.HasOne(d => d.MainParent)
                    .WithMany(p => p.InverseMainParent)
                    .HasForeignKey(d => d.MainParentId)
                    .HasConstraintName("FK_Comment_MainParentComment");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Comment_ParentComment");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK_Comment_Post");
            });

            modelBuilder.Entity<FriendRequest>(entity =>
            {
                entity.ToTable("FriendRequest", "Friendship");

                entity.Property(e => e.FriendRequestId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Issuer)
                    .WithMany(p => p.FriendRequestIssuers)
                    .HasForeignKey(d => d.IssuerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FriendRequest_UserIssuer");

                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.FriendRequestReceivers)
                    .HasForeignKey(d => d.ReceiverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FriendRequest_UserReceiver");
            });

            modelBuilder.Entity<Friendship>(entity =>
            {
                entity.ToTable("Friendship", "Friendship");

                entity.Property(e => e.FriendshipId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.FirstUser)
                    .WithMany(p => p.FriendshipFirstUsers)
                    .HasForeignKey(d => d.FirstUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Friendship_FirstUser");

                entity.HasOne(d => d.SecondUser)
                    .WithMany(p => p.FriendshipSecondUsers)
                    .HasForeignKey(d => d.SecondUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Friendship_SecondUser");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group", "Group");

                entity.HasIndex(e => e.Name, "IX_Group_Name");

                entity.HasIndex(e => e.Name, "IX_Group_ViewName");

                entity.Property(e => e.AvatarSrc).IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupCreator_User");
            });

            modelBuilder.Entity<GroupBlockedUser>(entity =>
            {
                entity.ToTable("GroupBlockedUsers", "Group");

                entity.Property(e => e.GroupBlockedUserId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.BannedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.BannedUntil).HasColumnType("datetime");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupBlockedUsers)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupBlockedUsers_Group");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.GroupBlockedUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupBlockedUsers_User");
            });

            modelBuilder.Entity<GroupDescriptionBlock>(entity =>
            {
                entity.ToTable("GroupDescriptionBlock", "Group");

                entity.Property(e => e.GroupDescriptionBlockId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupDescriptionBlocks)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_GroupDescriptionBlock_Group");

                entity.HasOne(d => d.TextBlock)
                    .WithMany(p => p.GroupDescriptionBlocks)
                    .HasForeignKey(d => d.TextBlockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupDescriptionBlock_TextBlock");
            });

            modelBuilder.Entity<GroupEvent>(entity =>
            {
                entity.ToTable("GroupEvent", "Group");

                entity.Property(e => e.GroupEventId).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.AvatarSrc).IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.GroupEvents)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupEvent_User");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupEvents)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupEvent_Group");
            });

            modelBuilder.Entity<GroupEventParticipant>(entity =>
            {
                entity.ToTable("GroupEventParticipant", "Group");

                entity.Property(e => e.GroupEventParticipantId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.JoinedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.GroupEvent)
                    .WithMany(p => p.GroupEventParticipants)
                    .HasForeignKey(d => d.GroupEventId)
                    .HasConstraintName("FK_GroupEventParticipant_GroupEvent");

                entity.HasOne(d => d.GroupParticipant)
                    .WithMany(p => p.GroupEventParticipants)
                    .HasForeignKey(d => d.GroupParticipantId)
                    .HasConstraintName("FK_GroupEventParticipant_GroupParticipant");
            });

            modelBuilder.Entity<GroupParticipant>(entity =>
            {
                entity.ToTable("GroupParticipant", "Group");

                entity.Property(e => e.GroupParticipantId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.IsParticipantVisible)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Joined)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupParticipants)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_GroupParticipant_Group");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.GroupParticipants)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupParticipant_User");
            });

            modelBuilder.Entity<GroupPost>(entity =>
            {
                entity.ToTable("GroupPost", "Post");

                entity.Property(e => e.GroupPostId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.VisibleAsCreatedByAdmin)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.GroupPosts)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupPost_User");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupPosts)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupPost_Group");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.GroupPosts)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK_GroupPost_Post");
            });

            modelBuilder.Entity<GroupPostComment>(entity =>
            {
                entity.ToTable("GroupPostComment", "Post");

                entity.Property(e => e.GroupPostCommentId).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.AuthorVisibilityType)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('U')");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.GroupPostComments)
                    .HasForeignKey(d => d.CommentId)
                    .HasConstraintName("FK_GroupPostComment_Comment");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupPostComments)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_GroupPostComment_Group");

                entity.HasOne(d => d.MainParent)
                    .WithMany(p => p.InverseMainParent)
                    .HasForeignKey(d => d.MainParentId)
                    .HasConstraintName("FK_GroupPostComment_GroupPostComment");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_GroupPostComment_GroupPostComment1");
            });

            modelBuilder.Entity<GroupVisitation>(entity =>
            {
                entity.ToTable("GroupVisitation", "Activity");

                entity.Property(e => e.GroupVisitationId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.LastVisited).HasColumnType("datetime");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupVisitations)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupVisitation_Group");
            });

            modelBuilder.Entity<HiddenGroup>(entity =>
            {
                entity.ToTable("HiddenGroup", "Group");

                entity.Property(e => e.HiddenGroupId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.HiddenGroups)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HiddenGroup_Group");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.HiddenGroups)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HiddenGroup_User");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message", "Chat");

                entity.Property(e => e.MessageId).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TextContent).HasMaxLength(250);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Message_User");

                entity.HasOne(d => d.Chat)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.ChatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Message_Chat");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notification", "Notification");

                entity.Property(e => e.NotificationId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.NotificationType)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Text).HasMaxLength(1000);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notification_User");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post", "Post");

                entity.Property(e => e.PostId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EditedAt).HasColumnType("datetime");

                entity.Property(e => e.TextContent).HasMaxLength(500);

                entity.HasOne(d => d.EditedByNavigation)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.EditedBy)
                    .HasConstraintName("FK_Post_User");
            });

            modelBuilder.Entity<ProfileVisitation>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ProfileVisitation", "Activity");

                entity.Property(e => e.LastVisited).HasColumnType("datetime");

                entity.Property(e => e.ProfileVisitationId).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.ToTable("Session", "User");

                entity.Property(e => e.SessionId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.EndedAt).HasColumnType("datetime");

                entity.Property(e => e.SessionToken)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.StartedAt).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Session_User");
            });

            modelBuilder.Entity<TextBlock>(entity =>
            {
                entity.ToTable("TextBlock", "Core");

                entity.Property(e => e.TextBlockId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Content).HasMaxLength(350);

                entity.Property(e => e.Header).HasMaxLength(150);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "User");

                entity.HasIndex(e => new { e.Name, e.Surname }, "IX_User");

                entity.HasIndex(e => new { e.Name, e.Surname }, "IX_User_ViewName");

                entity.HasIndex(e => e.Login, "UC_UserLogin")
                    .IsUnique();

                entity.Property(e => e.AvatarPath).IsUnicode(false);

                entity.Property(e => e.Birthdate).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Login)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Sex)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ShortDescription).HasMaxLength(400);

                entity.Property(e => e.Surname)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserDescriptionBlock>(entity =>
            {
                entity.ToTable("UserDescriptionBlock", "User");

                entity.Property(e => e.UserDescriptionBlockId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.TextBlock)
                    .WithMany(p => p.UserDescriptionBlocks)
                    .HasForeignKey(d => d.TextBlockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserDescriptionBlock_TextBlock");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserDescriptionBlocks)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserDescriptionBlock_User");
            });

            modelBuilder.Entity<UserPost>(entity =>
            {
                entity.ToTable("UserPost", "Post");

                entity.Property(e => e.UserPostId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.UserPosts)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK_UserPost_Post");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPosts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserPost_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
