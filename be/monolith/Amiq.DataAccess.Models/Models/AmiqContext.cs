using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Amiq.DataAccess.Models.Models
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

        public virtual DbSet<BlockedUser> BlockedUsers { get; set; }
        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<FriendRequest> FriendRequests { get; set; }
        public virtual DbSet<Friendship> Friendships { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupBlockedUser> GroupBlockedUsers { get; set; }
        public virtual DbSet<GroupDescriptionBlock> GroupDescriptionBlocks { get; set; }
        public virtual DbSet<GroupParticipant> GroupParticipants { get; set; }
        public virtual DbSet<GroupPost> GroupPosts { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<TextBlock> TextBlocks { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserDescriptionBlock> UserDescriptionBlocks { get; set; }
        public virtual DbSet<UserPost> UserPosts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("data source=localhost;Database=Amiq;MultipleActiveResultSets=True;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Polish_CI_AS");

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

                entity.Property(e => e.CommentId).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EditedAt).HasColumnType("datetime");

                entity.Property(e => e.TextContent)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_User");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Comment_ParentComment");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
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
                entity.HasNoKey();

                entity.ToTable("Friendship", "Friendship");

                entity.Property(e => e.FriendshipId).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group", "Group");

                entity.Property(e => e.AvatarSrc).IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<GroupBlockedUser>(entity =>
            {
                entity.ToTable("GroupBlockedUsers", "Group");

                entity.Property(e => e.GroupBlockedUserId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.BanDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserInt)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);
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

            modelBuilder.Entity<GroupParticipant>(entity =>
            {
                entity.ToTable("GroupParticipant", "Group");

                entity.Property(e => e.GroupParticipantId).HasDefaultValueSql("(newid())");

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

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message", "Chat");

                entity.Property(e => e.MessageId).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TextContent)
                    .IsRequired()
                    .HasMaxLength(250);

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

            modelBuilder.Entity<TextBlock>(entity =>
            {
                entity.ToTable("TextBlock", "Core");

                entity.Property(e => e.TextBlockId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Content).HasMaxLength(350);

                entity.Property(e => e.Header)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "User");

                entity.HasIndex(e => new { e.Name, e.Surname }, "IX_User");

                entity.HasIndex(e => e.Login, "UC_UserLogin")
                    .IsUnique();

                entity.Property(e => e.AvatarPath).IsUnicode(false);

                entity.Property(e => e.Birthdate).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Sex)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.ShortDescription).HasMaxLength(400);

                entity.Property(e => e.Surname)
                    .IsRequired()
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
