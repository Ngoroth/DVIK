using Dvik.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dvik.Data
{
    public class SqlTrainerData : SqlData<Trainer>
    {
        public SqlTrainerData(DvikDbContext dvikDbContext) : base(dvikDbContext)
        {
        }

        public override async Task<Trainer> SearchByIdAsync(int trainerId)
        {
            return await this.dvikDbContext.Trainers
                .Include(t => t.Photo)
                .FirstOrDefaultAsync(t => t.Id == trainerId);
        }
        public override async Task<IEnumerable<Trainer>> SearchByNameAsync(string name)
        {
            var query = this.dvikDbContext.Trainers.Include(t => t.Photo)
                .Where(c => c.Name.StartsWith(name) || string.IsNullOrEmpty(name))
                .OrderBy(c => c.Name);
            return await query.ToArrayAsync();
        }
    }
}
