using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Amiq.Services.Notification.DataAccessLayer.Models
{
    public partial class AmiqNotificationContext : DbContext
    {
        public AmiqNotificationContext()
        {
        }

        public AmiqNotificationContext(DbContextOptions<AmiqNotificationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Friendship> Friendships { get; set; } = null!;
        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<GroupParticipant> GroupParticipants { get; set; } = null!;
        public virtual DbSet<GroupPost> GroupPosts { get; set; } = null!;
        public virtual DbSet<GroupVisitation> GroupVisitations { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<ProfileVisitation> ProfileVisitations { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserPost> UserPosts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=host.docker.internal,1423;Database=Amiq_Notification;User Id=sa;Password=123Dimon!!!;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Friendship>(entity =>
            {
                entity.ToTable("Friendship", "Friendship");

                entity.Property(e => e.FriendshipId).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group", "Group");

                entity.Property(e => e.AvatarSrc).IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(150);
            });

            modelBuilder.Entity<GroupParticipant>(entity =>
            {
                entity.ToTable("GroupParticipant", "Group");

                entity.Property(e => e.GroupParticipantId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Joined).HasColumnType("datetime");
            });

            modelBuilder.Entity<GroupPost>(entity =>
            {
                entity.ToTable("GroupPost", "Post");

                entity.Property(e => e.GroupPostId).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.EditedAt).HasColumnType("datetime");

                entity.Property(e => e.TextContent).HasMaxLength(500);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.GroupPostAuthors)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupPost_Author");

                entity.HasOne(d => d.EditedByNavigation)
                    .WithMany(p => p.GroupPostEditedByNavigations)
                    .HasForeignKey(d => d.EditedBy)
                    .HasConstraintName("FK_GroupPost_EditedBy");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupPosts)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupPost_Group");
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

            modelBuilder.Entity<ProfileVisitation>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ProfileVisitation", "Activity");

                entity.Property(e => e.LastVisited).HasColumnType("datetime");

                entity.Property(e => e.ProfileVisitationId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.VisitedUser)
                    .WithMany()
                    .HasForeignKey(d => d.VisitedUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProfileVisitation_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "User");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.Surname).HasMaxLength(150);
            });

            modelBuilder.Entity<UserPost>(entity =>
            {
                entity.ToTable("UserPost", "Post");

                entity.Property(e => e.UserPostId).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.EditedAt).HasColumnType("datetime");

                entity.Property(e => e.TextContent).HasMaxLength(500);

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
