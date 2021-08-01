using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Meadpanion.Models;

namespace Meadpanion.Services
{
    public class MeadDataStore : IDataStore<Mead>
    {

        public async Task<int> AddItemAsync(Mead item)
        {
            return await App.Database.SaveMeadAsync(item);
        }

        public async Task<int> DeleteItemAsync(int ID)
        {
            var item = await App.Database.GetSingleMeadAsync(ID);
            return await App.Database.DeleteMeadAsync(item);
        }

        public async Task<Mead> GetItemAsync(int id)
        {
            return await App.Database.GetSingleMeadAsync(id);
        }

        public async Task<IEnumerable<Mead>> GetItemsAsync(int notused)
        {
            var meads = await App.Database.GetMeadsAsync();
            return meads;
        }

        public async Task<int> UpdateItemAsync(Mead item)
        {
            return await App.Database.UpdateMeadAsync(item);
        }
    }
}
