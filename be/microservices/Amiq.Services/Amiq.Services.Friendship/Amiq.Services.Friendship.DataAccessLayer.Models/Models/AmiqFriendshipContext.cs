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
                optionsBuilder.UseSqlServer("Server=host.docker.internal,1423;Database=Amiq_Friendship;User Id=sa;Password=123Dimon!!!;");
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
                    .HasConstraintName("FK_FriendRequest_Issuer");

                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.FriendRequestReceivers)
                    .HasForeignKey(d => d.ReceiverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FriendRequest_Receiver");
            });

            modelBuilder.Entity<Friendship>(entity =>
            {
                entity.ToTable("Friendship", "Friendship");

                entity.Property(e => e.FriendshipId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.FirstUser)
                    .WithMany(p => p.FriendshipFirstUsers)
                    .HasForeignKey(d => d.FirstUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Friendship_User");

                entity.HasOne(d => d.SecondUser)
                    .WithMany(p => p.FriendshipSecondUsers)
                    .HasForeignKey(d => d.SecondUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Friendship_User1");
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
