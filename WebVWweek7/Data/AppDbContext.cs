using Microsoft.EntityFrameworkCore;
using WebVWweek7.Models;

namespace WebVWweek7.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> contextOptions)
            : base(contextOptions)
        {

        }

        public DbSet<Category> categories { get; set; }
        public DbSet<Collection> collections { get; set; }
        public DbSet<Item> items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Collection>()
                .HasOne(o => o.Category)
                .WithMany(v => v.Collections)
                .HasForeignKey(o => o.CategoryId); ;

            modelBuilder.Entity<Item>()
                .HasOne(o => o.Collection)
                .WithMany(v => v.Items)
                .HasForeignKey(o => o.CollectionId); ;
        }

    }
}
