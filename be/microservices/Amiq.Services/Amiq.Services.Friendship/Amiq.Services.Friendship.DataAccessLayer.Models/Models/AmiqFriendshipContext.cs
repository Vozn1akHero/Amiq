using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Amiq.Services.Friendship.DataAccessLayer.Models.Models
{
    public partial class AmiqFriendshipContext : DbContext
    {
        public AmiqFriendshipContext()
        {
        }

        public AmiqFriendshipContext(DbContextOptions<AmiqFriendshipContext> options)
            : base(options)
        {
        }

        public virtual DbSet<FriendRequest> FriendRequests { get; set; } = null!;
        public virtual DbSet<Friendship> Friendships { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("data source=localhost;Database=Amiq_Friendship;MultipleActiveResultSets=True;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
