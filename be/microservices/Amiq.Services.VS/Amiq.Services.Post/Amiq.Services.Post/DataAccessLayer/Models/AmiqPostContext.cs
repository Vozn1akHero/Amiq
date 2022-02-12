using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Amiq.Services.Post.DataAccessLayer.Models
{
    public partial class AmiqPostContext : DbContext
    {
        public AmiqPostContext()
        {
        }

        public AmiqPostContext(DbContextOptions<AmiqPostContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<GroupPost> GroupPosts { get; set; } = null!;
        public virtual DbSet<GroupPostComment> GroupPostComments { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserPost> UserPosts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=host.docker.internal,1423;Database=Amiq_Post;User Id=sa;Password=123Dimon!!!;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group", "Group");

                entity.Property(e => e.AvatarSrc).IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(150);
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
                    .OnDelete(DeleteBehavior.ClientSetNull)
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
