using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Amiq.Services.User.DataAccessLayer.Models
{
    public partial class AmiqUserContext : DbContext
    {
        public AmiqUserContext()
        {
        }

        public AmiqUserContext(DbContextOptions<AmiqUserContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BlockedUser> BlockedUsers { get; set; } = null!;
        public virtual DbSet<TextBlock> TextBlocks { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserDescriptionBlock> UserDescriptionBlocks { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=host.docker.internal,1423;Database=Amiq_User;User Id=sa;Password=123Dimon!!!;");
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

            modelBuilder.Entity<TextBlock>(entity =>
            {
                entity.ToTable("TextBlock", "Core");

                entity.Property(e => e.TextBlockId).ValueGeneratedNever();

                entity.Property(e => e.Content).HasMaxLength(350);

                entity.Property(e => e.Header).HasMaxLength(150);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "User");

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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
