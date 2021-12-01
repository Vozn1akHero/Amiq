using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Amiq.Services.Group.DataAccessLayer.Models.Models
{
    public partial class AmiqGroupContext : DbContext
    {
        public AmiqGroupContext()
        {
        }

        public AmiqGroupContext(DbContextOptions<AmiqGroupContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<GroupBlockedUser> GroupBlockedUsers { get; set; } = null!;
        public virtual DbSet<GroupDescriptionBlock> GroupDescriptionBlocks { get; set; } = null!;
        public virtual DbSet<GroupEvent> GroupEvents { get; set; } = null!;
        public virtual DbSet<GroupEventParticipant> GroupEventParticipants { get; set; } = null!;
        public virtual DbSet<GroupParticipant> GroupParticipants { get; set; } = null!;
        public virtual DbSet<HiddenGroup> HiddenGroups { get; set; } = null!;
        public virtual DbSet<TextBlock> TextBlocks { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("data source=localhost,1433;User Id=sa;Password=123dimon;Database=Amiq_Group;MultipleActiveResultSets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group", "Group");

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

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.AvatarPath).IsUnicode(false);

                entity.Property(e => e.Birthdate).HasColumnType("date");

                entity.Property(e => e.Name)
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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
