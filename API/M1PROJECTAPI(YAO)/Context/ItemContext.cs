using M1PROJECTAPI_YAO_.Model;
using Microsoft.EntityFrameworkCore;

namespace M1PROJECTAPI_YAO_.Context
{
    public class ItemContext : DbContext
    {
        public ItemContext(DbContextOptions<ItemContext> options) : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Item>().ToTable("items");
        }

    }
}
