using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Meadpanion.Models;

namespace Meadpanion.Services
{
    class EventsDataStore : IDataStore<MeadEvents>
    {
        public async Task<int> AddItemAsync(MeadEvents item)
        {
            return await App.Database.SaveEventAsync(item);
        }

        public async Task<int> DeleteItemAsync(int id)
        {
            return await App.Database.DeleteEventAsync(await GetItemAsync(id));
        }

        public async Task<MeadEvents> GetItemAsync(int id)
        {
            return await App.Database.GetSingleEventAsync(id);
        }

        public async Task<IEnumerable<MeadEvents>> GetItemsAsync(int id)
        {
            return await App.Database.GetEventsAsync(id);

        }

        public async Task<int> UpdateItemAsync(MeadEvents item)
        {
            return await App.Database.UpdateEventAsync(item);
        }
    }
}
