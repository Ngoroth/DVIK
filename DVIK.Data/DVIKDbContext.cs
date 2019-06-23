using Dvik.Core;
using Microsoft.EntityFrameworkCore;

namespace Dvik.Data
{
    public class DvikDbContext : DbContext
    {
        public DvikDbContext(DbContextOptions<DvikDbContext> options) : base(options) { }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}
