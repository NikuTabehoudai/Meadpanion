using System.Collections.Generic;
using SQLite;
using System.Threading.Tasks;
using System;
using Meadpanion.SQL;

namespace Meadpanion
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<MeadTable>().Wait();
            _database.CreateTableAsync<ReadingsTable>().Wait();
            _database.CreateTableAsync<EventsTable>().Wait();
            _database.CreateTableAsync<RecipeTable>().Wait();

        }

        public Task<List<MeadTable>> GetMeadsAsync()
        {
            return _database.Table<MeadTable>().ToListAsync();
        }

        public Task<List<ReadingsTable>> GetReadingsAsync()
        {
            return _database.Table<ReadingsTable>().ToListAsync();

        }

        public Task<List<EventsTable>> GetEventsAsync()
        {
            return _database.Table<EventsTable>().ToListAsync();

        }

        public Task<List<RecipeTable>> GetRecipeAsync()
        {
            return _database.Table<RecipeTable>().ToListAsync();

        }

        public Task<int> SaveMeadAsync(MeadTable mead)
        {
            return _database.InsertAsync(mead);
        }

        public Task<int> SaveEventsAsync(EventsTable events)
        {
            return _database.InsertAsync(events);
        }

        public Task<int> SaveReadingsAsync(ReadingsTable readings)
        {
            return _database.InsertAsync(readings);
        }

        public Task<int> SavePersonAsync(RecipeTable recipe)
        {
            return _database.InsertAsync(recipe);
        }

        public Task<int> DeleteMeadAsync(MeadTable mead)
        {
            return _database.DeleteAsync(mead);
        }
        
        public Task<int> UpdateMeadAsync(MeadTable mead)
        {
            return _database.UpdateAsync(mead);
        }
    }

}
