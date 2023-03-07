using FinalProjectEntityFramework.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectEntityFramework.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Chore> Chores { get; set; }
        public virtual DbSet<ChoreUser> ChoreUsers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}