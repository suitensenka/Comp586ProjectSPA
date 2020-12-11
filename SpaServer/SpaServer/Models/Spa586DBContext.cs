using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SpaServer.Models
{
    public partial class Spa586DBContext : DbContext
    {
        public Spa586DBContext()
        {
        }

        public Spa586DBContext(DbContextOptions<Spa586DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Boards> Boards { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Users> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Boards>(entity =>
            {
                entity.HasIndex(e => e.User)
                    .HasName("user_idx");

                entity.Property(e => e.Content)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Subject)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.User)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.Boards)
                    .HasForeignKey(d => d.User)
                    .HasConstraintName("user");
            });

            modelBuilder.Entity<Comments>(entity =>
            {
                entity.HasIndex(e => e.IdBoard)
                    .HasName("idBoard_idx");

                entity.HasIndex(e => e.User)
                    .HasName("user_idx");

                entity.Property(e => e.Content)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.User)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.IdBoardNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.IdBoard)
                    .HasConstraintName("idBoard");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.User)
                    .HasConstraintName("user2");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.User)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.User)
                    .HasName("email_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.User)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Role)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Username)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
