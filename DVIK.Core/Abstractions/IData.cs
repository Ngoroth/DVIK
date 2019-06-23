using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dvik.Core.Abstractions
{
    public interface IData<T>
    {
        Task<IEnumerable<T>> SearchByNameAsync(string name);
        Task<T> SearchByIdAsync(int itemId);
        T Update(T updatedItem);
        Task<T> AddAsync(T newItem);
        Task<T> DeleteAsync(int id);
        Task<int> CommitAsync();
    }
}
