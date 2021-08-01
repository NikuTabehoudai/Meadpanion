using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meadpanion.Models;

namespace Meadpanion.Services
{
    class ReadingDataStore : IDataStore<Reading>
    {

        public async Task<int> AddItemAsync(Reading item)
        {
            return await App.Database.SaveReadingsAsync(item);
        }

        public async Task<int> DeleteItemAsync(int ID)
        {
            var item = await App.Database.GetSingleReadingAsync(ID);
            return await App.Database.DeleteReadingAsync(item);
        }

        public async Task<Reading> GetItemAsync(int id)
        {
            return await App.Database.GetSingleReadingAsync(id);
        }

        public async Task<IEnumerable<Reading>> GetItemsAsync(int meadId)
        {
            return await App.Database.GetReadingsAsync(meadId);
        }

        public async Task<int> UpdateItemAsync(Reading item)
        {
            return await App.Database.UpdateRaedingsAsync(item);
        }
    }
}
