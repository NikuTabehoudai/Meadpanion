using System.Collections.Generic;
using SQLite;
using Meadpanion.Models;
using System.Threading.Tasks;

namespace Meadpanion
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Mead>().Wait();
            _database.CreateTableAsync<Readings>().Wait();
            _database.CreateTableAsync<Events>().Wait();
            _database.CreateTableAsync<Recipe>().Wait();

        }

        public Task<List<Mead>> GetMeadsAsync()
        {
            return _database.Table<Mead>().ToListAsync();
        }

        public Task<List<Readings>> GetReadingsAsync()
        {
            return _database.Table<Readings>().ToListAsync();

        }

        public Task<List<Events>> GetEventsAsync()
        {
            return _database.Table<Events>().ToListAsync();

        }

        public Task<List<Recipe>> GetRecipeAsync()
        {
            return _database.Table<Recipe>().ToListAsync();

        }

        public Task<int> SaveMeadAsync(Mead mead)
        {
            return _database.InsertAsync(mead);
        }

        public Task<int> SaveEventsAsync(Events events)
        {
            return _database.InsertAsync(events);
        }

        public Task<int> SaveReadingsAsync(Readings readings)
        {
            return _database.InsertAsync(readings);
        }

        public Task<int> SavePersonAsync(Recipe recipe)
        {
            return _database.InsertAsync(recipe);
        }

        public Task<int> DeleteMeadAsync(Mead mead)
        {
            return _database.DeleteAsync(mead);
        }
        
        public Task<int> UpdateMeadAsync(Mead mead)
        {
            return _database.UpdateAsync(mead);
        }

    }
}
