using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Meadpanion.Models;
using Meadpanion.SQL;
using System.Linq;

namespace Meadpanion.Services
{
    public class MeadDataStore : IDataStore<Mead>
    {
        private List<Mead> meads;

        public MeadDataStore() { meads = new List<Mead>(); }

        private async Task LoadMeadData()
        {
            var meadTables = await App.Database.GetMeadsAsync();
            var eventsTable = await App.Database.GetEventsAsync();
            var readingsTable = await App.Database.GetReadingsAsync();

            foreach (var item in meadTables)
            {
                var eventsList = eventsTable.Where((s => s.ID == item.ID));
                List<MeadEvents> meadEventsList = new List<MeadEvents>();
                foreach (var eventItem in eventsList)
                {
                    meadEventsList.Add(new MeadEvents()
                    {
                        ID = eventItem.ID,
                        Note = eventItem.Note,
                        Date = eventItem.Date,
                        Name = eventItem.Name
                    });
                }

                var readingsList = readingsTable.Where((s => s.ID == item.ID));
                List<Readings> readings = new List<Readings>();
                foreach (var readingsItem in readingsList)
                {
                    readings.Add(new Readings()
                    {
                        ID = readingsItem.ID,
                        Date = readingsItem.Date,
                        Note = readingsItem.Note,
                        GravityReading = readingsItem.GravityReading

                    });
                }

                meads.Add(new Mead()
                {
                    ID = item.ID,
                    Name = item.Name,
                    RecipeID = item.RecipeID,
                    Active = item.Active,
                    Amount = item.Amount,
                    Note = item.Note,
                    Events = meadEventsList,
                    Readigns = readings

                });
            }
        }

        public async Task<bool> AddItemAsync(Mead item)
        {
            throw  new NotImplementedException();
        }

        public async Task<bool> DeleteItemAsync(Mead item)
        {
            throw new NotImplementedException();
        }

        public async Task<Mead> GetItemAsync(int id)
        {
            await LoadMeadData();
            return await Task.FromResult(meads.FirstOrDefault(s => s.ID == id));
        }

        public async Task<IEnumerable<Mead>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(meads);
        }

        public async Task<bool> UpdateItemAsync(Mead item)
        {
            throw new NotImplementedException();
        }

        //public async Task<bool> AddItemAsync(Mead item)
        //{
        //    return 
        //}

        //public async Task<bool> DeleteItemAsync(Mead item)
        //{
        //    return (await App.Database.DeleteMeadAsync(item) != 0);
        //}

        //public async Task<Mead> GetItemAsync(int id)
        //{
        //    return await Task.FromResult(meads.FirstOrDefault(s => s.ID == id));
        //}

        //public async Task<IEnumerable<Mead>> GetItemsAsync(bool forceRefresh = false)
        //{
        //    return await Task.FromResult(meads);
        //}

        //public async Task<bool> UpdateItemAsync(Mead item)
        //{

        //    return (await App.Database.UpdateMeadAsync(item) != 0);
        //}
    }
}
