using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Amiq.Services.Notification.DataAccessLayer.Models.Models
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
        public virtual DbSet<GroupParticipant> GroupParticipants { get; set; } = null!;
        public virtual DbSet<GroupVisitation> GroupVisitations { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<ProfileVisitation> ProfileVisitations { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

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

            modelBuilder.Entity<GroupParticipant>(entity =>
            {
                entity.ToTable("GroupParticipant", "Group");

                entity.Property(e => e.GroupParticipantId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Joined).HasColumnType("datetime");
            });

            modelBuilder.Entity<GroupVisitation>(entity =>
            {
                entity.ToTable("GroupVisitation", "Activity");

                entity.Property(e => e.GroupVisitationId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.LastVisited).HasColumnType("datetime");
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
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "User");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.Surname).HasMaxLength(150);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
