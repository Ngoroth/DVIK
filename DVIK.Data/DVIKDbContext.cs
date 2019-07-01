using Dvik.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dvik.Data
{
    public class DvikDbContext : IdentityDbContext<IdentityUser>
    {
        public DvikDbContext(DbContextOptions<DvikDbContext> options) : base(options) { }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}
