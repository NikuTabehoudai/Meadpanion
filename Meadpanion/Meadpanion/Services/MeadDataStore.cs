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
            meads.Clear();
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
                List<Reading> readings = new List<Reading>();
                foreach (var readingsItem in readingsList)
                {
                    readings.Add(new Reading()
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
                    Date = item.Date,
                    RecipeID = item.RecipeID,
                    Active = item.Active,
                    Amount = item.Amount,
                    Note = item.Note,
                    Events = meadEventsList,
                    Readings = readings

                });
            }
        }

        public async Task<bool> AddItemAsync(Mead item)
        {
            var meadTable = new MeadTable()
            {
                Name = item.Name,
                RecipeID = item.RecipeID,
                Active = item.Active,
                Date = item.Date,
                Amount = item.Amount,
                Note = item.Note
            };

            var meadID = await App.Database.SaveMeadAsync(meadTable);

            var readingTable = new ReadingsTable()
            {
                Date = item.Readings[0].Date,
                GravityReading = item.Readings[0].GravityReading,
                Note = item.Readings[0].Note,
                MeadID = meadID
                
            };

            await App.Database.SaveReadingsAsync(readingTable);

            return true;
        }

        public async Task<int> DeleteItemAsync(Mead item)
        {
            throw new NotImplementedException();
        }

        public async Task<Mead> GetItemAsync(int id)
        {
            
            return await Task.FromResult(meads.FirstOrDefault(s => s.ID == id));
        }

        public async Task<IEnumerable<Mead>> GetItemsAsync(bool forceRefresh = false)
        {
            await LoadMeadData();
            return await Task.FromResult(meads);
        }

        public async Task<int> UpdateItemAsync(Mead item)
        {
            return await App.Database.UpdateMeadAsync(new MeadTable() 
            { ID = item.ID,
            Active = item.Active,
            Amount = item.Amount,
            Date = item.Date,
            RecipeID = item.RecipeID,
            Name = item.Name,
            Note = item.Note,
            });
        }
    }
}
