using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Catedra_3_Backend.src.models;
using Microsoft.EntityFrameworkCore;

namespace Catedra_3_Backend.src.data
{
    public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<User>(options)
    {
        public new required DbSet<User> Users { get; set; }
        public required DbSet<Post> Posts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Post>().HasKey(p => new { p.UserId });
            modelBuilder.Entity<User>()
            .Property(u => u.Id);
        }

    }
}