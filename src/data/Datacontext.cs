using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Catedra_3_Backend.src.models;
using Microsoft.EntityFrameworkCore;

namespace Catedra_3_Backend.src.data
{
    public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<User>(options)
    {
        public new required DbSet<User> Users { get; set; }
    }
}