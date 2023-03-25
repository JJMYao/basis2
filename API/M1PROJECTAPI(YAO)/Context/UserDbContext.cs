using M1PROJECTAPI_YAO_.Model;
using Microsoft.EntityFrameworkCore;

namespace M1PROJECTAPI_YAO_.Context
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions <UserDbContext> options) : base(options)
        {

        }
        public DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserModel>().ToTable("users");
        }


    }
}
