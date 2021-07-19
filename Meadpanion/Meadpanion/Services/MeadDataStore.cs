using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Meadpanion.Models;
using System.Linq;
using SQLite;

namespace Meadpanion.Services
{
    class MeadDataStore : IDataStore<Mead>
    {
        private List<Mead> meads;


        public MeadDataStore()
        {
            LoadMeads();
        }


        private async void LoadMeads()
        { 
            meads = await App.Database.GetMeadsAsync();
        }

        public async Task<bool> AddItemAsync(Mead item)
        {
            return (await App.Database.SaveMeadAsync(item) != 0);
        }

        public async Task<bool> DeleteItemAsync(Mead item)
        {
            return (await App.Database.DeleteMeadAsync(item) != 0);
        }

        public async Task<Mead> GetItemAsync(int id)
        {
            return await Task.FromResult(meads.FirstOrDefault(s => s.ID == id));
        }

        public async Task<IEnumerable<Mead>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(meads);
        }

        public async Task<bool> UpdateItemAsync(Mead item)
        {

            return (await App.Database.UpdateMeadAsync(item) != 0);
        }
    }
}
