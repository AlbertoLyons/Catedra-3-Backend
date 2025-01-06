using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Catedra_3_Backend.src.models;
using Microsoft.EntityFrameworkCore;

namespace Catedra_3_Backend.src.data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public new DbSet<User> Users { get; set; }
    }
}