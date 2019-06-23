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
        protected readonly DvikDbContext dvikDbContext;

        public SqlData(DvikDbContext dvikDbContext)
        {
            this.dvikDbContext = dvikDbContext;
        }
        public async Task<T> AddAsync(T newItem)
        {
            await this.dvikDbContext.AddAsync(newItem);
            return newItem;
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

        public virtual async Task<T> SearchByIdAsync(int itemId)
        {
            return await this.dvikDbContext.FindAsync<T>(itemId);
        }

        public async Task<IEnumerable<T>> SearchByNameAsync(string name)
        {
            var query = this.dvikDbContext.Set<T>()
                .Where(c => c.Name.StartsWith(name) || string.IsNullOrEmpty(name))
                .OrderBy(c => c.Name);
            return await query.ToArrayAsync();
        }

        public T Update(T updatedItem)
        {
            var entity = this.dvikDbContext.Attach(updatedItem);
            entity.State = EntityState.Modified;
            return updatedItem;
        }
    }
}
