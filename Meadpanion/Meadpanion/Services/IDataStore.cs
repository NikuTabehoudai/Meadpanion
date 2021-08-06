using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meadpanion.Services
{
    public interface IDataStore<T>
    {
        Task<int> AddItemAsync(T item);
        Task<int> UpdateItemAsync(T item);
        Task<int> DeleteItemAsync(int id);
        Task<T> GetItemAsync(int id);
        Task<IEnumerable<T>> GetItemsAsync(int id);
    }
}
