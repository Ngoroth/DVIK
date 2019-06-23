using Dvik.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dvik.Data
{
    public class SqlData<T> : IData<T>
        where T : class, IHaveName
    {
        private readonly DvikDbContext dvikDbContext;

        public SqlData(DvikDbContext dvikDbContext)
        {
            this.dvikDbContext = dvikDbContext;
        }
        public async Task<T> AddAsync(T newCourse)
        {
            await this.dvikDbContext.AddAsync(newCourse);
            return newCourse;
        }

        public async Task<int> CommitAsync()
        {
            return await this.dvikDbContext.SaveChangesAsync();
        }

        public async Task<T> DeleteAsync(int id)
        {
            var item = await this.SearchByIdAsync(id);

            if (item != null)
            {
                this.dvikDbContext.Remove(item);
            }
            return item;
        }

        public async Task<T> SearchByIdAsync(int courseId)
        {
            return await this.dvikDbContext.FindAsync<T>(courseId);
        }

        public async Task<IEnumerable<T>> SearchByNameAsync(string name)
        {
            var query = this.dvikDbContext.Set<T>()
                .Where(c => c.Name.StartsWith(name) || string.IsNullOrEmpty(name))
                .OrderBy(c => c.Name);
            return await query.ToArrayAsync();
        }

        public T Update(T updatedCourse)
        {
            var entity = this.dvikDbContext.Attach(updatedCourse);
            entity.State = EntityState.Modified;
            return updatedCourse;
        }
    }
}
