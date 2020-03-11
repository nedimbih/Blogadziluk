using Blogadziluk.Models;
using Microsoft.EntityFrameworkCore;

namespace Blogadziluk.Data.DataAccess {
    public class BlogadzilukDbContext : DbContext {

        public BlogadzilukDbContext(DbContextOptions<BlogadzilukDbContext> options)
                : base(options) {
            Database.Migrate();
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Tags2Posts>()
                .HasKey(t => new { t.PostId, t.TagId });

            modelBuilder.Entity<Tags2Posts>()
                .HasOne(pt => pt.Post)
                .WithMany(p => p.PostTags)
                .HasForeignKey(pt => pt.PostId);

            modelBuilder.Entity<Tags2Posts>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.PostTags)
                .HasForeignKey(pt => pt.TagId);

            modelBuilder.Entity<Post>()
                .HasIndex(p => p.Slug)
                .IsUnique();

            modelBuilder.Entity<Tag>()
                .HasIndex(t => t.Text)
                .IsUnique();
        }
    }
}
