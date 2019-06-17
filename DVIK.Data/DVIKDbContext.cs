using Dvik.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Dvik.Data
{
    public class DvikDbContext : DbContext
    {
        public DvikDbContext(DbContextOptions<DvikDbContext> options) : base(options) { }
        public DbSet<Course> Courses { get; set; }
    }
}
